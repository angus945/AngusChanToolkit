using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : MonoBehaviour, IMovementRequire
{
    [SerializeField] TarodevController.ScriptableStats _stats;

    IDecisionInput input;
    void IMovementRequire.VelocityModifier(IDecisionInput input, ICollision collision, MovementStatus state)
    {
        this.input = input;

        HandleJump(input, collision, state);
        HandleGravity(input, collision, state);

        ClampJump(input, collision, state);
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

    bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;

    void HandleJump(IDecisionInput input, ICollision collision, MovementStatus state)
    {
        bool CanUseCoyote = _coyoteUsable && !collision.grounded && _time < collision.frameLeftGrounded + _stats.CoyoteTime;

        if (!_endedJumpEarly && !collision.grounded && !input.JumpKeep && state.lastVelocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (collision.grounded || CanUseCoyote)
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            state.currentVelocity.y = _stats.JumpPower;
        }

        _jumpToConsume = false;
    }

    void HandleGravity(IDecisionInput input, ICollision collision, MovementStatus state)
    {
        if (collision.grounded && state.currentVelocity.y <= 0f)
        {
            state.currentVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && state.currentVelocity.y > 0)
            {
                inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            }
            state.currentVelocity.y = Mathf.MoveTowards(state.currentVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    void ClampJump(IDecisionInput input, ICollision collision, MovementStatus state)
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
