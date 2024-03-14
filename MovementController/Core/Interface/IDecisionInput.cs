namespace AngusChanToolkit.Gameplay.Movement
{

    public interface IDecisionInput
    {
        bool Jump { get; }
        bool JumpKeep { get; }

        Vector MoveDirection { get; }
    }
}