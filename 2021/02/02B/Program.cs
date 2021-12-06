using Directions;
using static Directions.Utils;

var data = (new[] {
    "forward 5",
    "down 5",
    "forward 8",
    "up 3",
    "down 8",
    "forward 2"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    data = File.ReadAllLines(filename).ToArray();
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var parsedData = data.Select(d =>
{
    var parts = d.Split(" ");
    var dir = Enum.Parse<Direction>(parts[0]);
    var dist = int.Parse(parts[1]);

    return new Vector(dir, dist);
});

var xpos = 0;
var dpos = 0;
var aim = 0;

foreach (var (dir,dist) in parsedData)
{
    switch (dir)
    {
        case Direction.forward:
            xpos += dist;
            dpos += (aim * dist);
            break;
        case Direction.down:
            aim += dist;
            break;
        case Direction.up:
            aim -= dist;
            break;
    }
}

System.Console.WriteLine(xpos * dpos);
