
namespace AngusChanToolkit.Gameplay.Movement
{
    public class MovementController
    {
        ImovementProperity properity;
        IDecisionInput input;
        ICollision collision;
        IMovementRequire[] movements;
        IMovementApplyer applier;

        MovementStatus status = new MovementStatus();

        public MovementController(ImovementProperity properity, IDecisionInput input, ICollision collision, IMovementRequire[] movements, IMovementApplyer applier)
        {
            this.properity = properity;
            this.input = input;
            this.collision = collision;
            this.movements = movements;
            this.applier = applier;
        }
        public void Update()
        {
            collision.UpdateCollision(properity);

            for (int i = 0; i < movements.Length; i++)
            {
                movements[i].VelocityModifier(properity, input, collision, status);
            }

            applier.ApplyMovement(status);
        }
    }
}
