public interface IUIDynamicTrack
{
    float PositionX { get; }
    float PositionY { get; }
    float PositionZ { get; }

    float MaxValue { get; }
    float CurrentValue { get; }
}