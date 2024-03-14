using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IDecisionInput
{
    bool IDecisionInput.Jump { get => jumpDpwn; }
    bool IDecisionInput.JumpKeep { get => jumpHeld; }
    Vector3 IDecisionInput.MoveDirection { get => move; }

    bool jumpDpwn;
    bool jumpHeld;
    Vector3 move;

    void Update()
    {
        jumpDpwn = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C);
        jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C);
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
