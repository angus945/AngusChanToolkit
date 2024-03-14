using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecisionInput
{
    bool Jump { get; }
    bool JumpKeep { get; }
    Vector3 MoveDirection { get; }
}
