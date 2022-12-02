namespace AOC01A;

public static class InputParser
{
    public static List<Elf> ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No elves!");
            return new List<Elf>();
        }

        Elf currentElf = new(1);
        var elves = new List<Elf>
        {
            currentElf
        };

        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                elves.Add(currentElf = new Elf(elves.Count + 1));
                continue;
            }

            currentElf.FoodItems.Add(new Food(int.Parse(input[i])));
        }

        return elves;
    }
}
