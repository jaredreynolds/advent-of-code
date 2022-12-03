namespace AOC03A;

public class Item
{
    public char Id { get; }
    public int Priority { get; }

    public Item(char itemId)
    {
        Id = itemId;
        Priority = GetPriority(itemId);
    }

    private static int GetPriority(char itemId)
    {
        return itemId switch
        {
            (>= 'a') and (<= 'z') => itemId - 'a' + 1,
            (>= 'A') and (<= 'Z') => itemId - 'A' + 27,
            _ => throw new ArgumentOutOfRangeException($"{itemId}")
        };
    }
}
