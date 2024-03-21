using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngusChanToolkit.Gameplay.Movement;

public class StandardCollision : MonoBehaviour, ICollision
{
    [SerializeField] LayerMask ignoreLayer;

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

    void ICollision.UpdateCollision(ImovementProperity properity)
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        _groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, properity.grounderDistance, ~ignoreLayer);
        _ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, properity.grounderDistance, ~ignoreLayer);


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
