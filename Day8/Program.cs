using Day8;

var digits = new List<Digit>();

foreach (var line in File.ReadLines("input.txt"))
{
    digits.Add(new Digit(line));
}

Console.WriteLine("Part1: {0}", digits.Sum(d => d.GetPart1()));

Console.WriteLine("Part2: {0}", digits.Sum(d => d.GetPart2()));