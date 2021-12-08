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
            // im already sleeping this is bad...
            // it worked
            // still ashamed tho
            // need to come back one day

            var n0 = string.Empty;
            var n1 = (from d in _signalPattern where d.Length == 2 select d).Single();
            var n2 = string.Empty;
            var n3 = string.Empty;
            var n4 = (from d in _signalPattern where d.Length == 4 select d).Single();
            var n5 = string.Empty;
            var n6 = string.Empty;  
            var n8 = (from d in _signalPattern where d.Length == 7 select d).Single();
            var n7 = (from d in _signalPattern where d.Length == 3 select d).Single();
            var n9 = string.Empty;

            // Character within 7 that is not in 1 is _top
            foreach (var c in n7)
                if (!n1.Contains(c))
                    _top = c;

            // 2,3,5
            foreach (var pattern in _signalPattern.Where(p => p.Length == 5))
            {
                // contains both segment of number 1 means its 3
                if (pattern.Contains(n1[0]) && pattern.Contains(n1[1]))
                    n3 = pattern;
                else
                {
                    var count = 0;
                    foreach (var c in n4)
                        if (pattern.Contains(c))
                            count++;

                    // 2 has 2 segments common with 4, 5 has 3
                    if (count == 2)
                        n2 = pattern;
                    else
                        n5 = pattern;
                }
            }

            // I've got 2 and 5 already so i can set both _bottomRight and _topRight
            foreach (var c in n1)
                if (!n2.Contains(c))
                    _bottomRight = c;
                else if (!n5.Contains(c))
                    _topRight = c;

            // Number 0,6 and 9 all have 6 lines in them
            foreach (var pattern in _signalPattern.Where(p => p.Length == 6))
            {
                if (!pattern.Contains(_topRight))
                    n6 = pattern;
                else
                {
                    var count = 0;
                    foreach (var c in n3)
                        if (pattern.Contains(c))
                            count++;

                    if (count == 4)
                        n0 = pattern;
                    else
                        n9 = pattern;   
                }
            }

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

            foreach (var c in n3)
            {
                if (c != _bottomRight && c != _topRight && c != _top && c != _middle)
                    _bottom = c;
            }

            foreach (var c in n2)
                if (c != _top && c != _middle && c != _bottom && c != _topRight)
                    _bottomLeft = c;
        }
        private int GetDigit(string value)
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
                else if (value.Contains(_topRight))
                    return 9;
                else
                    return 6;
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

        public int GetPart1()
            => _outputValue
                .Count(o => new[] { 2, 4, 3, 7 }.Contains(o.Length));
    }
}
