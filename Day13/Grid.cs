namespace Day13
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    internal class Grid
    {
        private int _maxX, _maxY;

        private readonly HashSet<Point> _points
            = new HashSet<Point>();

        public void AddPoint(int x, int y)
        {
            if (_maxX < x)
                _maxX = x;

            if (_maxY < y)
                _maxY = y;

            _points.Add(new Point(x, y));
        }

        public Grid FoldX(int x)
        {
            var foldedGrid = new Grid();

            foreach (var p in _points)
            {
                if (p.X > x)
                {
                    foldedGrid.AddPoint(x - (p.X - x), p.Y);
                }
                else if (p.X < x)
                {
                    foldedGrid.AddPoint(p.X, p.Y);
                }
            }

            return foldedGrid;
        }

        public Grid FoldY(int y)
        {
            var foldedGrid = new Grid();

            foreach (var p in _points)
            {
                if (p.Y > y)
                {
                    foldedGrid.AddPoint(p.X, y - (p.Y - y));
                }
                else if (p.Y < y)
                {
                    foldedGrid.AddPoint(p.X, p.Y);
                }
            }

            return foldedGrid;
        }

        public int GetPointsCount()
            => _points.Count;

        public override string ToString()
        {
            var grid = new StringBuilder();
            for (var y = 0; y <= _maxY; y++)
            {
                for (var x = 0; x <= _maxX; x++)
                    grid.Append( _points.Contains(new Point(x, y)) ? "#" : "." );

                grid.Append(Environment.NewLine);
            }

            return grid.ToString();
        }
    }
}
