using AngusChanToolkit.Gameplay.Movement;
using UnityEngine;

public class AIInput : MonoBehaviour, IDecisionInput
{
    bool IDecisionInput.Jump { get => jumpDpwn; }
    bool IDecisionInput.JumpKeep { get => jumpHeld; }
    Vector IDecisionInput.MoveDirection { get => new Vector(move.x, move.y); }

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
