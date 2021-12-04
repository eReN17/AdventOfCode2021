namespace Day4
{
    internal class BingoGame
    {
        private readonly List<BingoMatrix> _bingoMatrices = new();

        private readonly Queue<int> _numbers;

        private readonly HashSet<int> _calledNumbers = new();

        public BingoGame(string filePath)
        {
            // get all the data from input file
            var data = File.ReadLines(filePath);

            // get called numbers and store them in a queue
            _numbers = new Queue<int>(data.First().Split(',').Select(n => int.Parse(n)));

            // Generate matricies from rest of the data
            var matrix = new BingoMatrix(this);
            for (var rowIndex = 2; rowIndex < data.Count(); rowIndex++)
            {
                if (string.IsNullOrWhiteSpace(data.ElementAt(rowIndex)))
                {
                    // Matricies are separated by empty line, found an empty line gonna store matrix a initialize new
                    _bingoMatrices.Add(matrix);
                    matrix = new BingoMatrix(this);
                }
                else
                {
                    matrix.AddRow(data.ElementAt(rowIndex));
                }
            }

            _bingoMatrices.Add(matrix);
        }

        internal bool WasNumberCalled(int number)
            => _calledNumbers.Contains(number);

        public int LastCalledNumber { get; private set; }

        public void PrintAll()
        {
            foreach (var m in _bingoMatrices)
                m.PrintMatrix();
        }

        public BingoMatrix? CallNextNumber()
        {
            // dequeue next number 
            LastCalledNumber = _numbers.Dequeue();

            // Add called number to set of called numbers
            _calledNumbers.Add(LastCalledNumber);

            // mark the called number in each matrix 
            foreach (var m in _bingoMatrices)
            {
                // If call number returns true that means bingo was completed for that matrix
                if (m.CallNumber(LastCalledNumber))
                {
                    //return m; PART1
                    if (_bingoMatrices.Count(m => m.Won) == _bingoMatrices.Count)
                        return m;
                }
            }

            return null;
        }
    }
}
