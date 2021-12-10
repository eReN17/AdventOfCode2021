using System.Diagnostics;
using System.Numerics;
using System.Text;

var syntaxErrorPoints = new Dictionary<char, int>()
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 }
};

var lines = File.ReadLines("input.txt");

var scores = new List<BigInteger>();

var total = 0;
foreach (var line in lines)
{
    var corrupted = false;
    var brackets = new Stack<char>();

    foreach (var c in line)
    {
        switch (c)
        {
            case '(':
            case '[':
            case '{':
            case '<':
                brackets.Push(c);
                break;
            case ')':
            case ']':
            case '}':
            case '>':
                if (!IsMatch(brackets.Pop(), c))
                {
                    total += syntaxErrorPoints[c];
                    corrupted = true;
                }

                break;
            default:
                break;
        }
    }

    if (!corrupted)
    {
        var completionSequence = string.Empty;
        while (brackets.Count > 0)
            completionSequence += GetPair(brackets.Pop());

        var score = CalculateCompletionScore(completionSequence);

        Debug.Assert(score > 0);

        scores.Add(score);
    }
}

scores.Sort();

Console.WriteLine("total syntax error score: {0}", total);

Console.WriteLine("Middle completion score: {0}", scores[scores.Count / 2]);

static bool IsMatch(char opening, char closing)
    =>  opening == '(' && closing == ')' ||
        opening == '[' && closing == ']' ||
        opening == '{' && closing == '}' ||
        opening == '<' && closing == '>';

static char GetPair(char opening)
    => opening == '(' ? ')' : (opening == '[' ? ']' : (opening == '{' ? '}' : '>'));
        

static BigInteger CalculateCompletionScore(string completionSequence)
{
    var completionPoints = new Dictionary<char, int>()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 }
    };

    var score = BigInteger.Zero;
    foreach (var c in completionSequence)
        score = (score * 5) + completionPoints[c];
    
    return score;
}