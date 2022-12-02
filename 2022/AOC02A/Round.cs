namespace AOC02A;

public class Round
{
    public int MyPlay { get; }
    public int TheirPlay { get; }
    public int Outcome { get; set; }

    public Round(int myPlay, int theirPlay)
    {
        MyPlay = myPlay;
        TheirPlay = theirPlay;
        Outcome = AOC02A.Outcome.GetOutcome(myPlay, theirPlay);
    }

    public Round(int myPlay, int theirPlay, int outcome)
    {
        MyPlay = myPlay;
        TheirPlay = theirPlay;
        Outcome = outcome;
    }
}
