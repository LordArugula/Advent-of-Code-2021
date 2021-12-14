namespace AdventOfCode2021;

public static class Day10
{
    private static readonly Dictionary<char, int> ScoreMap = new Dictionary<char, int>
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };

    private static readonly Dictionary<char, char> SymbolPairs = new Dictionary<char, char>
    {
        { '(', ')' },
        { ')', '(' },
        { '[', ']' },
        { ']', '[' },
        { '{', '}' },
        { '}', '{' },
        { '<', '>' },
        { '>', '<' }
    };

    private static readonly HashSet<char> OpenSymbols = new HashSet<char>
    {
        '(',
        '[',
        '{',
        '<'
    };

    private static readonly HashSet<char> CloseSymbols = new HashSet<char>
    {
        ')',
        ']',
        '}',
        '>'
    };

    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(10);

        int totalSyntaxErrorScore = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            string line = inputs[i];
            totalSyntaxErrorScore += GetSyntaxErrorScore(line);
        }
        Console.WriteLine(totalSyntaxErrorScore);
    }

    private static int GetSyntaxErrorScore(string line)
    {
        Stack<char> groups = new Stack<char>();

        for (int j = 0; j < line.Length; j++)
        {
            char symbol = line[j];

            if (IsOpenSymbol(symbol))
            {
                // new level of nesting
                groups.Push(symbol);
            }
            else
            {
                // close nesting
                if (groups.TryPeek(out char prevSymbol))
                {
                    if (SymbolPairs[prevSymbol] == symbol)
                    {
                        groups.Pop();
                    }
                    else
                    {
                        return ScoreMap[symbol];
                    }
                }
            }
        }

        return 0;
    }

    private static bool IsOpenSymbol(char symbol)
    {
        return OpenSymbols.Contains(symbol);
    }

    private static bool IsCloseSymbol(char symbol)
    {
        return CloseSymbols.Contains(symbol);
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(10);

    }
}
