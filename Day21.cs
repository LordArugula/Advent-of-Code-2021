namespace AdventOfCode2021;

public static class Day21
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(21);
        int player1Position = ParseStartingPosition(inputs[0]);
        int player2Position = ParseStartingPosition(inputs[1]);

        int player1Score = 0;
        int player2Score = 0;

        int dieRolls = 0;
        while (true)
        {
            player1Position += Roll3Die(dieRolls);
            while (player1Position > 10)
            {
                player1Position -= 10;
            }

            player1Score += player1Position;
            dieRolls += 3;

            if (player1Score >= 1000)
            {
                break;
            }

            player2Position += Roll3Die(dieRolls);
            while (player2Position > 10)
            {
                player2Position -= 10;
            }

            player2Score += player2Position;
            dieRolls += 3;

            if (player2Score >= 1000)
            {
                break;
            }
        }

        Console.WriteLine(Math.Min(player1Score, player2Score) * dieRolls);
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

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(21);

    }
}
