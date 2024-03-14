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

    void Awake()
    {
        input = GetComponent<IDecisionInput>();
        collision = GetComponent<ICollision>();
        movements = GetComponents<IMovementRequire>();
        applier = GetComponent<IMovementApplyer>();
    }

    // void Update()
    // {
    //     _time += Time.deltaTime;
    //     GatherInput();
    // }


    MovementState state = new MovementState();

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;

        collision.UpdateCollision();

        for (int i = 0; i < movements.Length; i++)
        {
            velocity = movements[i].VelocityModifier(input, collision, state, velocity);
        }

        applier.ApplyMovement(velocity);

        state.velocity = velocity;
    }
}