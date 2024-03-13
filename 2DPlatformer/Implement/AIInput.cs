using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class AIInput : MonoBehaviour, IDecisionInput
{
    bool IDecisionInput.JumpDown { get => jumpDpwn; }
    bool IDecisionInput.JumpHeld { get => jumpHeld; }
    Vector2 IDecisionInput.Move { get => move; }

    bool jumpDpwn;
    bool jumpHeld;
    Vector2 move;

    float moveTimer;
    float jumpTimer;

    void Start()
    {
        move = Vector2.left;
    }
    void Update()
    {
        moveTimer += Time.deltaTime;
        jumpTimer += Time.deltaTime;

        if (moveTimer > 3)
        {
            move *= -1f;

            moveTimer = 0;
        }

        if (jumpTimer > 2)
        {
            jumpDpwn = true;
            jumpTimer = 0;
        }
        else
        {
            jumpDpwn = false;
        }

    }
}
