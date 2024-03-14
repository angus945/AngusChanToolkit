namespace AngusChanToolkit.Gameplay.Movement
{
    public interface IMovementRequire
    {
        void VelocityModifier(ImovementProperity properity, IDecisionInput input, ICollision collision, MovementStatus state);
    }
}