using Day4;

var bingo = new BingoGame("input.txt");

BingoMatrix winningMatrix = null;

while (winningMatrix is null)
{
    winningMatrix = bingo.CallNextNumber();
    Console.WriteLine("Calling number: {0}", bingo.LastCalledNumber);

}

Console.WriteLine();
Console.WriteLine("We have a winner and the final score is: {0}", (bingo.LastCalledNumber * winningMatrix.GetUnmarkedSum()));
winningMatrix.PrintMatrix();
