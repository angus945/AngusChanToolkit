using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement_Horizontal : MonoBehaviour, IMovementRequire
{
    [SerializeField] TarodevController.ScriptableStats _stats;

    void IMovementRequire.VelocityModifier(IDecisionInput input, ICollision collision, MovementStatus state)
    {
        HandleDirection(input, collision, state);
    }

    void HandleDirection(IDecisionInput input, ICollision collision, MovementStatus state)
    {
        if (input.MoveDirection.x == 0)
        {
            var deceleration = collision.grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            state.currentVelocity.x = Mathf.MoveTowards(state.currentVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            state.currentVelocity.x = Mathf.MoveTowards(state.currentVelocity.x, input.MoveDirection.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }
}
