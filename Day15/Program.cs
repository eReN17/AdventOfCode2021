using System.Drawing;
using System.Text;

var rawData = File.ReadLines("input.txt").ToList();

//var grid = new Grid(rawData);

//grid.Move(Direction.Right);
//grid.Move(Direction.Bottom);
//grid.Move(Direction.Bottom);
//grid.Move(Direction.Right);
//grid.Move(Direction.Right);
//grid.Move(Direction.Bottom);
//grid.Move(Direction.Bottom);
//grid.Move(Direction.Bottom);
//grid.Move(Direction.Bottom);
//grid.Move(Direction.Left);
//grid.Move(Direction.Left);
//grid.Move(Direction.Top);

//grid.ToConsole();

//Console.Read();
var grid = new Grid(rawData);

while (!grid.IsSolved())
{
    if (grid.EndLocation.X - grid.ActualLocation.X < grid.EndLocation.Y - grid.ActualLocation.Y)
    {
        grid.Move(Direction.Bottom);
    }
    else
    {
        grid.Move(Direction.Right);
    }
}
var shortestPath = grid.ActualScore;

CalculateShortestPath(ref shortestPath, rawData);

static void CalculateShortestPath(ref int shortestPath, List<string> rawData)
{
    var grid = new Grid(rawData);

    MoveRecursive(grid.CloneMe(), Direction.Bottom, ref shortestPath);
    MoveRecursive(grid.CloneMe(), Direction.Right, ref shortestPath);
}

Console.WriteLine("Shortest path: {0}", shortestPath);

static void MoveRecursive(Grid grid, Direction direction, ref int shortestPath)
{
    if (grid.Move(direction))
    {
        if (grid.ActualScore >= shortestPath)
        {
            return;
        }

        if (grid.IsSolved())
        {
            shortestPath = grid.ActualScore;
            //Console.WriteLine($"Solved grid: { grid.ActualScore}");
            //grid.ToConsole();
        }
        else
        {
            MoveRecursive(grid.CloneMe(), Direction.Left, ref shortestPath);
            MoveRecursive(grid.CloneMe(), Direction.Right, ref shortestPath);
            MoveRecursive(grid.CloneMe(), Direction.Top, ref shortestPath);
            MoveRecursive(grid.CloneMe(), Direction.Bottom, ref shortestPath);
        }
    }
    else
    {
        //Console.WriteLine("can't move: {0}", direction);
        //grid.ToConsole();
    }
}

class Tile : ICloneable
{
    public Tile(int value)
        : this(value, false)
    {
        
    }

    public Tile(int value, bool visited = false)
    {
        Value = value;
        Visited = visited;
    }

    public bool Visited { get; set; } = false;
    public int Value { get; set; }

    public object Clone()
        => new Tile(Value, Visited);

    public Tile CloneMe()
        => (Tile)Clone();
}

class Grid : ICloneable
{
    private Tile[,] _map;

    private Point _start = new Point(0, 0);

    public Grid(List<string> rawData)
    {
        _map = new Tile[rawData.Count, rawData.First().Length];

        for (var row = 0; row <= _map.GetUpperBound(0); row++)
            for (var col = 0; col <= _map.GetUpperBound(1); col++)
                _map[row, col] = new Tile(int.Parse(rawData[row][col].ToString()));

        ActualLocation = _start;
        EndLocation = new Point(_map.GetUpperBound(0), _map.GetUpperBound(1));
    }

    private Grid(Grid grid)
    {
        _map = new Tile[grid._map.GetUpperBound(0) + 1, grid._map.GetUpperBound(1) + 1];

        for (var row = 0; row <= _map.GetUpperBound(0); row++)
            for (var col = 0; col <= _map.GetUpperBound(1); col++)
                _map[row, col] = grid.GetTile(row, col).CloneMe();

        ActualScore = grid.ActualScore;
        ActualLocation = grid.ActualLocation;
        EndLocation = grid.EndLocation;
    }

    public Point ActualLocation { get; private set; }

    public Point EndLocation { get; private set; }

    public int ActualScore { get; private set; }

    public bool IsSolved()
        => ActualLocation.X == EndLocation.X &&
            ActualLocation.Y == EndLocation.Y;

    public bool Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Top:
                if (ActualLocation.Y == 0)
                    return false;

                ActualLocation = new Point(ActualLocation.X, ActualLocation.Y - 1);
                break;
            case Direction.Bottom:
                if (ActualLocation.Y == _map.GetUpperBound(0))
                    return false;

                ActualLocation = new Point(ActualLocation.X, ActualLocation.Y + 1);
                break;
            case Direction.Left:
                if (ActualLocation.X == 0)
                    return false;

                ActualLocation = new Point(ActualLocation.X - 1, ActualLocation.Y);
                break;
            case Direction.Right:
                if (ActualLocation.X == _map.GetUpperBound(1))
                    return false;

                ActualLocation = new Point(ActualLocation.X + 1, ActualLocation.Y);
                break;
            default:
                break;
        }

        if (GetActualTile().Visited)
            return false;

        GetActualTile().Visited = true;
        ActualScore += GetActualTile().Value;
        return true;
    }

    private Tile GetActualTile()
        => _map[ActualLocation.Y, ActualLocation.X];

    private Tile GetTile(int row, int column)
        => _map[row, column];

    public object Clone()
        => new Grid(this);

    public Grid CloneMe()
        => (Grid)Clone();

    public override string ToString()
    {
        var gridBuilder = new StringBuilder();

        for (var row = 0; row <= _map.GetUpperBound(0); row++)
        {
            for (var column = 0; column <= _map.GetUpperBound(1); column++)
            {
                gridBuilder.Append(GetTile(row, column).Value);
            }
            gridBuilder.Append(Environment.NewLine);
        }

        return gridBuilder.ToString();
    }

    public void ToConsole()
    {
        for (var row = 0; row <= _map.GetUpperBound(0); row++)
        {
            for (var column = 0; column <= _map.GetUpperBound(1); column++)
            {
                if (GetTile(row, column).Visited)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                
                if (ActualLocation.X == column && ActualLocation.Y == row)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(GetTile(row, column).Value);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
    }
}

enum Direction
{
    Top,
    Bottom,
    Left,
    Right
}