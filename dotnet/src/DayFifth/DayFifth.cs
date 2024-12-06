namespace AdventOfCode.DayFifth
{
    //TODO: Try to implement linkedList graph
    public static class DayFifth
    {
        public static int SolutionOne()
        {
            return ProcessSolution(isOrdered: true);
        }

        public static int SolutionTwo()
        {
            return ProcessSolution(isOrdered: false);
        }

        private static int ProcessSolution(bool isOrdered)
        {
            Console.WriteLine("Enter rules. Empty line finishes rules input");
            var rulesInput = GetRulesInputByDelimiter('|');

            var orderedRules = new Dictionary<int, List<int>>();
            foreach (var rule in rulesInput)
            {
                var from = rule.Item1;
                var to = rule.Item2;

                if (!orderedRules.ContainsKey(from))
                    orderedRules[from] = new List<int>();
                orderedRules[from].Add(to);
            }

            Console.WriteLine("Enter pages. Empty line finishes input");
            var pagesInput = GetPagesInputByDelimiter(',');

            var updates = new List<List<int>>();
            foreach (var update in pagesInput)
            {
                if (isOrdered)
                {
                    if (IsOrdered(update, orderedRules))
                        updates.Add(update);
                }
                else
                {
                    if (!IsOrdered(update, orderedRules))
                        updates.Add(FixOrder(update, orderedRules));
                }
            }

            var middlePagesSum = updates
                .Select(update => update[update.Count / 2])
                .Sum();

            Console.WriteLine("Result: " + middlePagesSum);

            return middlePagesSum;
        }

        private static bool IsOrdered(List<int> update, Dictionary<int, List<int>> rules)
        {
            var positions = update
                .Select((page, index) => (page, index))
                .ToDictionary(x => x.page, x => x.index);

            foreach (var rule in rules)
            {
                if (!positions.ContainsKey(rule.Key))
                    continue;

                foreach (var to in rule.Value)
                {
                    if (!positions.ContainsKey(to))
                        continue;

                    if (positions[rule.Key] >= positions[to])
                        return false;
                }
            }

            return true;
        }

        private static List<int> FixOrder(List<int> update, Dictionary<int, List<int>> rules)
        {
            var result = update.ToList();

            result.Sort((a, b) =>
            {
                if (rules.TryGetValue(a, out List<int>? valueA) && valueA.Contains(b))
                    return -1;

                if (rules.TryGetValue(b, out List<int>? valueB) && valueB.Contains(a))
                    return 1;

                return a.CompareTo(b);
            });

            return result;
        }

        private static List<(int, int)> GetRulesInputByDelimiter(char delimiter)
        {
            var result = new List<(int, int)>();
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;

                var parts = input.Split(delimiter).Select(int.Parse).ToArray();
                if (parts.Length == 2)
                    result.Add((parts[0], parts[1]));
            }

            return result;
        }

        private static List<List<int>> GetPagesInputByDelimiter(char delimiter)
        {
            var result = new List<List<int>>();
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;

                result.Add(input.Split(delimiter).Select(int.Parse).ToList());
            }

            return result;
        }
    }
}
