namespace AOC04A;

public static class InputParser
{
    public static AssignmentPair[] ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No elves!");
        }

        return input
            .Select((line, i) =>
            {
                var assignments = line.Split(',').ToArray();
                return new AssignmentPair(new[]
                {
                    new Elf($"{i}A", assignments[0]),
                    new Elf($"{i}B", assignments[1])
                });
            })
            .ToArray();
    }
}
