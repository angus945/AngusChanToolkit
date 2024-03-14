using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementApplyer : MonoBehaviour, IMovementApplyer
{
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void IMovementApplyer.ApplyMovement(MovementStatus status)
    {
        status.lastVelocity = _rb.velocity;
        _rb.velocity = status.currentVelocity;
    }
}
