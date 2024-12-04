namespace AOC08A;

public class Tree : IEquatable<Tree>
{
    public int Height { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Coordinate Coordinate { get; set; }
    public Direction VisibleFrom { get; set; }
    public bool IsVisible => VisibleFrom > 0;
    public int ScenicValue { get; set; }

    public Tree(int height, int x, int y)
    {
        Height = height;
        X = x;
        Y = y;
        Coordinate = new(x, y);
    }

    public bool Equals(Tree? other)
        => other is not null && Coordinate == other.Coordinate && Height == other.Height;

    public override bool Equals(object? obj) => Equals(obj as Tree);

    public override int GetHashCode() => HashCode.Combine(Coordinate, Height);
}
