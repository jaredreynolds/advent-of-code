using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static Flashes.Utils;

namespace Flashes;

public record Coordinate(int x, int y);

public class Node
{
    public Node(Coordinate coordinate, int index, int value)
    {
        Coordinate = coordinate;
        Index = index;
        Value = value;
    }

    public Coordinate Coordinate { get; }
    public int Index { get; }
    public int Value { get; private set; }
    public bool HasFlashed { get; private set; }
    public int FlashCount { get; private set; }
    public Node?[] Neighbors { get; set; } = new Node[0];

    public void Resolve()
    {
        if (!HasFlashed && Value > 9)
        {
            ++FlashCount;
            FlashNeighbors();
        }
    }

    public void PowerUp()
    {
        ++Value;
        Resolve();
    }

    private void FlashNeighbors()
    {
        HasFlashed = true;
        foreach (var neighbor in Neighbors)
        {
            neighbor?.PowerUp();
        }
    }

    public void StepStart()
    {
        HasFlashed = false;
        ++Value;
    }

    public void StepEnd()
    {
        if (Value > 9)
        {
            Value = 0;
        }
    }
}

public class Graph
{
    private int _sizeX;
    private int _sizeY;
    private Node[] _graph;

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
        }
    }

    public int TotalFlashes => _graph.Sum(n => n.FlashCount);
    public bool AllFashed => _graph.All(n => n.HasFlashed);

    public void Step()
    {
        // 1 - increase all by 1
        for (int i = 0; i < _graph.Length; i++)
        {
            _graph[i].StepStart();
        }

        // 2 - resolve flashing
        for (int i = 0; i < _graph.Length; i++)
        {
            _graph[i].Resolve();
        }

        // 3 - reset those that have flashed
        for (int i = 0; i < _graph.Length; i++)
        {
            _graph[i].StepEnd();
        }
    }

    public void Dump()
    {
        Node node;
        ConsoleColor color;

        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                node = _graph[GetIndex(x, y)];
                color = node.HasFlashed ? ConsoleColor.Blue : ConsoleColor.DarkBlue;
                Write(color, node.Value.ToString());
            }

            System.Console.WriteLine();
        }
    }

    private Node?[] GetNeighbors(int index)
    {
        var coordinate = ToCoordinate(index);
        var (x, y) = coordinate;

        var neighbors = new Node?[8];
        neighbors[0] = GetNode(x, y - 1);      // N
        neighbors[1] = GetNode(x + 1, y - 1);  // NE
        neighbors[2] = GetNode(x + 1, y);      // E
        neighbors[3] = GetNode(x + 1, y + 1);  // SE
        neighbors[4] = GetNode(x, y + 1);      // S
        neighbors[5] = GetNode(x - 1, y + 1);  // SW
        neighbors[6] = GetNode(x - 1, y);      // W
        neighbors[7] = GetNode(x - 1, y - 1);  // W
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
