using System.Numerics;
using System.Text;

var lines = File.ReadLines("input_test.txt");

// Instructions extracted from file
var instructions = new Dictionary<string, string>();

// Starting word
var template = lines.First();

for (var line = 2; line < lines.Count(); line++)
{
    var split = lines.ElementAt(line).Split("->");
    instructions.Add(split[0].Trim(), split[1].Trim());
}

// First 10 steps for task 1
// Get alphabet from instructions
var alphabet = ExtractAlphabet(instructions);

// All possible 2character length words 
var words = ConstructPossibleWordsFromAlphabet(alphabet);

// Try to get frequencies for each 2 letter word after 10 turns
var frequenciesAfter10 = new Dictionary<string, Dictionary<char, BigInteger>>();

var step = 1;
foreach (var word in words)
{
    step = 1;
    var tempWord = word;
    do
    {
        tempWord = ApplyInstrctions(tempWord, instructions);

    } while (step++ < 10);

    frequenciesAfter10.Add(word, CalculateCharacterFrequency(tempWord));
}

var letterFrequency = new Dictionary<char, BigInteger>();

for (var c = 0; c < template.Length - 1; c++)
{
    foreach (var f in frequenciesAfter10[$"{template[c]}{template[c + 1]}"])
    {
        if (!letterFrequency.ContainsKey(f.Key))
            letterFrequency.Add(f.Key, 0);
        letterFrequency[f.Key] += f.Value;
    }
}

foreach (var lf in letterFrequency)
    Console.WriteLine("{0}:{1}", lf.Key, lf.Value);

Console.ReadKey();

step = 1;
do
{
    Console.WriteLine("Step: {0}", step);

    template = ApplyInstrctions(template, instructions);

} while (step++ < 10);

// Get letter frequency for final sequence after 10 steps
letterFrequency = CalculateCharacterFrequency(template);

// Output final anaswer for part 1
Console.WriteLine("Max freq - Min freq: {0}", letterFrequency.Select(lf => lf.Value).Max() - letterFrequency.Select(lf => lf.Value).Min());

// PART2
// finalize 20 steps
do
{
    Console.WriteLine("Step: {0}", step);

    template = ApplyInstrctions(template, instructions);

} while (step++ < 20);



var frequenciesAfter20 = new Dictionary<string, Dictionary<char, BigInteger>>();

// How is freq. gonna look like after 20 steps for each possible 2 character word
foreach (var word in words)
{
    step = 1;
    var tempWord = word;
    do
    {
        tempWord = ApplyInstrctions(tempWord, instructions);

    } while (step++ < 20);

    frequenciesAfter20.Add(word, CalculateCharacterFrequency(tempWord));
}

letterFrequency.Clear();

for (var c = 0; c < template.Length - 1; c++)
{
    foreach (var f in frequenciesAfter20[$"{template[c]}{template[c + 1]}"])
    {
        if (!letterFrequency.ContainsKey(f.Key))
            letterFrequency.Add(f.Key, 0);

        letterFrequency[f.Key] += f.Value;
    }
}

foreach (var l in template)
    letterFrequency[l]--;

Console.WriteLine();
Console.WriteLine("Solution for part2:");

// Freq after turn 40?
foreach (var f in letterFrequency)
    Console.WriteLine("{0}:{1}", f.Key, f.Value);

// Output final anaswer for part 2
Console.WriteLine("Max freq - Min freq: {0}", letterFrequency.Select(lf => lf.Value).Max() - letterFrequency.Select(lf => lf.Value).Min());

static string ApplyInstrctions(string template, Dictionary<string, string> instructions)
{
    var stringBuilder = new StringBuilder();

    for (var i = 0; i < template.Length; i++)
    {
        stringBuilder.Append(template[i]);
        if (i < template.Length - 1 && instructions.ContainsKey(template.Substring(i, 2)))
        {
            stringBuilder.Append(instructions[template.Substring(i, 2)]);
        }
    }

    return stringBuilder.ToString();
}

static Dictionary<char, BigInteger> CalculateCharacterFrequency(string template)
{
    var freq = new Dictionary<char, BigInteger>();
    foreach (var c in template)
    {
        if (!freq.ContainsKey(c))
            freq.Add(c, 0);

        freq[c]++;
    }

    return freq;
}

static char[] ExtractAlphabet(Dictionary<string, string> instructions)
{
    var alphabet = new HashSet<char>();
    foreach (var instruction in instructions)
    {
        foreach (var c in instruction.Key) alphabet.Add(c);
        foreach (var c in instruction.Value) alphabet.Add(c);
    }

    return alphabet.ToArray();
}

static string[] ConstructPossibleWordsFromAlphabet(char[] alphabet)
{
    var words = new HashSet<string>();
    foreach (var l1 in alphabet)
        foreach (var l2 in alphabet)
            words.Add($"{l1}{l2}");

    return words.ToArray();
}