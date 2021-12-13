namespace AdventOfCode2021;

public static class Day8
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(8);

        int count = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            ReadOnlySpan<char> inputAsSpan = inputs[i].AsSpan();
            int delimIndex = inputAsSpan.IndexOf('|');
            string[] outputDigits = inputAsSpan[(delimIndex + 1)..]
                .ToString()
                .Split(' ');
            for (int j = 0; j < outputDigits.Length; j++)
            {
                if (outputDigits[j].Length == 2
                    || outputDigits[j].Length == 3
                    || outputDigits[j].Length == 4
                    || outputDigits[j].Length == 7)
                {
                    count++;
                }
            }
        }

        Console.Write(count);
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(8);
        for (int i = 0; i < inputs.Length; i++)
        {
            NewMethod(inputs[i]);
        }
    }

