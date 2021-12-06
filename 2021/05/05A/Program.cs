using System.Diagnostics.CodeAnalysis;
using System.Text;
using Lines;
using static Lines.Utils;

var input = (new[] {
    "0,9 -> 5,9",
    "8,0 -> 0,8",
    "9,4 -> 3,4",
    "2,2 -> 2,1",
    "7,0 -> 7,4",
    "6,4 -> 2,0",
    "0,9 -> 2,9",
    "3,4 -> 1,4",
    "0,0 -> 8,8",
    "5,5 -> 8,2"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename);
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var nodes = new Dictionary<Coordinate, int>();

input
    .Select(line => new Line(line))
    .ForEach(([DisallowNull]line) => nodes.Increment(line.Points));

System.Console.WriteLine(nodes.Where(kvp => kvp.Value >= 2).Count());
