const int StepsCount = 100;
const int FlashLevel = 10;
const int Flashed = 11;

var lines = File.ReadLines("input.txt");

var octopusesGrid = lines.Select(l => l.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

var totalFlashCount = 0;

// Simulate 100 steps
var step = 1;
//for (step = 1; step <= StepsCount; step++)
//{
//    // Increase energy level for each octopus in grid
//    IncreaseEnergyLevel(octopusesGrid);

//    // Flash octopuses with intensity of 10
//    FlashOctopuses(octopusesGrid);

//    // Recovery flashed octopuses aka set intensity to 0 if they surpasesd 9
//    totalFlashCount += Recover(octopusesGrid);

//    // Draw first 10 steps to check if everythings alright
//    if (step <= 10)
//    {
//        Console.WriteLine("Step: {0}", step);
//        DrawGrid(octopusesGrid);
//        Console.WriteLine();
//    }
//}

//Console.WriteLine("Total flashes: {0}", totalFlashCount);

var flashCount = 0;
step = 0;
do
{
    step++;
    IncreaseEnergyLevel(octopusesGrid);
    FlashOctopuses(octopusesGrid);
    flashCount = Recover(octopusesGrid);
    
} while (flashCount != 100);

Console.WriteLine("Synchronized flash at step: {0}", step);

static void IncreaseEnergyLevel(int[][] octopuses)
{
    for (var row = 0; row < octopuses.Length; row++)
        for (var col = 0; col < octopuses[0].Length; col++)
            octopuses[row][col]++;
}

static void FlashOctopuses(int[][] octopuses)
{
    for (var row = 0; row < octopuses.Length; row++)
        for (var col = 0; col < octopuses[0].Length; col++)
            if (octopuses[row][col] == FlashLevel)
                FlashRec(row, col, octopuses);
}

static int Recover(int[][] octopuses)
{
    var flashCount = 0;
    for (var row = 0; row < octopuses.Length; row++)
        for (var col = 0; col < octopuses[0].Length; col++)
            if (octopuses[row][col] >= FlashLevel)
            {
                octopuses[row][col] = 0;
                flashCount++;
            }

    return flashCount;
}

static void FlashRec(int row, int col, int[][] octopuses)
{
    octopuses[row][col] = Flashed;

    // Top octopus
    if (row != 0 && octopuses[row - 1][col] != FlashLevel)
    {
        octopuses[row - 1][col]++;
        if (octopuses[row - 1][col] == FlashLevel)
            FlashRec(row - 1, col, octopuses);
    }

    // Bottom octopus
    if (row != octopuses.Length - 1 && octopuses[row + 1][col] != FlashLevel)
    {
        octopuses[row + 1][col]++;
        if (octopuses[row + 1][col] == FlashLevel)
            FlashRec(row + 1, col, octopuses);
    }

    // Left octopus
    if (col != 0 && octopuses[row][col - 1] != FlashLevel)
    {
        octopuses[row][col - 1]++;
        if (octopuses[row][col - 1] == FlashLevel)
            FlashRec(row, col - 1, octopuses);
    }

    // Right octopus
    if (col != octopuses[0].Length - 1 && octopuses[row][col + 1] != FlashLevel)
    {
        octopuses[row][col + 1]++;
        if (octopuses[row][col + 1] == FlashLevel)
            FlashRec(row, col + 1, octopuses);
    }

    // Diagonal topleft
    if (col != 0 && row != 0 &&  octopuses[row - 1][col - 1] != FlashLevel)
    {
        octopuses[row - 1][col - 1]++;
        if (octopuses[row - 1][col - 1] == FlashLevel)
            FlashRec(row - 1, col - 1, octopuses);
    }

    // Diagonal topright
    if (col != octopuses[0].Length - 1 && row != 0 && octopuses[row - 1][col + 1] != FlashLevel)
    {
        octopuses[row - 1][col + 1]++;
        if (octopuses[row - 1][col + 1] == FlashLevel)
            FlashRec(row - 1, col + 1, octopuses);
    }

    // Diagonal bottomleft
    if (col != 0 && row != octopuses.Length - 1 && octopuses[row + 1][col - 1] != FlashLevel)
    {
        octopuses[row + 1][col - 1]++;
        if (octopuses[row + 1][col - 1] == FlashLevel)
            FlashRec(row + 1, col - 1, octopuses);
    }

    // Diagonal bottomright
    if (col != octopuses[0].Length - 1 && row != octopuses.Length - 1 && octopuses[row + 1][col + 1] != FlashLevel)
    {
        octopuses[row + 1][col + 1]++;
        if (octopuses[row + 1][col + 1] == FlashLevel)
            FlashRec(row + 1, col + 1, octopuses);
    }
}

static void DrawGrid(int[][] grid)
{
    for (var row = 0; row < grid.Length; row++)
    {
        for (var col = 0; col < grid[0].Length; col++)
        {
            switch (grid[row][col])
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    break;

            }

            Console.Write(grid[row][col]);
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
    }
}