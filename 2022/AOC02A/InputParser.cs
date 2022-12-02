namespace AOC02A;

public static class InputParser
{
    public static Round[] ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No rounds!");
        }

        return input
            .Select(line => new Round(ItemType.MyPlay(line[2]), ItemType.TheirPlay(line[0])))
            .ToArray();
    }

    public static Round[] ParseInput2(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No rounds!");
        }

        return input
            .Select(line =>
            {
                var theirPlay = ItemType.TheirPlay(line[0]);
                var myOutcome = ItemType.MyOutcome(line[2]);
                return new Round(Outcome.GetItem(theirPlay, myOutcome), theirPlay, myOutcome);
            })
            .ToArray();
    }
}
