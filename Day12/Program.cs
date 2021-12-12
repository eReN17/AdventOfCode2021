using Day12;

const string Start = "start";
const string End = "end";

var lines = File.ReadLines("input.txt");

var connections = ConstructConnections(lines);

var paths = new HashSet<string>();

foreach (var start in from p in connections where p.Key == Start select p)
{
    var connection = new Connection();

    connection.AddNode(start.Key);
    connection.AddNode(start.Value);

    ConstructPathsRecursive(connection, paths, connections);
}

foreach (var p in paths)
{
    Console.WriteLine(p);
}

Console.WriteLine("Constructed paths {0}:", paths.Count);

static void ConstructPathsRecursive(Connection connection, HashSet<string> paths, List<KeyValuePair<string, string>> connections)
{
    var connectedNodes = connections.Where(p => p.Key == connection.GetLastNode()).ToArray();

    foreach (var n in connectedNodes)
    {
        var clonedConnection = connection.CloneMe();

        if (n.Value == End)
        {
            clonedConnection.AddNode(n.Value);
            paths.Add(clonedConnection.ToString());
        }
        else if (clonedConnection.AddNode(n.Value))
        {
            
            ConstructPathsRecursive(clonedConnection, paths, connections);
        }
    }
}

static List<KeyValuePair<string, string>> ConstructConnections(IEnumerable<string> lines)
{
    var connections = new List<KeyValuePair<string, string>>();

    foreach (var line in lines)
    {
        var lineSplit = line.Split('-');

        if (lineSplit[0] == Start || lineSplit[1] == Start)
        {
            connections.Add(new KeyValuePair<string, string>(
                lineSplit[0] == Start ? lineSplit[0] : lineSplit[1],
                lineSplit[0] == Start ? lineSplit[1] : lineSplit[0]
            ));
        }
        else if (lineSplit[0] == End || lineSplit[1] == End)
        {
            connections.Add(new KeyValuePair<string, string>(
                lineSplit[0] == End ? lineSplit[1] : lineSplit[0],
                lineSplit[0] == End ? lineSplit[0] : lineSplit[1]
            ));
        }
        else
        {
            connections.Add(new KeyValuePair<string, string>(lineSplit[0], lineSplit[1]));
            connections.Add(new KeyValuePair<string, string>(lineSplit[1], lineSplit[0]));
        }
    }

    return connections;
}