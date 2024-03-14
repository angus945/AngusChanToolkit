using System;
using TarodevController;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlatformController : MonoBehaviour
{
    ImovementProperity properity;
    IDecisionInput input;
    ICollision collision;
    IMovementRequire[] movements;
    IMovementApplyer applier;

    MovementStatus state = new MovementStatus();

    void Awake()
    {
        properity = GetComponent<ImovementProperity>();
        input = GetComponent<IDecisionInput>();
        collision = GetComponent<ICollision>();
        movements = GetComponents<IMovementRequire>();
        applier = GetComponent<IMovementApplyer>();
    }
    void FixedUpdate()
    {
        collision.UpdateCollision();

        for (int i = 0; i < movements.Length; i++)
        {
            movements[i].VelocityModifier(properity, input, collision, state);
        }

        applier.ApplyMovement(state);
    }
}