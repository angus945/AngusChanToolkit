using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : MonoBehaviour, IMovement
{
    [SerializeField] TarodevController.ScriptableStats _stats;

    Vector2 IMovement.frameVelocity { get => _frameVelocity; set => _frameVelocity = value; }

    Vector2 _frameVelocity;

    void FixedUpdate()
    {
        HandleJump();
        HandleDirection();
        HandleGravity();
    }

    //TODO Temp
    bool _grounded;
    float _time;
    float _frameLeftGrounded;
    IDecisionInput _frameInput;
    Rigidbody2D _rb;
    //TODO

    #region Jumping

    bool _jumpToConsume;
    bool _bufferedJumpUsable;
    bool _endedJumpEarly;
    bool _coyoteUsable;
    float _timeJumpWasPressed;

    bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _stats.JumpPower;
    }

    #endregion

    #region Horizontal

    void HandleDirection()
    {
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
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

    #endregion

}
