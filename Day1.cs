namespace AdventOfCode2021;

public static class Day1
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(1);

        int increases = 0;
        int prevMeasurement = int.MaxValue;
        for (int i = 0; i < inputs.Length; i++)
        {
            int input = int.Parse(inputs[i]);
            if (prevMeasurement < input)
            {
                increases++;
            }
            prevMeasurement = input;
        }

        Console.WriteLine(increases);
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(1);

        int increases = 0;
        int prevMeasurement = int.MaxValue;
        for (int i = 0; i < inputs.Length; i++)
        {
            int windowMeasurement = 0;
            for (int a = i; a < i + 3 && a < inputs.Length; a++)
            {
                windowMeasurement += int.Parse(inputs[a]);
            }
            
            if (prevMeasurement < windowMeasurement)
            {
                increases++;
            }
            prevMeasurement = windowMeasurement;
        }

        Console.WriteLine(increases);
    }
}