using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngusChanToolkit.Gameplay.Movement;

public class MovementApplyer : MonoBehaviour, IMovementApplyer
{
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void IMovementApplyer.ApplyMovement(MovementStatus status)
    {
        status.lastVelocity = new Vector(_rb.velocity.x, _rb.velocity.y);
        _rb.velocity = new Vector2(status.currentVelocity.x, status.currentVelocity.y);

        //flip by velocity, use rotation to flip
        if (status.currentVelocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (status.currentVelocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
