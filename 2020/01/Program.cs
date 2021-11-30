using System.Diagnostics;
using ExpenseReport;
using static ExpenseReport.Utils;

const int target = 2020;

int[] data = (new[] {
    1721,
    979,
    366,
    299,
    675,
    1456
}).Randomize();

var filename = args.Length > 0 ? args[0] : "input.txt";
if (File.Exists(filename)) {
    data = File.ReadAllLines(filename).Select(s => int.Parse(s)).ToArray();
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short, randomized sample data.");
}

var operandPair = new OperandPair(0, 0);

var stopwatch = new Stopwatch();
stopwatch.Start();

for (int i = 0; i < data.Length; i++)
{
    var diff = target - data[i];
    if (data.Contains(diff)) {
        operandPair = new OperandPair(data[i], diff);
        break;
    }
}

stopwatch.Stop();

var (operand1, operand2) = operandPair;

if (!data.Contains(operand1) || !data.Contains(operand2)) {
    WriteLine(ConsoleColor.Red, $"Found ({operand1},{operand2}) but one of them isn't in the data!");
    return;
}

Console.WriteLine($"{operand1} + {operand2} = {operand1 + operand2}");
Console.WriteLine($"{operand1} * {operand2} = {operand1 * operand2}");
Console.WriteLine($"\nTime: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.ElapsedTicks:N} ticks)");
