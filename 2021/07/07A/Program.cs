using System.Diagnostics.CodeAnalysis;
using System.Text;
using CrabFuel;
using static CrabFuel.Utils;

var input = (new string[] {
    "16,1,2,0,4,2,7,1,2,14"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename);
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var crabLoc = input
    .First()
    .Split(',')
    .Select(c => Int32.Parse(c))
    .ToArray();

int minCost = Int32.MaxValue;
int minTarget = -1;
int minLoc = crabLoc.Min();
int maxLoc = crabLoc.Max();

for (int target = minLoc; target <= maxLoc; target++)
{
    var cost = crabLoc.Sum(loc => Math.Abs(target - loc));

    if (cost < minCost)
    {
        minCost = cost;
        minTarget = target;
    }
}

System.Console.WriteLine($"Target: {minTarget} for {minCost} fuel");
