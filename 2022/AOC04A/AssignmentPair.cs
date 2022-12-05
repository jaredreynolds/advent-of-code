namespace AOC04A;

public class AssignmentPair
{
    public Elf[] Elves { get; }

    public AssignmentPair(Elf[] elves)
    {
        Elves = elves;
    }

    public bool IsOneAssignmentSubsetOfOther()
    {
        return Elves[0].Assignment.IsSubsetOf(Elves[1].Assignment)
            || Elves[1].Assignment.IsSubsetOf(Elves[0].Assignment);
    }

    public bool AssignmentsOverlap()
    {
        return Elves[0].Assignment.Intersect(Elves[1].Assignment).Any();
    }
}
