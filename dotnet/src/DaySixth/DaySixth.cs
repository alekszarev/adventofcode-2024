namespace AdventOfCode.DaySixth
{
    public static class DaySixth
    {
        private static readonly (int x, int y) UP_INDEX_DIRECTION = (-1, 0);
        private static readonly (int x, int y) RIGHT_INDEX_DIRECTION = (0, 1);
        private static readonly (int x, int y) DOWN_INDEX_DIRECTION = (1, 0);
        private static readonly (int x, int y) LEFT_INDEX_DIRECTION = (0, -1);
        private const char OBSTRUCTION = '#';
        private const char MARKED_PLACE_SOLUTION_ONE = 'X';

        enum Directions
        {
            UpDirection = '^',
            RightDirection = '>',
            DownDirection = 'v',
            LeftDirection = '<',
        }

        public static void SolutionTwo()
        {
            var loops = 0;
            var matrix = Helpers.GetCharMatrixInput();
            var currentGuardPosition = FindGuradPosition(matrix);

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == '.')
                    {
                        matrix[i][j] = OBSTRUCTION;
                        if (IsLoop(matrix, currentGuardPosition))
                            loops++;

                        matrix[i][j] = '.';
                    }
                }
            }

            Console.WriteLine($"Total loops: {loops}");
        }

        private static bool IsLoop(List<char[]> matrix, (int x, int y, Directions direction) currentGuardPosition)
        {
            var visited = new HashSet<(int x, int y, Directions direction)>();
            while (true)
            {
                if (visited.Contains(currentGuardPosition))
                    return true;

                visited.Add(currentGuardPosition);

                var (x, y) = GetDirectionIndex(currentGuardPosition.direction);
                var nextX = currentGuardPosition.x + x;
                var nextY = currentGuardPosition.y + y;

                if (nextX < 0 || nextX >= matrix.Count || nextY < 0 || nextY >= matrix[0].Length)
                    return false; 

                var nextCell = matrix[nextX][nextY];
                if (nextCell == OBSTRUCTION)
                {
                    currentGuardPosition.direction = SwitchDirection(currentGuardPosition.direction);
                }
                else
                {
                    currentGuardPosition.x = nextX;
                    currentGuardPosition.y = nextY;
                }
            }
        }

        public static void SolutionOne()
        {
            var positionsVisited = 0;

            var matrix = Helpers.GetCharMatrixInput();
            var currentGuardPosition = FindGuradPosition(matrix);

            while (true)
            {
                var (x, y) = GetDirectionIndex(currentGuardPosition.direction);
                var nextX = currentGuardPosition.x + x;
                var nextY = currentGuardPosition.y + y;

                if (nextX < 0 || nextX >= matrix.Count || nextY < 0 || nextY >= matrix[0].Length)
                    break;

                var nextCell = matrix[nextX][nextY];
                if (nextCell == OBSTRUCTION)
                {
                    currentGuardPosition.direction = SwitchDirection(currentGuardPosition.direction);
                }
                else
                {
                    if (matrix[nextX][nextY] != MARKED_PLACE_SOLUTION_ONE)
                        positionsVisited++;

                    matrix[nextX][nextY] = MARKED_PLACE_SOLUTION_ONE;
                    currentGuardPosition.x = nextX;
                    currentGuardPosition.y = nextY;
                }
            }

            Console.WriteLine("Map after route");
            foreach (var row in matrix)
            {
                Console.WriteLine(row);
            }

            Console.WriteLine($"Positions: {positionsVisited}");
        }

        private static (int x, int y, Directions direction) FindGuradPosition(List<char[]> matrix)
        {
            var currentGuardPosition = (x: 0, y: 0, direction: Directions.UpDirection);
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (Enum.IsDefined(typeof(Directions), (int)matrix[i][j]))
                    {
                        currentGuardPosition.x = i;
                        currentGuardPosition.y = j;
                        currentGuardPosition.direction = (Directions)matrix[i][j];
                        break;
                    }
                }
            }

            return currentGuardPosition;
        }

        private static (int x, int y) GetDirectionIndex(Directions direction)
        {
            return direction switch
            {
                Directions.UpDirection => UP_INDEX_DIRECTION,
                Directions.RightDirection => RIGHT_INDEX_DIRECTION,
                Directions.DownDirection => DOWN_INDEX_DIRECTION,
                Directions.LeftDirection => LEFT_INDEX_DIRECTION,
                _ => throw new InvalidOperationException()
            };
        }

        private static Directions SwitchDirection(Directions currentDirection)
        {
            return currentDirection switch
            {
                Directions.UpDirection => Directions.RightDirection,
                Directions.RightDirection => Directions.DownDirection,
                Directions.DownDirection => Directions.LeftDirection,
                Directions.LeftDirection => Directions.UpDirection,
                _ => throw new InvalidOperationException()
            };
        }
    }
}
