using System;
using TarodevController;
using UnityEngine;
using AngusChanToolkit.Gameplay.Movement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlatformController : MonoBehaviour
{
    MovementController controller;

    void Awake()
    {
        ImovementProperity properity = GetComponent<ImovementProperity>();
        IDecisionInput input = GetComponent<IDecisionInput>();
        ICollision collision = GetComponent<ICollision>();
        IMovementRequire[] movements = GetComponents<IMovementRequire>();
        IMovementApplyer applier = GetComponent<IMovementApplyer>();

        controller = new MovementController(properity, input, collision, movements, applier);
    }
    void FixedUpdate()
    {
        controller.Update();
    }
}