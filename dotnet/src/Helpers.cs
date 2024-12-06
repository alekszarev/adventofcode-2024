
public static class Helpers
{
    public static List<char[]> GetCharMatrixInput()
    {
        Console.WriteLine("Enter input. Empty line finishes the input.");

        var matrix = new List<char[]>();
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                break;

            matrix.Add(input.ToCharArray());
        }

        return matrix;
    }
}

