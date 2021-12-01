using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    internal class Window
    {
        public Window()
        {
            Values = new int?[3];
        }

        public int?[] Values { get; private set; }

        public void AddValue(int value)
        {
            Values[2] = Values[1];
            Values[1] = Values[0];
            Values[0] = value;
        }

        public int GetSum()
            => (Values[0] ?? 0) + (Values[1] ?? 0) + (Values[2] ?? 0);

        public bool IsValid()
            => Values[0].HasValue && Values[1].HasValue && Values[2].HasValue;
    }
}
