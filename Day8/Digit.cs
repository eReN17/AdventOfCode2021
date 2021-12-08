using System.Diagnostics;

namespace Day8
{
    internal class Digit
    {
        private char _top;
        private char _bottom;
        private char _middle;
        private char _topLeft;
        private char _topRight;
        private char _bottomLeft;
        private char _bottomRight;

        private string _n9 = string.Empty;

        private readonly string[] _signalPattern;
        private readonly string[] _outputValue;

        public Digit(string entry)
        {
            _signalPattern = entry.Split('|')[0].Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).ToArray();
            _outputValue = entry.Split('|')[1].Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).ToArray();

            Parse();
        }

        private void Parse()
        {
            // so terrible but it works :O
            string n0 = string.Empty, n1, n2, n3, n4, n5, n6, n7, n8;

            // Number 1 characters representation
            n1 = (from d in _signalPattern where d.Length == 2 select d).Single();

            // Number 7 characters representation
            n7 = (from d in _signalPattern where d.Length == 3 select d).Single();
            // Character within 7 that is not in 1 is _top
            foreach (var c in n7)
                if (!n1.Contains(c))
                    _top = c;

            // Number 0,6 and 9 all have 6 lines in them
            foreach (var pattern in _signalPattern.Where(p => p.Length == 6))
            {
                if (pattern.Contains(n1) || pattern.Contains($"{n1[1]}{n1[0]}"))
                    n0 = pattern;
            }

            // Number 4 characters representation
            // from that i can get  _topLeft and middle
            n4 = (from d in _signalPattern where d.Length == 4 select d).Single();
            foreach (var c in n4)
            {
                if (!n0.Contains(c))
                {
                    _middle = c;
                    
                }
                else if (!n1.Contains(c))
                {
                    _topLeft = c;
                }
            }

            // Number 2,5 and 3 all have 5 lines in them
            // 3 is most easy if it contains both lines from number 1
            foreach (var pattern in _signalPattern.Where(p => p.Length == 5))
            {
                if (pattern.Contains(_topLeft))
                {
                    n5 = pattern;
                    foreach (var c in pattern)
                        if (c != _top && c != _topLeft && c != _middle)
                            if (n1.Contains(c))
                            {
                                _bottomRight = c;
                            }
                            else
                            {
                                _bottom = c;
                            }
                }
            }

            // Number 8 characters representation
            n8 = (from d in _signalPattern where d.Length == 7 select d).Single();


            foreach (var pattern in _signalPattern.Where(p => p.Length == 6 && !p.Contains(_middle)))
            {
                foreach (var c in n1)
                {
                    if (!pattern.Contains(c))
                    {
                        n6 = pattern;
                        continue;
                    }
                }
                _n9 = new string(pattern.ToCharArray().OrderBy(c => c).ToArray());
            }
        }

        public int GetPart2()
        {
            var output = string.Empty;
            foreach (var value in _outputValue)
            {
                output += GetDigit(value).ToString();
            }
            return int.Parse(output);
        }

        public int GetDigit(string value)
        {
            if (value.Length == 2)
                return 1;
            else if (value.Length == 3)
                return 7;
            else if (value.Length == 4)
                return 4;
            else if (value.Length == 7)
                return 8;
            else if (value.Length == 5)
            {
                // 2,5,3
                if (value.Contains(_topLeft))
                    return 5;
                else if (value.Contains(_bottomRight))
                    return 3;
                else
                    return 2;
            }
            else 
            {
                Debug.Assert(value.Length == 6);

                // 0,6,9
                if (!value.Contains(_middle))
                    return 0;
                else
                {
                    return (_n9 == new string(value.ToCharArray().OrderBy(c => c).ToArray()) ? 9 : 6);
                }
            }
        }

        public int GetPart1()
            => _outputValue
                .Count(o => new[] { 2, 4, 3, 7 }.Contains(o.Length));
    }
}
