using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Vents;

public record Coordinate(int x, int y);

public class Node
{
    private bool? _isLowPoint;
    private int? _basinSize;

    public Node(Coordinate coordinate, int index, int value)
    {
        Coordinate = coordinate;
        Index = index;
        Value = value;
    }

    public Coordinate Coordinate { get; }
    public int Index { get; }
    public int Value { get; }
    public Node?[] Neighbors { get; set; } = new Node[0];

    public int BasinSize => _basinSize ??= (IsLowPoint ? CalcBasinSize() : 0);

    public bool IsLowPoint
    {
        get
        {
            return _isLowPoint ??= Neighbors
                .All(n => n != null ? (n.Value > Value) : true);
        }
    }

    private int CalcBasinSize()
    {
        var stack = new Stack<Node?>();
        stack.Push(this);

        var visited = new HashSet<Coordinate>();

        while (stack.Count > 0)
        {
            var node = stack.Pop();

            if (node == null || visited.Contains(node.Coordinate) || node.Value == 9)
            {
                continue;
            }

            visited.Add(node.Coordinate);

            node.Neighbors
                .ForEach(stack.Push);
        }

        return visited.Count;
    }
}

public class Graph
{
    private int _sizeX;
    private int _sizeY;
    private Node[] _graph;
    private List<Node> _lowPoints = new List<Node>();

    public Graph(int[][] data)
    {
        _sizeY = data.Length;
        _sizeX = data[0].Length;

        _graph = new Node[_sizeX * _sizeY];

        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                var coordinate = new Coordinate(x, y);
                var index = ToIndex(coordinate);
                _graph[index] = new Node(coordinate, index, data[y][x]);
            }
        }

        for (int i = 0; i < _graph.Length; i++)
        {
            var node = _graph[i];

            node.Neighbors = GetNeighbors(i);

            if (node.IsLowPoint)
            {
                _lowPoints.Add(node);
            }
        }
    }

    public Node[] LowPoints => _lowPoints.ToArray();

    private Node?[] GetNeighbors(int index)
    {
        var coordinate = ToCoordinate(index);
        var (x, y) = coordinate;

        var neighbors = new Node?[4];
        neighbors[0] = GetNode(x, y - 1);  // N
        neighbors[1] = GetNode(x + 1, y);  // E
        neighbors[2] = GetNode(x, y + 1);  // S
        neighbors[3] = GetNode(x - 1, y);  // W
        return neighbors;
    }

    private Node? GetNode(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _sizeX || y >= _sizeY)
        {
            return null;
        }

        return _graph[GetIndex(x, y)];
    }

    private Coordinate ToCoordinate(int index)
    {
        return new Coordinate(index % _sizeX, index / _sizeX);
    }

    private int ToIndex(Coordinate coordinate)
    {
        return GetIndex(coordinate.x, coordinate.y);
    }

    private int GetIndex(int x, int y)
    {
        return (y * _sizeX) + x;
    }
}
