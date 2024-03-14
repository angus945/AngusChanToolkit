using UnityEngine;

public class MovementProperity : MonoBehaviour, ImovementProperity
{
    [field: SerializeField] public float maxSpeed { get; set; }
    [field: SerializeField] public float acceleration { get; set; }
    [field: SerializeField] public float groundDeceleration { get; set; }
    [field: SerializeField] public float airDeceleration { get; set; }
    [field: SerializeField] public float groundingForce { get; set; }
    [field: SerializeField] public float grounderDistance { get; set; }

    [field: SerializeField] public float jumpPower { get; set; }
    [field: SerializeField] public float maxFallSpeed { get; set; }
    [field: SerializeField] public float fallAcceleration { get; set; }
    [field: SerializeField] public float jumpEndEarlyGravityModifier { get; set; }
    [field: SerializeField] public float coyoteTime { get; set; }
    [field: SerializeField] public float jumpBuffer { get; set; }



}
