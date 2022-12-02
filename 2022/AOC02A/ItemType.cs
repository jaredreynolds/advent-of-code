namespace AOC02A;

public static class ItemType
{
    public const int Rock = 1;
    public const int Paper = 2;
    public const int Scissors = 3;

    public static int TheirPlay(char theirPlay) => theirPlay - 'A' + 1;
    public static int MyPlay(char myPlay) => myPlay - 'X' + 1;
    public static int MyOutcome(char myOutcome) => (myOutcome - 'X') * 3;
}
