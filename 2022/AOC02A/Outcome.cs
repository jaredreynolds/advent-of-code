using static AOC02A.ItemType;

namespace AOC02A;

public static class Outcome
{
    private static readonly Dictionary<(int mine, int theirs), int> _outcomesByItems = InitOutcomesByItems();
    private static readonly Dictionary<(int theirs, int outcome), int> _itemsByItemOutcome = InitItemsByItemOutcome();

    public const int Lose = 0;
    public const int Draw = 3;
    public const int Win = 6;

    public static int GetOutcome(int mine, int theirs) => _outcomesByItems[(mine, theirs)];
    public static int GetItem(int theirs, int outcome) => _itemsByItemOutcome[(theirs, outcome)];

    private static Dictionary<(int mine, int theirs), int> InitOutcomesByItems()
    {
        return new()
        {
            [(Rock, Rock)] = Draw,
            [(Rock, Paper)] = Lose,
            [(Rock, Scissors)] = Win,
            [(Paper, Rock)] = Win,
            [(Paper, Paper)] = Draw,
            [(Paper, Scissors)] = Lose,
            [(Scissors, Rock)] = Lose,
            [(Scissors, Paper)] = Win,
            [(Scissors, Scissors)] = Draw
        };
    }

    private static Dictionary<(int theirs, int outcome), int> InitItemsByItemOutcome()
    {
        return new()
        {
            [(Rock, Win)] = Paper,
            [(Rock, Lose)] = Scissors,
            [(Rock, Draw)] = Rock,
            [(Paper, Win)] = Scissors,
            [(Paper, Lose)] = Rock,
            [(Paper, Draw)] = Paper,
            [(Scissors, Win)] = Rock,
            [(Scissors, Lose)] = Paper,
            [(Scissors, Draw)] = Scissors,
        };
    }
}
