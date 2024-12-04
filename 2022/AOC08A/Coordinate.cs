namespace AOC08A;

public record Coordinate(int X, int Y);

public record CoordinateNeighbors(Coordinate? North, Coordinate? East, Coordinate? South, Coordinate? West)
{
    public Coordinate? GetNeighbor(Direction direction)
    {
        return direction switch
        {
            Direction.North => North,
            Direction.East => East,
            Direction.South => South,
            Direction.West => West,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };
    } 
};
