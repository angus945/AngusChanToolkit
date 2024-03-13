using System;
using TarodevController;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlatformController : MonoBehaviour
{
    [SerializeField] ScriptableStats _stats;
    Rigidbody2D _rb;
    CapsuleCollider2D _col;

    IDecisionInput _frameInput;
    IMovement _movement;

    bool _cachedQueryStartInColliders;

    float _time;

    //TODO
    bool _bufferedJumpUsable;
    bool _endedJumpEarly;
    bool _coyoteUsable;
    bool _jumpToConsume;
    float _timeJumpWasPressed;
    //TODO

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();

        _frameInput = GetComponent<IDecisionInput>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }

    void GatherInput()
    {
        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    void FixedUpdate()
    {
        CheckCollisions();

        ApplyMovement();
    }

    #region Collisions

    float _frameLeftGrounded = float.MinValue;
    bool _grounded;

    void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

        // Hit a Ceiling
        if (ceilingHit)
        {
            Vector2 velocity = _movement.frameVelocity;
            velocity.y = Mathf.Min(0, velocity.y);
            _movement.frameVelocity = velocity;
        }

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    #endregion


    // #region Jumping

    // bool _jumpToConsume;
    // bool _bufferedJumpUsable;
    // bool _endedJumpEarly;
    // bool _coyoteUsable;
    // float _timeJumpWasPressed;

    // bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    // bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    // void HandleJump()
    // {
    //     if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

    //     if (!_jumpToConsume && !HasBufferedJump) return;

    //     if (_grounded || CanUseCoyote) ExecuteJump();

    //     _jumpToConsume = false;
    // }

    // void ExecuteJump()
    // {
    //     _endedJumpEarly = false;
    //     _timeJumpWasPressed = 0;
    //     _bufferedJumpUsable = false;
    //     _coyoteUsable = false;
    //     _frameVelocity.y = _stats.JumpPower;
    // }

    // #endregion

    // #region Horizontal

    // void HandleDirection()
    // {
    //     if (_frameInput.Move.x == 0)
    //     {
    //         var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
    //         _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
    //     }
    //     else
    //     {
    //         _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
    //     }
    // }

    // #endregion

    // #region Gravity

    // void HandleGravity()
    // {
    //     if (_grounded && _frameVelocity.y <= 0f)
    //     {
    //         _frameVelocity.y = _stats.GroundingForce;
    //     }
    //     else
    //     {
    //         var inAirGravity = _stats.FallAcceleration;
    //         if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
    //         _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
    //     }
    // }

    // #endregion

    void ApplyMovement() => _rb.velocity = _movement.frameVelocity;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}