using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal class Lanternfish
    {
        private int _internalBase = 6;

        public Lanternfish(int timer)
            : this(timer, 6)
        {

        }

        public Lanternfish(int timer, int baseTimer)
        {
            InternalTimer = timer;
            _internalBase = baseTimer;
        }

        public int InternalTimer { get; private set; }

        public Lanternfish Tick()
        {
            if (InternalTimer == 0)
            {
                InternalTimer = _internalBase;
                return new Lanternfish(_internalBase + 2);
            }
            else
            {
                InternalTimer--;
                return null;
            }
        }
    }
}
