var data = File.ReadLines("input.txt");

var crabPositions = new List<int>();

foreach (var row in data)
    crabPositions.AddRange(row.Split(',').Select(d => int.Parse(d)));

crabPositions.Sort();

var optimalPositionMedian = crabPositions[crabPositions.Count / 2];

Console.WriteLine("Optimal position median: {0}", optimalPositionMedian);

Console.WriteLine("Median: {0}", CalculateFuelConsumptionPart1(crabPositions, optimalPositionMedian));

var consumptions = new List<int>();
for (var position = crabPositions.Min(); position <= crabPositions.Max(); position++)
    consumptions.Add(CalculateFuelConsumptionPart2(crabPositions, position));

Console.WriteLine("Minimume consumption: {0}", consumptions.Min());

static int CalculateFuelConsumptionPart1(List<int> crabs, int position)
{
    var totalFuelConsumption = 0;
    foreach (var p in crabs)
        totalFuelConsumption += Math.Abs(p - position);

    return totalFuelConsumption;
}

static int CalculateFuelConsumptionPart2(List<int> crabs, int position)
{
    var totalFuelConsumption = 0;
    foreach (var p in crabs)
        totalFuelConsumption += CalculateFuelConsumptionForMoves(Math.Abs(p - position));

    return totalFuelConsumption;
}

static int CalculateFuelConsumptionForMoves(int moves)
{
    var consumption = 0;
    for (var m = 1; m <= moves; m++)
        consumption += m;
    return consumption;
}

static int FindModus(List<int> list)
    => list
        .GroupBy(n => n)
        .Select(x => new { x.Key, Count = x.Count() })
        .OrderByDescending(x => x.Count)
        .First()
        .Key;