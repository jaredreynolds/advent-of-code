using System.Diagnostics.CodeAnalysis;
using System.Text;
using Fish;
using static Fish.Utils;

const int numDays = 256;

var input = (new string[] {
    "3,4,3,1,2"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename);
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var fishByStage = new ulong[9];

input
    .First()
    .Split(',')
    .Select(i => ulong.Parse(i))
    .ForEach(i => ++fishByStage[i]);

ulong numNew;

for (int i = 0; i < numDays; i++)
{
    numNew = fishByStage[0];

    for (int j = 1; j < fishByStage.Length; j++)
    {
        // shift counts left (decrease timer)
        fishByStage[j - 1] = fishByStage[j];
    }

    fishByStage[6] += numNew; // just spawned/reset
    fishByStage[8] = numNew;
}

ulong sum = 0;
for (int i = 0; i < fishByStage.Length; i++)
{
    sum += fishByStage[i];
}
System.Console.WriteLine(sum);
