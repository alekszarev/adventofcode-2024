namespace adventofcode.DayTwo
{
    public static class DayTwo
    {
        public static void SolutionTwo()
        {
            var safe = 0;
            var unSafe = 0;
            Console.WriteLine("Enter input. Empty line finishes the input.");

            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                    break;

                var magicNumbers = input.Split(' ').Select(int.Parse).ToList();
                var isSafe = IsSafe(magicNumbers);

                if (isSafe)
                    safe++;
                else
                    unSafe++;
            }

            Console.WriteLine($"Safe: {safe}, Unsafe: {unSafe}");
        }

        static bool IsSafe(List<int> report)
        {
            for (int i = 0; i < report.Count; i++)
            {
                var modifiedReport = report.Where((x, index) => index != i).ToList();
                if (IsOneDirection(modifiedReport))
                    return true;

            }

            return false;
        }

        static bool IsOneDirection(List<int> report)
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                var rest = report[i + 1] - report[i];
                if (Math.Abs(rest) > 3 || rest == 0)
                    return false;

                if (i > 0)
                {
                    var prevRest = report[i] - report[i - 1];
                    if (rest * prevRest < 0)
                        return false;
                }
            }
            return true;
        }
    }

}