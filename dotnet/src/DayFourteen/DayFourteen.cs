using System.Text.RegularExpressions;

namespace AdventOfCode.DayFourteen
{
    public static class DayFourteen
    {
        private const int ITTERATIONS = 1000000;
        private const int GRID_WIDTH = 101;
        private const int GRID_HEIGHT = 103;
        private const int MID_X = GRID_WIDTH / 2;
        private const int MID_Y = GRID_HEIGHT / 2;

        public static void SolutionTwo()
        {
            var input = ReadInput();
            var grid = new List<Robot>[GRID_HEIGHT, GRID_WIDTH];

            for (int itt = 1; itt <= ITTERATIONS; itt++)
            {
                ClearGrid(grid);

                foreach (var robot in input.Robots.Values)
                {
                    robot.Iterate(1, GRID_WIDTH, GRID_HEIGHT);

                    if (grid[robot.Position.y, robot.Position.x] == null)
                        grid[robot.Position.y, robot.Position.x] = new List<Robot> { robot };
                    else
                        grid[robot.Position.y, robot.Position.x].Add(robot);
                }

                if (IsConsecutive(grid))
                {
                    Console.WriteLine($"Itteration: {itt}");
                    PrintGrid(itt, grid, GRID_WIDTH / 2, GRID_HEIGHT / 2);
                    return;
                }
            }
        }

        private static bool IsConsecutive(List<Robot>[,] grid)
        {
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                int consecutive = 0;
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    if (grid[y, x] != null)
                        consecutive++;
                    else
                        consecutive = 0;

                    if (consecutive > GRID_WIDTH / 10)
                        return true;
                }
            }

            return false; 
        }

        public static void SolutionOne()
        {
            var input = ReadInput();

            var grid = new List<Robot>[GRID_HEIGHT, GRID_WIDTH];
            ClearGrid(grid);

            int quadrant1 = 0;
            int quadrant2 = 0;
            int quadrant3 = 0;
            int quadrant4 = 0;

            foreach (var robot in input.Robots.Values)
            {
                robot.Iterate(ITTERATIONS, GRID_WIDTH, GRID_HEIGHT);

                if (robot.Position.x == MID_X || robot.Position.y == MID_Y)
                    continue;

                if (grid[robot.Position.y, robot.Position.x] == null)
                    grid[robot.Position.y, robot.Position.x] = new List<Robot> { robot };
                else
                    grid[robot.Position.y, robot.Position.x].Add(robot);

                if (robot.Position.x > MID_X && robot.Position.y < MID_Y)
                    quadrant1++;
                else if (robot.Position.x < MID_X && robot.Position.y < MID_Y)
                    quadrant2++;
                else if (robot.Position.x < MID_X && robot.Position.y > MID_Y)
                    quadrant3++;
                else if (robot.Position.x > MID_X && robot.Position.y > MID_Y)
                    quadrant4++;
            }

            PrintGrid(1, grid, MID_X, MID_Y);

            Console.WriteLine($"Quadrant 1: {quadrant2}");
            Console.WriteLine($"Quadrant 2: {quadrant1}");
            Console.WriteLine($"Quadrant 3: {quadrant3}");
            Console.WriteLine($"Quadrant 4: {quadrant4}");
            Console.WriteLine($"Result: {quadrant1 * quadrant2 * quadrant3 * quadrant4}");

        }

        private static void ClearGrid(List<Robot>?[,] grid)
        {
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    grid[y, x] = null;
                }
            }
        }

        private static void PrintGrid(int itt, List<Robot>[,] grid, int midX, int midY)
        {
            string outputFilePath = "output.txt";
            using (StreamWriter writer = new StreamWriter(outputFilePath, append: true)) 
            {
                writer.WriteLine($"Itt: {itt}");
                for (int y = 0; y < GRID_HEIGHT; y++)
                {
                    for (int x = 0; x < GRID_WIDTH; x++)
                    {
                        if (x == midX || y == midY)
                        {
                            writer.Write(" ");
                        }
                        else if (grid[y, x] != null)
                        {
                            writer.Write(new string("@").PadRight(3));
                        }
                        else
                        {
                            writer.Write(".  ");
                        }
                    }
                    writer.WriteLine();
                }
                writer.WriteLine();
            }
        }

        private static Input ReadInput()
            {
                var input = new Input();
                var fileLines = File.ReadAllLines("DayFourteen/input.txt");

                int robotId = 1;
                var regex = new Regex(@"p=(\d+),(\d+)\s+v=([-\d]+),([-\d]+)");

                foreach (var line in fileLines)
                {
                    var match = regex.Match(line.Trim());
                    if (match.Success)
                    {
                        var positionX = int.Parse(match.Groups[1].Value);
                        var positionY = int.Parse(match.Groups[2].Value);
                        var velocityX = int.Parse(match.Groups[3].Value);
                        var velocityY = int.Parse(match.Groups[4].Value);

                        var robot = new Robot
                        {
                            Id = robotId++,
                            Position = (positionX, positionY),
                            Velocity = (velocityX, velocityY)
                        };

                        input.Robots[robot.Id] = robot;
                    }
                }

                return input;
            }
        }
    }
