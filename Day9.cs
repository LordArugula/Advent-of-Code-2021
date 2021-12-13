namespace AdventOfCode2021;

public static class Day9
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(9);

        int width = inputs.Length;
        int length = inputs[0].Length;
        int[,] heightmap = new int[width, length];
        BuildHeightmap(heightmap, inputs);

        int totalRiskLevel = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (IsHeightLowerThanAdjacentNeighbors(heightmap, x, y))
                {
                    totalRiskLevel += heightmap[x, y] + 1;
                }
            }
        }

        Console.WriteLine(totalRiskLevel);
    }

    private static void BuildHeightmap(int[,] heightmap, string[] inputs)
    {
        int width = inputs.Length;
        int length = inputs[0].Length;

        for (int x = 0; x < width; x++)
        {
            string input = inputs[x];
            for (int y = 0; y < length; y++)
            {
                heightmap[x, y] = input[y] - '0';
            }
        }
    }

    private static bool IsHeightLowerThanAdjacentNeighbors(int[,] heightmap, int x, int y)
    {
        int height = heightmap[x, y];

        if (x + 1 < heightmap.GetLength(0)
            && heightmap[x + 1, y] <= height)
        {
            return false;
        }

        if (x - 1 >= 0
            && heightmap[x - 1, y] <= height)
        {
            return false;
        }

        if (y + 1 < heightmap.GetLength(1)
            && heightmap[x, y + 1] <= height)
        {
            return false;
        }

        if (y - 1 >= 0
            && heightmap[x, y - 1] <= height)
        {
            return false;
        }
        return true;
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(9);

        int width = inputs.Length;
        int length = inputs[0].Length;
        int[,] heightmap = new int[width, length];
        bool[,] flags = new bool[width, length];
        BuildHeightmap(heightmap, flags, inputs);

        List<int> basinSizes = new List<int>(capacity: 3);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (flags[x, y])
                {
                    continue;
                }

                int basinSize = 0;
                Queue<(int x, int y)> queue = new Queue<(int x, int y)>(capacity: width);
                queue.Enqueue((x, y));
                while (queue.TryDequeue(out (int x, int y) pos))
                {
                    if (flags[pos.x, pos.y])
                    {
                        continue;
                    }

                    flags[pos.x, pos.y] = true;
                    basinSize++;

                    EnqueueNeighbors(queue, pos.x, pos.y, width, length);
                }
                basinSizes.Add(basinSize);
            }
        }

        int productOfLargestThreeSizes = 1;
        foreach(int size in basinSizes.OrderByDescending(x => x).Take(3))
        {
            productOfLargestThreeSizes *= size;
        }

        Console.WriteLine(productOfLargestThreeSizes);
    }

    private static void EnqueueNeighbors(Queue<(int x, int y)> queue, int x, int y, int width, int length)
    {
        if (x - 1 >= 0)
        {
            queue.Enqueue((x - 1, y));
        }

        if (x + 1 < width)
        {
            queue.Enqueue((x + 1, y));
        }

        if (y - 1 >= 0)
        {
            queue.Enqueue((x, y - 1));
        }

        if (y + 1 < length)
        {
            queue.Enqueue((x, y + 1));
        }
    }

    private static void BuildHeightmap(int[,] heightmap, bool[,] flags, string[] inputs)
    {
        int width = inputs.Length;
        int length = inputs[0].Length;

        for (int x = 0; x < width; x++)
        {
            string input = inputs[x];
            for (int y = 0; y < length; y++)
            {
                heightmap[x, y] = input[y] - '0';
                flags[x, y] = heightmap[x, y] == 9;
            }
        }
    }

}
