using System.Diagnostics.CodeAnalysis;
using System.Text;
using Vents;
using static Vents.Utils;

var input = (new string[] {
    "2199943210",
    "3987894921",
    "9856789892",
    "8767896789",
    "9899965678"
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

var risk = new Graph(data)
    .LowPoints
    .Sum(p => p.Value + 1);

System.Console.WriteLine($"Risk: {risk}");
