namespace Day4
{
    using System.Drawing;

    internal class BingoMatrix
    {
        private readonly Dictionary<int, Point> _numbers = new();

        private readonly List<int[]> _rows = new();

        private BingoGame _game;

        public BingoMatrix(BingoGame game)
        {
            _game = game;
        }

        public bool Won { get; private set; }

        public bool CallNumber(int number)
        {
            // Number is not in this matrix
            if (!_numbers.ContainsKey(number))
                return false;

            // Get the position of a number within matrix
            var position = _numbers[number];

            // Check column where the new number resides
            if (IsWinningColumn(position.X))
                return (Won = true);

            // Check row where the new number resides
            if (IsWinningRow(position.Y))
                return (Won = true);

            return false;
        }

        public void AddRow(string row)
        {
            // Get the row numbers
            var numbers = row.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToArray();

            // Add number to list numbers are added to dicitonary where number is the key and the value is position within matrix
            for (var column = 0; column < numbers.Length; column++)
            {
                _numbers.Add(numbers[column], new Point(column, _rows.Count));
            }

            _rows.Add(numbers);
        }

        public override string ToString()
        {
            var matrixString = string.Empty;
            foreach (var row in _rows)
            {
                foreach (var n in row)
                {
                    matrixString += n.ToString().PadRight(3, ' ');
                }
                matrixString += Environment.NewLine;
            }
            return matrixString;
        }

        public int GetUnmarkedSum()
        {
            var sum = 0;
            foreach (var r in _rows)
                foreach (var v in r)
                    if (!_game.WasNumberCalled(v))
                        sum += v;

            return sum;
        }

        public void PrintMatrix()
        {
            Console.WriteLine();
            foreach (var row in _rows)
            {
                foreach (var n in row)
                {
                    if (_game.WasNumberCalled(n))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(n.ToString().PadRight(3, ' '));
                    if (_game.WasNumberCalled(n))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine();
            }
        }

        private bool IsWinningRow(int row)
        {
            foreach (var n in _rows[row])
            {
                if (!_game.WasNumberCalled(n))
                    return false;
            }

            return true;
        }

        private bool IsWinningColumn(int column)
        {
            foreach (var r in _rows)
            {
                if (!_game.WasNumberCalled(r[column]))
                    return false;
            }

            return true;
        }
    }
}
