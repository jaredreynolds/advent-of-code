//using System.Diagnostics;
//using static AOC02A.ItemType;
//using static AOC02A.Outcome;

//namespace AOC02A;

//public class GameItem : IComparable<GameItem>
//{
//    private static readonly Dictionary<(int mine, int theirs), int> _outcomes = InitOutcomes();

//    public int ItemType { get; set; }

//    public GameItem(int itemType)
//    {
//        ItemType = itemType;
//    }

//    public static GameItem ParseTheirs(char theirPlay) => new(TheirPlay(theirPlay));
//    public static GameItem ParseMine(char myPlay) => new(MyPlay(myPlay));

//    public int CompareTo(GameItem? other)
//    {
//        Debug.Assert(other is not null);

//        return _outcomes[(ItemType, other.ItemType)];
//    }

//    private static Dictionary<(int mine, int theirs), int> InitOutcomes()
//    {
//        return new()
//        {
//            [(Rock, Rock)] = Draw,
//            [(Rock, Paper)] = Lose,
//            [(Rock, Scissors)] = Win,
//            [(Paper, Rock)] = Win,
//            [(Paper, Paper)] = Draw,
//            [(Paper, Scissors)] = Lose,
//            [(Scissors, Rock)] = Lose,
//            [(Scissors, Paper)] = Win,
//            [(Scissors, Scissors)] = Draw
//        };
//    }
//}
