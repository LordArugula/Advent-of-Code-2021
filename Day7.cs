namespace AdventOfCode2021;

public static class Day7
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(7);
        IEnumerable<int> crabPositions = inputs[0]
            .Split(',')
            .Select(x => int.Parse(x));

        int bestPosition = -1;
        int bestCost = int.MaxValue;

        for (int position = 0; position < crabPositions.Max(); position++)
        {
            int cost = 0;
            foreach (int crab in crabPositions)
            {
                cost += Math.Abs(crab - position);
            } 

            if (cost < bestCost)
            {
                bestCost = cost;
                bestPosition = position;
            }
        }
        Console.WriteLine(bestCost);
    }

    public static void Part2()
    {

        string[] inputs = InputHelper.GetInput(7);
        IEnumerable<int> crabPositions = inputs[0]
            .Split(',')
            .Select(x => int.Parse(x));

        int bestPosition = -1;
        int bestCost = int.MaxValue;

        for (int position = 0; position < crabPositions.Max(); position++)
        {
            int cost = 0;
            foreach (int crab in crabPositions)
            {
                int distance = Math.Abs(crab - position);
                cost += (distance * (distance + 1)) / 2;
            }

            if (cost < bestCost)
            {
                bestCost = cost;
                bestPosition = position;
            }
        }
        Console.WriteLine(bestCost);
    }
}