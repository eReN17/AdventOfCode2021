using System.Collections;
using System.Text;

var diagnosticData = File.ReadLines("input.txt");

Console.WriteLine("Power consumption: {0}", CalcualtePowerConsumption(diagnosticData));

Console.WriteLine("Life support: {0}", CalculateLifeSupport(diagnosticData));

static int CalcualtePowerConsumption(IEnumerable<string> diagnosticData)
{
    // Array for counting '1' for each position
    var onesCount = new int[diagnosticData.ElementAt(0).Length];

    // Count number of '1' for each position for each row in data
    Array.ForEach(diagnosticData.ToArray(), (string d) =>
    {
        for (var index = 0; index < d.Length; index++)
            if (d[index] == '1')
                onesCount[index]++;
    });

    // Calculate gamma and epsilon rate
    // Gamma: dominant bit ( 0,1 ) troughout the data input
    // Epsilon: reversed gamma 
    var gammaRate = new BitArray(onesCount.Length);
    var epsilonRate = new BitArray(onesCount.Length);
    for (var index = 0; index < gammaRate.Length; index++)
    {
        gammaRate[index] = onesCount[index] > diagnosticData.Count() / 2;
        epsilonRate[index] = !gammaRate[index];
    }

    return Multiply(gammaRate, epsilonRate);
}

static int CalculateLifeSupport(IEnumerable<string> diagnosticData)
{
    var oxygenGeneratorRating = FindOxygenGeneratorRatingRecursive(diagnosticData.ToArray(), 0);
    var co2ScrubberRating = FindCO2ScrubberRatingRecursive(diagnosticData.ToArray(), 0);

    var oxygenBits = new BitArray(oxygenGeneratorRating.Reverse().Select(s => s == '1').ToArray());
    var co2Bits = new BitArray(co2ScrubberRating.Reverse().Select(s => s == '1').ToArray());

    return Multiply(oxygenBits, co2Bits);
}

static string FindOxygenGeneratorRatingRecursive(string[] diagnosticData, int position)
{
    if (diagnosticData.Length == 1 || position - 1  == diagnosticData[0].Length)
        return diagnosticData.First();

    // Calculate #of '1' at position within provided data
    var onesCountAtPosition = 0;
    Array.ForEach(diagnosticData,
        (string d) => {
            if (d[position] == '1') onesCountAtPosition++; 
        });

    // Get the subset for diagnostic data. If there is more 1s @ position for data get those rows otherwise get those with 0
    var data = diagnosticData.Where(d => d[position] == (onesCountAtPosition >= diagnosticData.Count() / 2d ? '1' : '0')).ToArray();
    return FindOxygenGeneratorRatingRecursive(data, position + 1);
}

static string FindCO2ScrubberRatingRecursive(string[] diagnosticData, int position)
{
    if (diagnosticData.Length == 1 || position - 1 == diagnosticData[0].Length)
        return diagnosticData.First();

    // Calculate #of '1' at position within provided data
    var onesCountAtPosition = 0;
    Array.ForEach(diagnosticData,
        (string d) => {
            if (d[position] == '1') onesCountAtPosition++;
        });

    // Get the subset for diagnostic data. If there is more 1s @ position for data get rows with 0 at position otherwise get rows with 1 at position
    var data = diagnosticData.Where(d => d[position] == (onesCountAtPosition >= diagnosticData.Count() / 2d ? '0' : '1')).ToArray();
    return FindCO2ScrubberRatingRecursive(data, position + 1);
}

static int Multiply(BitArray array1, BitArray array2)
{
    var values = new int[2];

    array1.CopyTo(values, 0);
    array2.CopyTo(values, 1);

    return values[0] * values[1];
}