using System.Collections;
using System.Collections.Generic;
using AngusChanToolkit.Gameplay.Movement;
using UnityEngine;

public class FreeMovement : MonoBehaviour, IMovementRequire
{
    void IMovementRequire.VelocityModifier(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state)
    {
        state.currentVelocity.x = input.MoveDirection.x * properity.maxSpeed;
        state.currentVelocity.y = input.MoveDirection.y * properity.maxSpeed;
    }
}
