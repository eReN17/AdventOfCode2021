using System.Drawing;

var lines = File.ReadLines("input.txt");

var grid = ParseInput(lines);

var lowPointsSum = 0;
for (var row = 0; row < lines.Count(); row++)
{
    for (var column = 0; column < lines.First().Length; column++)
    {
        // Top
        if (row != 0 && grid[row, column] >= grid[row - 1, column])
            continue;

        // Bottom
        if (row != lines.Count() - 1 && grid[row, column] >= grid[row + 1, column])
            continue;

        // Left
        if (column != 0 && grid[row, column] >= grid[row, column - 1])
            continue;

        // Right
        if (column != lines.First().Length - 1 && grid[row, column] >= grid[row, column + 1])
            continue;

        // Lowest
        lowPointsSum += grid[row, column] + 1;
    }
}

Console.WriteLine("Sum of the risk levels of all low points: {0}", lowPointsSum);

var basins = FindBasins(grid);

static int[,] ParseInput(IEnumerable<string> input)
{
    // each line has same length
    var grid = new int[input.Count(), input.First().Length];

    for (var row = 0; row < input.Count(); row++)
        for (var column = 0; column < input.First().Length; column++)
            grid[row, column] = int.Parse(input.ElementAt(row)[column].ToString());

    return grid;
}

static List<int> FindBasins(int [,] grid)
{
    var basins = new List<int>();

    var found = new bool[grid.GetUpperBound(0), grid.GetUpperBound(1)];

    for (var row = 0; row < grid.GetUpperBound(0); row++)
        for (var column = 0; column < grid.GetUpperBound(1); column++)
        {
            if (grid[row, column] != 9 && !found[row, column])
                basins.Add(GetBasinRec(row, column, grid, ref found));
        }

    return basins;
}

static int GetBasinRec(int row, int column, int [,] grid, ref bool [,] found)
{
    found[row, column] = true;
    var value = 1;  //grid[row, column];

    // Top
    if (row != 0 && grid[row - 1, column] != 9 && !found[row -1, column])
        value += GetBasinRec(row - 1, column, grid, ref found);

    // Bottom
    if (row != grid.GetUpperBound(0) - 1 && grid[row + 1, column] != 9 && !found[row + 1, column])
        value += GetBasinRec(row + 1, column, grid, ref found);

    // Left
    if (column != 0 && grid[row, column-1] != 9 && !found[row, column - 1])
        value += GetBasinRec(row, column-1, grid, ref found);

    // Right
    if (column != grid.GetUpperBound(1) -1 && grid[row, column + 1] != 9 && !found[row, column + 1])
        value += GetBasinRec(row, column+1, grid, ref found);

    return value;
}