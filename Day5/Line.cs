using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Line
    {
        public Line(int x1, int y1, int x2, int y2)
        {
            FromPoint = new Point(x1, y1);
            ToPoint = new Point(x2, y2);
        }

        public Point FromPoint { get; private set; }
        public Point ToPoint { get; private set; }

        public bool IsStraightLine()
            => FromPoint.X == ToPoint.X || FromPoint.Y == ToPoint.Y;

        public override string ToString()
            => $"{FromPoint.X},{FromPoint.Y} -> {ToPoint.X},{ToPoint.Y}";

        public IEnumerable<Point> GetAllPoints()
        {
            var points = new List<Point>();
            points.Add(FromPoint);
            points.Add(ToPoint);

            if (FromPoint.X == ToPoint.X)
            {
                var y = ToPoint.Y > FromPoint.Y ? 1 : -1;
                while(ToPoint.Y != FromPoint.Y + y)
                {
                    points.Add(new Point(FromPoint.X, FromPoint.Y + y));

                    if (ToPoint.Y > FromPoint.Y)
                        y++;
                    else
                        y--;
                }
            }
            else if (FromPoint.Y == ToPoint.Y)
            {
                var x = ToPoint.X > FromPoint.X ? 1 : -1;
                while (ToPoint.X != FromPoint.X + x)
                {
                    points.Add(new Point(FromPoint.X + x, FromPoint.Y));

                    if (ToPoint.X > FromPoint.X)
                        x++;
                    else
                        x--;
                }
            }
            else
            {
                // is 45 diagonal
                var x = ToPoint.X > FromPoint.X ? 1 : -1;
                var y = ToPoint.Y > FromPoint.Y ? 1 : -1;

                while (ToPoint.X != FromPoint.X + x && ToPoint.Y != FromPoint.Y + y)
                {
                    points.Add(new Point(FromPoint.X + x, FromPoint.Y + y));

                    if (ToPoint.X > FromPoint.X)
                        x++;
                    else
                        x--;

                    if (ToPoint.Y > FromPoint.Y)
                        y++;
                    else
                        y--;
                }
            }

            return points;
        }
    }
}
