namespace AngusChanToolkit.Gameplay.Movement
{
    public interface ICollision
    {
        bool ceilingHit { get; }
        bool groundHit { get; }

        float frameLeftGrounded { get; }
        bool grounded { get; }

        void UpdateCollision(ImovementProperity properity);
    }
}