using System.Diagnostics;
using static AOC05A.InputParser;

namespace AOC05A;

public class Dock
{
    public List<Stack<char>> Stacks { get; } = new();

    public void Execute(Move[] moves, MoveStyle moveStyle)
    {
        foreach (var move in moves)
        {
            if (moveStyle == MoveStyle.Single)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = Stacks[move.Source - 1].Pop();
                    Stacks[move.Destination - 1].Push(crate);
                }
            }
            else if (moveStyle == MoveStyle.Multiple)
            {
                Stack<char> tempStack = new();

                for (int i = 0; i < move.Count; i++)
                {
                    var crate = Stacks[move.Source - 1].Pop();
                    tempStack.Push(crate);
                }
                for (int i = 0; i < move.Count; i++)
                {
                    var crate = tempStack.Pop();
                    Stacks[move.Destination - 1].Push(crate);
                }
                
            }

        }
    }

    public void PrintTops()
    {
        for (int i = 0; i < Stacks.Count; i++)
        {
            Console.Write(Stacks[i].Peek());
        }
        Console.WriteLine();
    }

    public enum MoveStyle
    {
        Single,
        Multiple
    }
}
