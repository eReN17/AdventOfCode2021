// See https://aka.ms/new-console-template for more information
using Day1;

var increasedCount = 0;
var increasedWindowsCount = 0;

// PART ONE & PART TWO
// foreach (var line in System.IO.File.ReadLines(""))
using (var fileStream = new FileStream("input.txt", FileMode.Open, FileAccess.Read))
using (var streamReader = new StreamReader(fileStream))
{
    string line = string.Empty;
    int? previouseValue = null;

    Window window1 = new Window();
    Window window2 = new Window();

    while ((line = streamReader.ReadLine())
        != null)
    {
        if (int.TryParse(line, out int value))
        {
            if (previouseValue.HasValue)
                window2.AddValue(value);

            if (window1.IsValid() && window2.IsValid())
                if (window1.GetSum() < window2.GetSum())
                    increasedWindowsCount++;

            window1.AddValue(value);

            if (previouseValue.HasValue && previouseValue.Value < value)
            {
                increasedCount++;
            }
            previouseValue = value;
        }
    }
}

Console.WriteLine("Value incresed: {0} times.", increasedCount);
Console.WriteLine("Window value incresed: {0} times.", increasedWindowsCount);