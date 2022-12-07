using AOC07A;
using Data;

var challengeData = Fetcher.GetChallengeData("07");
var input = challengeData.Input;

var files = InputParser.ParseInput(input);

Dictionary<string, int> dirSizes = new();

foreach (var file in files)
{
    for (int i = 0; i < file.Dirs.Length; i++)
    {
        var dir = string.Join('\\', file.Dirs.Take(i + 1));
        if (!dirSizes.ContainsKey(dir))
        {
            dirSizes[dir] = 0;
        }

        dirSizes[dir] += file.Size;
    }
}

Console.WriteLine($"Total <= 100,000: {dirSizes.Values.Where(s => s <= 100_000).Sum()}");

const int totalSpace = 70_000_000;
const int spaceRequired = 30_000_000;
var neededSpace = spaceRequired - (totalSpace - dirSizes["/"]);

var smallestDirToDelete = dirSizes.Where(ds => ds.Value >= neededSpace).MinBy(ds => ds.Value);
Console.WriteLine($"Smallest dir to delete: {smallestDirToDelete.Key} ({smallestDirToDelete.Value})");
