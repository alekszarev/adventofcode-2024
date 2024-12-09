namespace AdventOfCode.DayNine
{
    class WorkModel
    {
        public List<int> NormalizedInput = [];
        public Queue<int> EmptyIndexesQueue = new();
        public Stack<int> FileIndexesStack = new();
    }

    public static class DayNine
    {
        public static void SolutionOne()
        {
            var workModel = GetWorkModel();
            ulong currentChanges = 0;
            while (workModel.EmptyIndexesQueue.Count > 0)
            {
                var emptySpaceIndex = workModel.EmptyIndexesQueue.Dequeue();
                var fileItemIndex = workModel.FileIndexesStack.Pop();
                if (emptySpaceIndex > fileItemIndex)
                    break;

                workModel.NormalizedInput[emptySpaceIndex] = workModel.NormalizedInput[fileItemIndex];
                workModel.NormalizedInput[fileItemIndex] = -1;
                currentChanges++;
            }

            foreach (var item in workModel.NormalizedInput)
            {
                Console.Write(item.ToString() + ' ');
            }
            Console.WriteLine();

            ulong checksum = 0;
            for (var i = 0; i < workModel.NormalizedInput.Count; i++)
            {
                var item = workModel.NormalizedInput[i];
                if (item == -1)
                    break;

                var sum = i * workModel.NormalizedInput[i];
                checksum += (ulong)(i * workModel.NormalizedInput[i]);
                Console.WriteLine($"{i} * {workModel.NormalizedInput[i]} = {sum}");
            }

            Console.WriteLine(checksum);
            Console.WriteLine(workModel.NormalizedInput.Count);
        }

        private static WorkModel GetWorkModel()
        {
            var input = File.ReadAllText("DayNine/input.txt");
            var workModel = new WorkModel();

            int fileId = 0;
            var currentIndex = 0;
            for (int i = 0; i < input?.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (int c = 0; c < int.Parse(input[i].ToString()); c++)
                    {
                        workModel.NormalizedInput.Add(fileId);
                        Console.WriteLine($"stack: {currentIndex}");
                        workModel.FileIndexesStack.Push(currentIndex++);
                    }
                    fileId++;

                }
                else
                {
                    for (int c = 0; c < int.Parse(input[i].ToString()); c++)
                    {

                        Console.WriteLine($"queue: {currentIndex}");
                        workModel.NormalizedInput.Add(-1);
                        workModel.EmptyIndexesQueue.Enqueue(currentIndex++);
                    }

                }
            }

            return workModel;
        }
    }
}
