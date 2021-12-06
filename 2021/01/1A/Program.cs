using Depth;
using static Depth.Utils;

int[] data = (new[] {
    199,
    200,
    208,
    210,
    200,
    207,
    240,
    269,
    260,
    263
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    data = File.ReadAllLines(filename).Select(s => int.Parse(s)).ToArray();
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var increaseCount = 0;

for (int i = 1; i < data.Length; i++)
{
    if (data[i] > data[i-1]) {
        ++increaseCount;
    }
}

System.Console.WriteLine(increaseCount);
