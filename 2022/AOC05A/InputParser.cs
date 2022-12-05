namespace AOC05A;

public static class InputParser
{
    public static (Dock dock, Move[]) ParseInput(string[] input)
    {
        if (!input.Any())
        {
            Console.WriteLine("No elves!");
        }

        Dock dock = new();
        int crateLineBreak;

        for (crateLineBreak = 0; crateLineBreak < input.Length && input[crateLineBreak][1] != '1'; crateLineBreak++) { }

        int i;
        for (i = crateLineBreak - 1; i >= 0; i--)
        {
            var line = input[i];
            int charIndex;

            for (int stackIndex = 0; (charIndex = (stackIndex * 4)) < line.Length; stackIndex++)
            {
                if (stackIndex >= dock.Stacks.Count)
                {
                    dock.Stacks.Add(new());
                }

                var crate = line[charIndex + 1];
                if (crate == ' ') continue;

                dock.Stacks[stackIndex].Push(crate);
            }
        }

        List<Move> moves = new();
        for (i = crateLineBreak + 2; i < input.Length; i++)
        {
            var ops = input[i].Split(' ');
            moves.Add(new Move()
            {
                Count = int.Parse(ops[1]),
                Source = int.Parse(ops[3]),
                Destination = int.Parse(ops[5])
            });
        }

        return (dock, moves.ToArray());
    }
}
