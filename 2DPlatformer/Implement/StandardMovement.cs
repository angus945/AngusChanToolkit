using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : MonoBehaviour, IMovementRequire
{
    [SerializeField] TarodevController.ScriptableStats _stats;

    IDecisionInput input;
    Vector3 IMovementRequire.VelocityModifier(IDecisionInput input, ICollision collision, MovementState state, Vector3 velocity)
    {
        this.input = input;

        HandleJump(input, collision, state);
        HandleDirection(input, collision, state);
        HandleGravity(input, collision, state);

        ClampJump(input, collision, state);

        return _frameVelocity;
    }

    Vector2 _frameVelocity;
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

    void HandleJump(IDecisionInput input, ICollision collision, MovementState state)
    {
        bool CanUseCoyote = _coyoteUsable && !collision.grounded && _time < collision.frameLeftGrounded + _stats.CoyoteTime;

        if (!_endedJumpEarly && !collision.grounded && !input.JumpKeep && state.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (collision.grounded || CanUseCoyote)
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
        }

        _jumpToConsume = false;
    }

    void HandleDirection(IDecisionInput input, ICollision collision, MovementState state)
    {
        if (input.MoveDirection.x == 0)
        {
            var deceleration = collision.grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, input.MoveDirection.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    void HandleGravity(IDecisionInput input, ICollision collision, MovementState state)
    {
        if (collision.grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    void ClampJump(IDecisionInput input, ICollision collision, MovementState state)
    {
        if (collision.ceilingHit)
        {
            Vector2 velocity = _frameVelocity;
            velocity.y = Mathf.Min(0, velocity.y);
            _frameVelocity = velocity;
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
