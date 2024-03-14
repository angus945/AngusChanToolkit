using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementRequire
{
    Vector3 VelocityModifier(IDecisionInput input, ICollision collision, MovementState state, Vector3 velocity);
    // Vector2 frameVelocity { get; set; }

}
