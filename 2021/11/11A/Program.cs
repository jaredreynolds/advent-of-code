using System.Diagnostics.CodeAnalysis;
using System.Text;
using Flashes;
using static Flashes.Utils;

var input = (new string[] {
    "5483143223",
    "2745854711",
    "5264556173",
    "6141336146",
    "6357385478",
    "4167524645",
    "2176841721",
    "6882881134",
    "4846848554",
    "5283751526"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename);
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var data = input
    .Select(s => s.ToCharArray())
    .Select(ca => ca.Select(c => c - 48).ToArray())
    .ToArray();

var graph = new Graph(data);

for (int step = 1; step <= 100; step++)
{
    graph.Step();
    if (step % 10 == 0)
    {
        WriteLine(ConsoleColor.DarkCyan, $"After step {step}:");
        graph.Dump();
        System.Console.WriteLine();
    }
}

System.Console.WriteLine($"Total flashes: {graph.TotalFlashes}");
