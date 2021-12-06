using System.Diagnostics.CodeAnalysis;

namespace Fish;

public record Coordinate(int x, int y);

public class Line
{
    public Line(Coordinate start, Coordinate end)
    {
        Start = start;
        End = end;
        Points = GetPoints();
    }

    public Line(string line) : this(Parse(line)) { }

    private Line(Coordinate[] points) : this(points[0], points[1]) { }

    private static Coordinate[] Parse(string line)
    {
        var points = line.Split(" -> ");
        var start = points[0].Split(',').Select(p => Int32.Parse(p)).ToArray();
        var end = points[1].Split(',').Select(p => Int32.Parse(p)).ToArray();
        return new[] { new Coordinate(start[0], start[1]), new Coordinate(end[0], end[1]) };
    }

    public Coordinate Start { get; }
    public Coordinate End { get; }
    public Coordinate[] Points { get; }

    private Coordinate[] GetPoints()
    {
        int rise = End.y - Start.y;
        int run = End.x - Start.x;

        int riseStep = Math.Sign(rise);
        int runStep = Math.Sign(run);

        var points = new HashSet<Coordinate>();
        points.Add(Start);

        var currentPoint = Start;
        do
        {
            currentPoint = currentPoint with {
                x = currentPoint.x + runStep,
                y = currentPoint.y + riseStep
            };
            points.Add(currentPoint);
        } while (currentPoint != End);

        return points.ToArray();
    }
}
