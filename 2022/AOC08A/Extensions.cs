using QuikGraph;

namespace AOC08A;

public static class Extensions
{
    public static CoordinateNeighbors Neighbors(this Coordinate origin, int width, int height)
    {
        return new(
            origin.NorthNeighbor(width, height),
            origin.EastNeighbor(width, height),
            origin.SouthNeighbor(width, height),
            origin.WestNeighbor(width, height));
    }

    public static Coordinate? NorthNeighbor(this Coordinate origin, int width, int height)
        => origin.Neighbor(width, height, yDelta: +1);
    public static Coordinate? EastNeighbor(this Coordinate origin, int width, int height)
        => origin.Neighbor(width, height, xDelta: +1);
    public static Coordinate? SouthNeighbor(this Coordinate origin, int width, int height)
        => origin.Neighbor(width, height, yDelta: -1);
    public static Coordinate? WestNeighbor(this Coordinate origin, int width, int height)
        => origin.Neighbor(width, height, xDelta: -1);

    public static Coordinate? Neighbor(this Coordinate origin, int width, int height, int xDelta = 0, int yDelta = 0)
    {
        var neighbor = new Coordinate(origin.X + xDelta, origin.Y + yDelta);
        return neighbor.X >= 0 && neighbor.X < width && neighbor.Y >= 0 && neighbor.Y < height
            ? neighbor
            : null;
    }

    public static void WalkUntil<TVertex, TTag>(this IEnumerable<STaggedEdge<TVertex, TTag>> edges, TTag tag, Func<IEnumerable<STaggedEdge<TVertex, TTag>>, TTag tag, TVertex?)
}
