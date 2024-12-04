using QuikGraph;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AOC08A;

public class Map
{
    public Tree[][] Trees { get; }
    public int Width { get; }
    public int Height { get; }
    public AdjacencyGraph<Tree, STaggedEdge<Tree, Direction>> TreeGraph { get; } = new();
    public Dictionary<Coordinate, Tree> TreesByCoordinate { get; } = new();

    private int? _treesVisibileFromOutside;
    public int TreesVisibleFromOutside => _treesVisibileFromOutside ??= CalcVisibilityFromOutside();

    private Tree? _mostScenicTree;
    public Tree MostScenicTree
    {
        get { return _mostScenicTree ??= CalcMostScenicTree(); }
    }

    public Map(int width, int height, IEnumerable<Tree> trees)
    {
        Width = width;
        Height = height;

        Trees = new Tree[width][];
        for (int x = 0; x < width; x++)
        {
            Trees[x] = new Tree[height];
        }

        foreach (var tree in trees)
        {
            Trees[tree.X][tree.Y] = tree;
            TreesByCoordinate[tree.Coordinate] = tree;
        }

        GenerateGraph(trees);
    }

    public void GenerateGraph(IEnumerable<Tree> trees)
    {
        var directions = new[] { Direction.North, Direction.East, Direction.South, Direction.West };

        foreach (var sourceTree in trees)
        {
            var neighbors = sourceTree.Coordinate.Neighbors(Width, Height);

            foreach (var direction in directions)
            {
                var neighbor = neighbors.GetNeighbor(direction);
                if (neighbor is not null)
                {
                    var targetTree = TreesByCoordinate[neighbor];
                    TreeGraph.AddVerticesAndEdge(new STaggedEdge<Tree, Direction>(sourceTree, targetTree, direction));
                }
            }
        }
    }

    private int CalcVisibilityFromOutside()
    {
        foreach (var fromDirection in new[] { Direction.North, Direction.South })
        {
            for (int x = 0; x < Width; x++)
            {
                CalcVisibility(GetRowOrColumn(x, 0, fromDirection), fromDirection);
            }
        }

        foreach (var fromDirection in new[] { Direction.East, Direction.West })
        {
            for (int y = 0; y < Height; y++)
            {
                CalcVisibility(GetRowOrColumn(0, y, fromDirection), fromDirection);
            }
        }

        int count = 0;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                count += Trees[x][y].IsVisible ? 1 : 0;
            }
        }

        return count;
    }

    private Tree[] GetRowOrColumn(int x, int y, Direction fromDirection, Range? range = null)
    {
        var localRange = range ?? (fromDirection == Direction.North || fromDirection == Direction.South
            ? 0..Height
            : 0..Width);

        return fromDirection switch
        {
            Direction.North => Trees[x][localRange].Reverse().ToArray(),
            Direction.East => GetRow(y).Reverse().ToArray(),
            Direction.South => Trees[x][localRange],
            Direction.West => GetRow(y).ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(fromDirection))
        };

        IEnumerable<Tree> GetRow(int localY)
        {
            for (int localX = 0; localX < Width; localX++)
            {
                yield return Trees[localX][localY];
            }
        }
    }

    private static void CalcVisibility(Tree[] treeLine, Direction fromDirection)
    {
        treeLine[0].VisibleFrom |= fromDirection;
        int maxHeight = treeLine[0].Height;

        for (int i = 1; i < treeLine.Length; i++)
        {
            var tree = treeLine[i];
            if (tree.Height > maxHeight)
            {
                tree.VisibleFrom |= fromDirection;
                maxHeight = tree.Height;
            }
        }
    }

    private Tree CalcMostScenicTree()
    {
        Tree mostScenicTree = new(0, 0, 0);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (!TreeGraph.TryGetOutEdges(Trees[x][y], out var edges)) continue;

                edges.
            }
        }

        return mostScenicTree;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                sb.Append(Trees[x][y].Height);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
