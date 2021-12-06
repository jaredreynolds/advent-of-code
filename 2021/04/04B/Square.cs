using System.Diagnostics.CodeAnalysis;

namespace Bingo;

public record Coordinate(int x, int y);

public class Square
{
    public Square(Coordinate coordinate, int index, int value)
    {
        Coordinate = coordinate;
        Index = index;
        Value = value;
    }

    public Coordinate Coordinate { get; }
    public int Index { get; set; }
    public int Value { get; set; }
    public bool IsMarked { get; set; }
}

public class Board
{
    private readonly int _size;
    private int[] _markedCountRows = new int[0];
    private int[] _markedCountColumns = new int[0];
    [AllowNull] private ILookup<int, Square> _squaresByValue;
    private int _lastCalledValue;

    public Board(int[] values)
    {
        _size = (int)Math.Sqrt(values.Length);
        InitSquares(values);
        InitTrackers();
    }

    public Square[] Squares { get; set; } = new Square[0];
    public bool IsWinner { get; set; }

    public bool MarkSquare(int value)
    {
        IsWinner = false;
        var squares = _squaresByValue[value];
        foreach (var square in squares)
        {
            square.IsMarked = true;
            _markedCountRows[square.Coordinate.x]++;
            _markedCountColumns[square.Coordinate.y]++;
            if (_markedCountRows[square.Coordinate.x] == _size || _markedCountColumns[square.Coordinate.y] == _size)
            {
                IsWinner = true;
                _lastCalledValue = value;
            }
        }
        return IsWinner;
    }

    public int Score {
        get
        {
            if (!IsWinner)
            {
                return 0;
            }

            return Squares.Sum(s => s.IsMarked ? 0 : s.Value) * _lastCalledValue;
        }
    }

    private void InitSquares(int[] values)
    {
        Squares = values
            .Select((value, index) => new Square(ToCoordinate(index), index, value))
            .ToArray();
    }

    private void InitTrackers()
    {
        _markedCountRows = new int[_size];
        _markedCountColumns = new int[_size];
        _squaresByValue = Squares.ToLookup(s => s.Value);
    }

    private Coordinate ToCoordinate(int index)
    {
        return new Coordinate(index % _size, index / _size);
    }

    private int ToIndex(Coordinate coordinate)
    {
        return (coordinate.y * _size) + coordinate.x;
    }
}
