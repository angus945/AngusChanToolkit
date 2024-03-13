using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IDecisionInput
{
    bool IDecisionInput.JumpDown { get => jumpDpwn; }
    bool IDecisionInput.JumpHeld { get => jumpHeld; }
    Vector2 IDecisionInput.Move { get => move; }

    [SerializeField] private TarodevController.ScriptableStats _stats;

    bool jumpDpwn;
    bool jumpHeld;
    Vector2 move;

    void Update()
    {
        jumpDpwn = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C);
        jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C);
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_stats.SnapInput)
        {
            move.x = Mathf.Abs(move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(move.x);
            move.y = Mathf.Abs(move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(move.y);
        }
    }
}
