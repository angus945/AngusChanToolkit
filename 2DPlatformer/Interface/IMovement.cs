using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementRequire
{
    void VelocityModifier(IDecisionInput input, ICollision collision, MovementStatus state);
}
