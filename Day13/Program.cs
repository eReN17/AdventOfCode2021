using Day13;

const string FoldString = "fold along ";

var lines = File.ReadLines("input.txt");

var grid = new Grid();

var folds = new List<Tuple<char, int>>();

foreach (var l in lines)
{
    var coords = l.Split(',');
    if (coords.Length == 2)
    {
        grid.AddPoint(
            int.Parse(coords[0]),
            int.Parse(coords[1])
        );
    }
    else
    {
        if (l.Contains(FoldString))
        {
            var foldData = l.Substring(FoldString.Length).Split('=');
            folds.Add(new Tuple<char, int>(foldData[0].First(), int.Parse(foldData[1])));
        }
    }
}

var foldedGrid = Fold(grid, folds.First());

//Console.WriteLine("Point count after 1 fold: {0}", foldedGrid.GetPointsCount());

foreach (var f in folds)
{
    grid = Fold(grid, f);
}

Console.WriteLine(grid);

static Grid Fold(Grid grid, Tuple<char, int> fold)
{
    if (fold.Item1 == 'x')
        return grid.FoldX(fold.Item2);
    else if (fold.Item1 == 'y')
        return grid.FoldY(fold.Item2);

    return grid;
}