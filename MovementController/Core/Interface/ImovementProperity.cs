namespace AngusChanToolkit.Gameplay.Movement
{
    public interface ImovementProperity
    {
        //Movement
        float maxSpeed { get; }
        float acceleration { get; }
        float groundDeceleration { get; }
        float airDeceleration { get; }
        float groundingForce { get; }
        float grounderDistance { get; }

        //Jumping
        float jumpPower { get; }
        float maxFallSpeed { get; }
        float fallAcceleration { get; }
        float jumpEndEarlyGravityModifier { get; }
        float coyoteTime { get; }
        float jumpBuffer { get; }

    }
}