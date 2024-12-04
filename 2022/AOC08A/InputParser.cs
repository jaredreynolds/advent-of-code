namespace AOC08A;

public static class InputParser
{
    public static Map ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No input!");
            return new Map(0, 0, Array.Empty<Tree>());
        }

        var width = input[0].Length;
        var height = input.Length;

        return new Map(width, height, GetTrees(input, width, height));
    }

    private static IEnumerable<Tree> GetTrees(string[] input, int width, int height)
    {
        int y = height - 1;
        foreach (var line in input)
        {
            for (int x = 0; x < width; x++)
            {
                yield return new(line[x] - '0', x, y);
            }

            --y;
        }
    }
}
