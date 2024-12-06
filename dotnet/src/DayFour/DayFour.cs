namespace AdventOfCode.DayFour
{
    public static class DayFour
    {
        private const string WORD_TO_FIND_SOLUTION_ONE = "XMAS";
        private static readonly int[,] ALL_DIRECTIONS_SOLUTION_ONE =
        {
            { 0, 1 },
            { 0, -1 },
            { 1, 0 },
            { -1, 0 },
            { 1, 1 },
            { 1, -1 },
            { -1, 1 },
            { -1, -1 }
        };

        private static readonly int[][][] X_INDICES_PATTERN_SOLUTION_TWO =
        {
            [
                [-1, -1],
                [1, -1]
            ],
            [
                [1, 1],
                [-1, 1]
            ]
        };



        public static void SolutionOne()
        {
            var matrixInput = Helpers.GetCharMatrixInput();
            var totalCount = FindOccurencesSolutionOne(matrixInput);
            Console.WriteLine($"Found: {totalCount} of {WORD_TO_FIND_SOLUTION_ONE}");
        }

        public static void SolutionTwo()
        {
            var matrixInput = Helpers.GetCharMatrixInput();
            var totalCount = FindOccurencesSolutionTwo(matrixInput);
            Console.WriteLine($"Found: {totalCount} of X-MAS");


            int rows = matrixInput.Count;
            int cols = matrixInput[0].Length;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrixInput[i][j]);
                }
                Console.WriteLine();
            }
        }

        private static bool IsWithinBounds(int y, int x, int rows, int cols)
        {
            return y >= 0 && y < rows && x >= 0 && x < cols;
        }

        private static int FindOccurencesSolutionTwo(List<char[]> matrix)
        {
            var occurrences = 0;
            int rows = matrix.Count;
            int cols = matrix[0].Length;

            for (int y = 1; y < rows - 1; y++)
            {
                for (int x = 1; x < cols - 1; x++)
                {
                    if (matrix[y][x] == 'A')
                    {
                        {
                            var leftDiagonalUp = matrix[y + X_INDICES_PATTERN_SOLUTION_TWO[0][0][0]][x + X_INDICES_PATTERN_SOLUTION_TWO[0][0][1]];
                            var rightDiagonalUp = matrix[y + X_INDICES_PATTERN_SOLUTION_TWO[1][1][0]][x + X_INDICES_PATTERN_SOLUTION_TWO[1][1][1]];

                            var leftDiagonalDown = matrix[y + X_INDICES_PATTERN_SOLUTION_TWO[0][1][0]][x + X_INDICES_PATTERN_SOLUTION_TWO[0][1][1]];
                            var rightDiagonalDown = matrix[y + X_INDICES_PATTERN_SOLUTION_TWO[1][0][0]][x + X_INDICES_PATTERN_SOLUTION_TWO[1][0][1]];

                            if ((leftDiagonalUp == 'M' && rightDiagonalDown == 'S') || (leftDiagonalUp == 'S' && rightDiagonalDown == 'M'))
                            {
                                if ((rightDiagonalUp == 'M' && leftDiagonalDown == 'S') || (rightDiagonalUp == 'S' && leftDiagonalDown == 'M'))
                                {
                                    occurrences++;
                                }
                            }
                        }
                    }
                }
            }

            return occurrences;
        }

        private static int FindOccurencesSolutionOne(List<char[]> matrix)
        {
            var occurrences = 0;
            int rows = matrix.Count;
            int cols = matrix[0].Length;

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    for (int d = 0; d < ALL_DIRECTIONS_SOLUTION_ONE.GetLength(0); d++)
                    {
                        if (IsMatchSolutionOne(x, y, d, matrix, rows, cols))
                            occurrences++;
                    }
                }
            }

            return occurrences;
        }

        private static bool IsMatchSolutionOne(int x, int y, int d, List<char[]> matrix, int rows, int cols)
        {
            for (int i = 0; i < WORD_TO_FIND_SOLUTION_ONE.Length; i++)
            {
                int dx = x + i * ALL_DIRECTIONS_SOLUTION_ONE[d, 0];
                int dy = y + i * ALL_DIRECTIONS_SOLUTION_ONE[d, 1];

                if (dx < 0
                    || dy < 0
                    || dx >= rows
                    || dy >= cols
                    || matrix[dx][dy] != WORD_TO_FIND_SOLUTION_ONE[i])
                    return false;
            }

            return true;
        }
    }
}
