namespace AOC06A;

public static class InputParser
{
    public static IEnumerable<SignalStream> ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No signals!");
        }

        for (int i = 0; i < input.Length; i++)
        {
            yield return new SignalStream(input[i]);
        }
    }
}
