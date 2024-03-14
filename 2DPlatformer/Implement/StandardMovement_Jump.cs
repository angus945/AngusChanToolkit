using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement_Jump : MonoBehaviour, IMovementRequire
{
    IDecisionInput input;
    void IMovementRequire.VelocityModifier(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state)
    {
        this.input = input;

        HandleJump(properity, input, collision, state);
        HandleGravity(properity, input, collision, state);

        ClampJump(properity, input, collision, state);
    }

    float _time;

    void Update()
    {
        _time += Time.deltaTime;

        if (input.Jump)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    bool _jumpToConsume;
    bool _bufferedJumpUsable;
    bool _endedJumpEarly;
    bool _coyoteUsable;
    float _timeJumpWasPressed;


    void HandleJump(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state)
    {
        bool HasBufferedJump = _bufferedJumpUsable && _time < _timeJumpWasPressed + properity.jumpBuffer;
        bool CanUseCoyote = _coyoteUsable && !collision.grounded && _time < collision.frameLeftGrounded + properity.coyoteTime;

        if (!_endedJumpEarly && !collision.grounded && !input.JumpKeep && state.lastVelocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (collision.grounded || CanUseCoyote)
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            state.currentVelocity.y = properity.jumpPower;
        }

        _jumpToConsume = false;
    }

    void HandleGravity(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state)
    {
        if (collision.grounded && state.currentVelocity.y <= 0f)
        {
            state.currentVelocity.y = properity.groundingForce;
        }
        else
        {
            float inAirGravity = properity.fallAcceleration;
            if (_endedJumpEarly && state.currentVelocity.y > 0)
            {
                inAirGravity *= properity.jumpEndEarlyGravityModifier;
            }
            state.currentVelocity.y = Mathf.MoveTowards(state.currentVelocity.y, -properity.maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    void ClampJump(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state)
    {
        if (collision.ceilingHit)
        {
            state.currentVelocity.y = Mathf.Min(0, state.currentVelocity.y);
        }

        // Landed on the Ground
        if (collision.grounded && collision.groundHit)
        {
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
        }
    }
}
