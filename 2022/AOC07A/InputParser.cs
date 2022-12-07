namespace AOC07A;

public static class InputParser
{
    public static List<FileStuff> ParseInput(string[] input)
    {
        List<FileStuff> fileStuffs = new();

        if (!input.Any())
        {
            Console.WriteLine("No input!");
            return fileStuffs;
        }

        Stack<string> traversal = new();

        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            var cmd = CommandParser.Parse(line);
            if (cmd is null) throw new InvalidOperationException("cmd is null");

            switch (cmd.Type)
            {
                case CommandParser.CommandType.ChangeDir:
                    traversal.Push(cmd.DirName!);
                    break;
                case CommandParser.CommandType.ChangeDirUp:
                    traversal.Pop();
                    break;
                case CommandParser.CommandType.List:
                    i += ProcessList(input, i, fileStuffs, traversal);
                    break;
                default:
                    throw new NotSupportedException(cmd.Type.ToString());
            }
        }

        return fileStuffs;
    }

    private static int ProcessList(string[] input, int listCmdIndex, List<FileStuff> fileStuffs, Stack<string> traversal)
    {
        int i;
        for (i = listCmdIndex + 1; i < input.Length; i++)
        {
            var parts = input[i].Split(' ');

            if (parts[0] == "$") break;
            if (parts[0] == "dir") continue;

            fileStuffs.Add(new FileStuff
            {
                Name = parts[1],
                Dirs = traversal.Reverse().ToArray(),
                Size = int.Parse(parts[0]),
            }) ;
        }

        int linesProcessed = i - listCmdIndex - 1;
        return linesProcessed;
    }
}
