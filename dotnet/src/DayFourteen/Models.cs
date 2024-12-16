namespace AdventOfCode.DayFourteen
{

    public class Robot
    {
        public int Id { get; set; }
        public (int x, int y) Position { get; set; }
        public (int x, int y) Velocity { get; set; }

        public void Iterate(int countOfIterations, int gridWidth, int gridHeight)
        {
            int newX = (Position.x + Velocity.x * countOfIterations) % gridWidth;
            int newY = (Position.y + Velocity.y * countOfIterations) % gridHeight;

            if (newX < 0) newX += gridWidth;
            if (newY < 0) newY += gridHeight;

            this.Position = (newX, newY);
        }
    }

    public class Input
    {
        public Dictionary<int, Robot> Robots { get; set; } = new Dictionary<int, Robot>();
    }
}