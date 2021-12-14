namespace AdventOfCode2021;

public static class Day11
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(11);

        int[,] energyLevels = BuildEnergyLevelMap(inputs);

        int totalFlashes = 0;
        int steps = 100;
        while (steps > 0)
        {
            steps--;
            EnergizeOctopi(energyLevels);
            totalFlashes += FlashOctopi(energyLevels);
        }

        Console.WriteLine(totalFlashes);
    }

    private static int FlashOctopi(int[,] energyLevels)
    {
        int width = energyLevels.GetLength(0);
        int length = energyLevels.GetLength(1);

        int totalFlashes = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (energyLevels[x, y] > 9)
                {
                    energyLevels[x, y] = 0;
                    totalFlashes++;
                }
            }
        }

        return totalFlashes;
    }

    private static int[,] BuildEnergyLevelMap(string[] inputs)
    {
        int width = inputs.Length;
        int length = inputs[0].Length;

        int[,] energyLevels = new int[width, length];

        for (int x = 0; x < width; x++)
        {
            string input = inputs[x];
            for (int y = 0; y < length; y++)
            {
                energyLevels[x, y] = input[y] - '0';
            }
        }

        return energyLevels;
    }

    private static void EnergizeOctopi(int[,] energyLevels)
    {
        int width = energyLevels.GetLength(0);
        int length = energyLevels.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                energyLevels[x, y]++;
                if (energyLevels[x, y] == 10)
                {
                    EnergizeNeighbors(energyLevels, x, y);
                }
            }
        }
    }

    private static readonly (int x, int y)[] Neighbors = new (int x, int y)[]
    {
        (-1, +1), (+0, +1), (+1, +1),
        (-1, +0),           (+1, +0),
        (-1, -1), (+0, -1), (+1, -1)
    };

    private static void EnergizeNeighbors(int[,] energyLevels, int x, int y)
    {
        for (int i = 0; i < Neighbors.Length; i++)
        {
            int nx = x + Neighbors[i].x;
            int ny = y + Neighbors[i].y;
            if (nx >= 0 && nx < energyLevels.GetLength(0)
                && ny >= 0 && ny < energyLevels.GetLength(1))
            {
                energyLevels[nx, ny]++;
                if (energyLevels[nx, ny] == 10)
                {
                    EnergizeNeighbors(energyLevels, nx, ny);
                }
            }
        }
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(11);
        int[,] energyLevels = BuildEnergyLevelMap(inputs);

        int stepCounter = 0;
        while (true)
        {
            stepCounter++;
            EnergizeOctopi(energyLevels);
            int flashes = FlashOctopi(energyLevels);
            if (flashes == energyLevels.Length)
            {
                Console.WriteLine(stepCounter);
                break;
            }
        }
    }
}
