using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardCollision : MonoBehaviour, ICollision
{
    [SerializeField] TarodevController.ScriptableStats _stats;

    float ICollision.frameLeftGrounded { get => _frameLeftGrounded; }
    bool ICollision.grounded { get => _grounded; }
    bool ICollision.ceilingHit { get => _ceilingHit; }
    bool ICollision.groundHit { get => _groundHit; }

    CapsuleCollider2D _col;

    float _frameLeftGrounded = float.MinValue;
    bool _grounded;
    bool _cachedQueryStartInColliders;
    bool _groundHit;
    bool _ceilingHit;

    float _time;

    void Awake()
    {
        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

        _col = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        _time += Time.deltaTime;
    }

    void ICollision.UpdateCollision()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        _groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
        _ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

        // Hit a Ceiling
        // if (ceilingHit)
        // {
        //     Vector2 velocity = _movement.frameVelocity;
        //     velocity.y = Mathf.Min(0, velocity.y);
        //     _movement.frameVelocity = velocity;
        // }

        // Landed on the Ground
        if (!_grounded && _groundHit)
        {
            _grounded = true;
        }
        else if (_grounded && !_groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }
}
