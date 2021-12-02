// See https://aka.ms/new-console-template for more information
// foreach (var line in System.IO.File.ReadLines(""))
using (var fileStream = new FileStream("input.txt", FileMode.Open, FileAccess.Read))
using (var streamReader = new StreamReader(fileStream))
{
    string line = string.Empty;

    var horizontal = 0;
    var depth = 0;
    var aim = 0;

    while ((line = streamReader.ReadLine())
        != null)
    {
        // As i can see the input set values are always less than 10
        // so i can read just the last character of a line for the value

        // same for the direction i can check just the first character of the line (f, d, u)
        if (int.TryParse(line.Last().ToString(), out int value))
        {
            switch (line.First())
            {
                case 'f':
                    horizontal += value;
                    depth += (aim * value);
                    break;
                case 'u':
                    //depth -= value;
                    aim -= value;
                    break;
                case 'd':
                    //depth += value; 
                    aim += value;
                    break;
                default:
                    break;
            }
        }
    }

    Console.WriteLine("Horizontal: {0}, Depth: {1}, Multiplied: {2}", horizontal, depth, horizontal * depth);
}