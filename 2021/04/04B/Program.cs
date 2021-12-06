using System.Text;
using Bingo;
using static Bingo.Utils;

var input = (new[] {
"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
"",
"22 13 17 11  0",
" 8  2 23  4 24",
"21  9 14 16  7",
" 6 10  3 18  5",
" 1 12 20 15 19",
"",
" 3 15  0  2 22",
" 9 18 13 17  5",
"19  8  7 25 23",
"20 11 10 24  4",
"14 21 16 12  6",
"",
"14 21 17 24  4",
"10 16 15  9 19",
"18  8 23 26 20",
"22 11 13  6  5",
" 2  0 12  3  7",
""
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename).Concat(new[] { "" }).ToArray();
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var calledNumbers = input[0].Split(',').Select(i => Int32.Parse(i)).ToArray();

var boards = new List<Board>();
var boardValues = new List<int>();

foreach (var line in input.Skip(2))
{
    if (string.IsNullOrWhiteSpace(line) && boardValues.Count > 0)
    {
        boards.Add(new Board(boardValues.ToArray()));
        boardValues.Clear();
        continue;
    }

    boardValues.AddRange(
        line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(i => Int32.Parse(i))
    );
}

var winnerDeclared = false;
var calledNumberIndex = 0;

foreach (var calledNumber in calledNumbers)
{
    foreach (var board in boards.Where(b => !b.IsWinner))
    {
        winnerDeclared = board.MarkSquare(calledNumber);
        if (winnerDeclared)
        {
            System.Console.WriteLine($"Winner! {board.Score}");
        }
    }
}
