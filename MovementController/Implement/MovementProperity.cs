using UnityEngine;
using AngusChanToolkit.Gameplay.Movement;

public class MovementProperity : MonoBehaviour, ImovementProperity
{
    [field: SerializeField] public float maxSpeed { get; set; } = 14;
    [field: SerializeField] public float acceleration { get; set; } = 120;
    [field: SerializeField] public float groundDeceleration { get; set; } = 60;
    [field: SerializeField] public float airDeceleration { get; set; } = 30;
    [field: SerializeField] public float groundingForce { get; set; } = -1.5f;
    [field: SerializeField] public float grounderDistance { get; set; } = 0.05f;

    [field: SerializeField] public float jumpPower { get; set; } = 36f;
    [field: SerializeField] public float maxFallSpeed { get; set; } = 40f;
    [field: SerializeField] public float fallAcceleration { get; set; } = 110f;
    [field: SerializeField] public float jumpEndEarlyGravityModifier { get; set; } = 3f;
    [field: SerializeField] public float coyoteTime { get; set; } = 0.15f;
    [field: SerializeField] public float jumpBuffer { get; set; } = 0.2f;



}
