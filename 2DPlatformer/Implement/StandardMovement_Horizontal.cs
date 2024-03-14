using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement_Horizontal : MonoBehaviour, IMovementRequire
{
    void IMovementRequire.VelocityModifier(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state)
    {
        if (input.MoveDirection.x == 0)
        {
            float deceleration = collision.grounded ? properity.groundDeceleration : properity.airDeceleration;
            state.currentVelocity.x = Mathf.MoveTowards(state.currentVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            state.currentVelocity.x = Mathf.MoveTowards(state.currentVelocity.x, input.MoveDirection.x * properity.maxSpeed, properity.acceleration * Time.fixedDeltaTime);
        }
    }
}
