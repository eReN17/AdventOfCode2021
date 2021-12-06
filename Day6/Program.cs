using Day6;
using System.Numerics;

var data = File.ReadLines("input.txt");

var populationBasedOnTimer = new Dictionary<int, List<Lanternfish>>();

for (var variant = 0; variant <= 100; variant++)
{
    var l = new Lanternfish(variant);

    var fishPopulation = new List<Lanternfish>();
    var newPopulation = new List<Lanternfish>();

    fishPopulation.Add(l);

    for (var day = 1; day <= 128; day++)
    {
        foreach (var fish in fishPopulation)
        {
            var newfish = fish.Tick();

            if (newfish != null)
            {
                newPopulation.Add(newfish);
            }
        }

        fishPopulation.AddRange(newPopulation);
        newPopulation.Clear();
    }

    Console.WriteLine("Variant #{0}, population: {1}", variant, fishPopulation.Count);
    populationBasedOnTimer.Add(variant, fishPopulation);
}

var finalCount = BigInteger.Zero;

foreach (var row in data)
    foreach (var value in row.Split(',').Select(d => int.Parse(d)))
    {
        var populationAfter128Days = populationBasedOnTimer[value];
        foreach (var fish in populationAfter128Days)
            finalCount += populationBasedOnTimer[fish.InternalTimer].Count;
    }

Console.WriteLine("Lanternfish population after 256 days: {0}", finalCount);