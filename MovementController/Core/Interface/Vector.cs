namespace AngusChanToolkit.Gameplay.Movement
{
    public struct Vector
    {
        public float x;
        public float y;
        public float z;

        public Vector(float x, float y) : this()
        {
            this.x = x;
            this.y = y;
        }
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}