using Day5;
using System.Diagnostics;
using System.Drawing;
using System.Text;

var data = File.ReadLines("input.txt");

var parsedLines = new List<Line>();

foreach (var line in data)
{
    var points = line.Split(new string[] { "->", "," }, StringSplitOptions.TrimEntries).Select(p => int.Parse(p)).ToArray();

    Debug.Assert(points.Length == 4);

    parsedLines.Add(new Line(points[0], points[1], points[2], points[3]));
}

Console.WriteLine("# of dangerous points: {0}", CalculateDangerousPoints(parsedLines));

static int CalculateDangerousPoints(IEnumerable<Line> lines)
{
    var coveredPoints = new Dictionary<Point, int>();

    var maxX = 0;
    var maxY = 0;

    foreach (var line in lines)
    {
        // Part1
        //if (line.IsStraightLine())
        //{
            foreach (var p in line.GetAllPoints())
            {
                if (p.X > maxX) maxX = p.X;
                if (p.Y > maxY) maxY = p.Y;

                if (coveredPoints.ContainsKey(p))
                    coveredPoints[p] = coveredPoints[p] + 1;
                else
                    coveredPoints.Add(p, 1);
            }
        //}
    }

    DrawToFile("out.txt", coveredPoints, maxX, maxY);

    return coveredPoints.Count(p => p.Value > 1);
}

static void DrawToConsole(Dictionary<Point, int> points, int maxX, int maxY)
{
    for (var y = 0; y <= maxY; y++)
    {
        for (var x = 0; x <= maxX; x++)
        {
            var point = new Point(x, y);
            if (points.ContainsKey(point))
            {
                Console.Write(points[point]);
            }
            else
            {
                Console.Write('.');
            }
        }
        Console.WriteLine();
    }
}

static void DrawToFile(string filePath, Dictionary<Point, int> points, int maxX, int maxY)
{
    var gridBuilder = new StringBuilder();
    for (var y = 0; y <= maxY; y++)
    {
        for (var x = 0; x <= maxX; x++)
        {
            var point = new Point(x, y);
            if (points.ContainsKey(point))
            {
                gridBuilder.Append(points[point]);
            }
            else
            {
                gridBuilder.Append('.');
            }
        }
        gridBuilder.Append(Environment.NewLine);
    }

    System.IO.File.WriteAllText(filePath, gridBuilder.ToString());
}