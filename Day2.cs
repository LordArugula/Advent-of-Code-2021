namespace AdventOfCode2021;

public static class Day2
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(2);

        int horizontal = 0;
        int depth = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            string[] commandArgs = inputs[i].Split(' ');

            switch (commandArgs[0])
            {
                case "forward":
                    horizontal += int.Parse(commandArgs[1]);
                    break;
                case "up":
                    depth -= int.Parse(commandArgs[1]);
                    break;
                case "down":
                    depth += int.Parse(commandArgs[1]);
                    break;
            }
        }

        Console.WriteLine(horizontal * depth);
    }

    public static void Part2()
    {

        string[] inputs = InputHelper.GetInput(2);

        int horizontal = 0;
        int depth = 0;
        int aim = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            string[] commandArgs = inputs[i].Split(' ');

            int val = int.Parse(commandArgs[1]);
            switch (commandArgs[0])
            {
                case "forward":
                    horizontal += val;
                    depth += val * aim;
                    break;
                case "up":
                    aim -= val;
                    break;
                case "down":
                    aim += val;
                    break;
            }
        }

        Console.WriteLine(horizontal * depth);
    }
}
