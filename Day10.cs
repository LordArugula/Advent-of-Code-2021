namespace AdventOfCode2021;

public static class Day10
{
    private static readonly Dictionary<char, int> ErrorScoreMap = new Dictionary<char, int>
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };

    private static readonly Dictionary<char, ulong> AutoCompleteScoreMap = new Dictionary<char, ulong>
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 }
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
                        return ErrorScoreMap[symbol];
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

        List<ulong> autoCompleteScores = new List<ulong>();

        for (int i = 0; i < inputs.Length; i++)
        {
            string line = inputs[i];
            ulong score = GetAutoCompleteScore(line);
            if (score == 0)
            {
                continue;
            }
            autoCompleteScores.Add(score);
        }

        autoCompleteScores.Sort();
        Console.WriteLine(autoCompleteScores[autoCompleteScores.Count / 2]);
    }

    private static ulong GetAutoCompleteScore(string line)
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
                        // Corrupted
                        return 0;
                    }
                }
            }
        }

        IEnumerable<ulong> scores = groups
            .Select(symbol => AutoCompleteScoreMap[SymbolPairs[symbol]]);

        ulong totalScore = 0;
        foreach (ulong score in scores)
        {
            totalScore = 5u * totalScore + score;
        }
        return totalScore;
    }
}
