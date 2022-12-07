namespace AOC07A;

public static class CommandParser
{
    public static Command Parse(string rawCommand)
    {
        var parts = rawCommand.Split(' ');

        return parts[1] switch
        {
            "ls" => new Command(CommandType.List),
            "cd" when parts[2] == ".." => new Command(CommandType.ChangeDirUp),
            "cd" => new Command(CommandType.ChangeDir, parts[2]),
            _ => throw new NotSupportedException(rawCommand)
        };
    }

    public record Command(CommandType Type, string? DirName = null);

    public enum CommandType
    {
        Unknown,
        ChangeDir,
        ChangeDirUp,
        List
    }
}
