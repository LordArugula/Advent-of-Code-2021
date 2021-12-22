namespace AdventOfCode2021;

public static class Day21
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(21);

        DiracPlayer player1 = new DiracPlayer(ParseStartingPosition(inputs[0]));
        DiracPlayer player2 = new DiracPlayer(ParseStartingPosition(inputs[1]));

        int dieRolls = 0;
        while (true)
        {
            player1.Position += Roll3Die(dieRolls);
            player1.Score += player1.Position;
            dieRolls += 3;

            if (player1.Score >= 1000)
            {
                break;
            }

            player2.Position += Roll3Die(dieRolls);
            player2.Score += player2.Position;
            dieRolls += 3;

            if (player2.Score >= 1000)
            {
                break;
            }
        }

        Console.WriteLine(Math.Min(player1.Score, player2.Score) * dieRolls);
    }

    private static int Roll3Die(int dieRolls)
    {
        int roll1 = dieRolls + 1;
        if (roll1 > 100)
        {
            roll1 -= 100;
        }
        int roll2 = dieRolls + 2;
        if (roll2 > 100)
        {
            roll2 -= 100;
        }
        int roll3 = dieRolls + 3;
        if (roll3 > 100)
        {
            roll3 -= 100;
        }
        return roll1 + roll2 + roll3;
    }

    private static int ParseStartingPosition(string input)
    {
        ReadOnlySpan<char> span = input.AsSpan();
        int index = span.LastIndexOf(' ') + 1;
        return int.Parse(span[index..]);
    }

    private class DiracPlayer
    {
        private int position;

        public int Position
        {
            get => position;
            set
            {
                position = value;
                while (position > 10)
                {
                    position -= 10;
                }
            }
        }

        public int Score { get; set; }

        public DiracPlayer(int position) : this(position, 0)
        { }

        public DiracPlayer(int position, int score)
        {
            Position = position;
            Score = score;
        }
    }

    private static readonly (int roll, int count)[] DieRollMap
        = new (int roll, int count)[]
        {
            ( 3, 1 ),
            ( 4, 3 ),
            ( 5, 6 ),
            ( 6, 7 ),
            ( 7, 6 ),
            ( 8, 3 ),
            ( 9, 1 ),
        };

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(21);

        int positionA = ParseStartingPosition(inputs[0]);
        int positionB = ParseStartingPosition(inputs[1]);

        DiracPlayer a = new DiracPlayer(positionA);
        DiracPlayer b = new DiracPlayer(positionB);

        Dictionary<(DiracPlayer, DiracPlayer), (long, long)> universesMap
            = new Dictionary<(DiracPlayer, DiracPlayer), (long, long)>();

        (long winsA, long winsB) = CountWins(a, b, universesMap);
        Console.WriteLine(Math.Max(winsA, winsB));
    }

    private static (long, long) CountWins(DiracPlayer current,
        DiracPlayer next,
        Dictionary<(DiracPlayer, DiracPlayer), (long, long)> universesMap)
    {
        if (universesMap.ContainsKey((current, next)))
        {
            return universesMap[(current, next)];
        }

        (long currWins, long nextWins) wins = (0L, 0L);

        foreach ((int roll, int count) in DieRollMap)
        {
            DiracPlayer player = new DiracPlayer(current.Position, current.Score);
            player.Position += roll;
            player.Score += player.Position;

            if (player.Score >= 21)
            {
                wins.currWins += count;
            }
            else
            {
                (long nextWins, long currWins) = CountWins(next, player, universesMap);
                wins.currWins += count * currWins;
                wins.nextWins += count * nextWins;
            }
        }
        universesMap.TryAdd((current, next), wins);
        return wins;
    }
}
