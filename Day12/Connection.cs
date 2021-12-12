namespace Day12
{
    internal class Connection : ICloneable
    {
        private readonly List<string> _nodes = new List<string>();

        private bool _smallCaveLock = false;

        private string _lastNode = string.Empty;

        public string GetLastNode()
            => _lastNode;

        public bool AddNode(string node)
        { 
            if (IsCaveSmall(node))
            {
                if (_nodes.Contains(node))
                {
                    if (_smallCaveLock)
                        return false;

                    _smallCaveLock = true;
                }
            }

            _lastNode = node;
            _nodes.Add(node);
            return true;
        }

        private bool IsCaveSmall(string cave)
        {
            foreach (var c in cave)
                if (c >= 'A' && c <= 'Z')
                    return false;

            return true;
        }

        public override string ToString()
            => string.Join("-", _nodes);

        public object Clone()
        {
            var connection = new Connection();

            foreach (var n in _nodes)
                connection.AddNode(n);

            return connection;
        }

        public Connection CloneMe()
            => (Connection)Clone();
    }
}