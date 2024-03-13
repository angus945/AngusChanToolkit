using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecisionInput
{
    bool JumpDown { get; }
    bool JumpHeld { get; }
    Vector2 Move { get; }
}
