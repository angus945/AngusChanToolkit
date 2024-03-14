using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollision
{
    bool ceilingHit { get; }
    bool groundHit { get; }

    float frameLeftGrounded { get; }
    bool grounded { get; }

    void UpdateCollision();
}
