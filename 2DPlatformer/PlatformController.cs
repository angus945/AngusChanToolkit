using System;
using TarodevController;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlatformController : MonoBehaviour
{
    [SerializeField] ScriptableStats _stats;

    IDecisionInput input;
    ICollision collision;
    IMovementRequire[] movements;
    IMovementApplyer applier;

    MovementStatus state = new MovementStatus();

    void Awake()
    {
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
            movements[i].VelocityModifier(input, collision, state);
        }

        applier.ApplyMovement(state);
    }
}