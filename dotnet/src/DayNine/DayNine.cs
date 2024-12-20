﻿namespace AdventOfCode.DayNine
{
    class WorkModel
    {
        public List<int> NormalizedInput = [];
        public Queue<int> EmptyIndexesQueue = new();
        public Stack<int> FileIndexesStack = new();
    }

    public static class DayNine
    {
        public static void SolutionTwo()
        {
            var workModel = GetWorkModel();

            var workFileIndex = -1;
            var nextFileIndex = -1;
            var leftOverEmptyIndexes = new Queue<int>();

            while (workModel.FileIndexesStack.Count > 0)
            {
                //File blocks
                var workFileBlock = new Stack<int>();
                do
                {
                    if (workModel.FileIndexesStack.Count > 0)
                        workFileIndex = workModel.FileIndexesStack.Pop();

                    if (workModel.FileIndexesStack.Count > 0)
                        nextFileIndex = workModel.FileIndexesStack.Peek();

                    workFileBlock.Push(workFileIndex);

                } while (workModel.NormalizedInput[workFileIndex] == workModel.NormalizedInput[nextFileIndex]);

                //Empty blocks
               // var currentEmptySpaceBlock = new Queue<int>();
               // do
               // {
               //     if (workModel.EmptyIndexesQueue.Count > 0)
               //         workFileIndex = workModel.EmptyIndexesQueue.Dequeue();
               //
               //     if (workModel.FileIndexesStack.Count > 0)
               //         nextFileIndex = workModel.EmptyIndexesQueue.Peek();
               //
               //     currentEmptySpaceBlock.Enqueue(workFileIndex);
               //
               //     foreach (var item in workModel.NormalizedInput)
               //     {
               //         var itemToWrite = item == -1 ? "." : item.ToString();
               //         Console.Write(itemToWrite + ' ');
               //     }
               //     Console.WriteLine();
               //
               // } while (workModel.EmptyIndexesQueue[workFileIndex] == workModel.EmptyIndexesQueue[nextFileIndex]);


            }
        }

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
