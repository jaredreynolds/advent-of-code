namespace AOC04A;

public class Elf
{
    public string Id { get; set; } = string.Empty;
    public HashSet<int> Assignment { get; } = new();

    public Elf(string id, string assignment)
    {
        GetAssignment(assignment);
        Id = id;
    }

    private void GetAssignment(string assignment)
    {
        var parts = assignment.Split('-').Select(assn => int.Parse(assn)).ToArray();
        var begin = parts[0];
        var end = parts[1];
        for (var i = begin; i <= end; i++)
        {
            Assignment.Add(i);
        }
    }
}
