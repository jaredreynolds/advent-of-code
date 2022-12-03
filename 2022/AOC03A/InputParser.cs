namespace AOC03A;

public static class InputParser
{
    public static Rucksack[] ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No rucksacks!");
        }

        return input
            .Select((line, i) => new Rucksack(line, i))
            .ToArray();
    }
}
