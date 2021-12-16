namespace AdventOfCode2021;

public static class Day15
{
    private static readonly Point[] Neighbors = new Point[]
    {
        (+0, +1),
        (-1, +0),
        (+1, +0),
        (+0, -1)
    };

    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(15);
        int width = inputs.Length;
        int length = inputs[0].Length;

        int[,] riskLevelMap = new int[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                riskLevelMap[x, y] = inputs[x][y] - '0';
            }
        }

        Point start = new Point(0, 0);
        Point end = new Point(width - 1, length - 1);
        Dictionary<Point, int> cost = Pathfind(riskLevelMap, start, end);

        Console.WriteLine(cost[end]);
    }

    private static Dictionary<Point, int> Pathfind(int[,] riskLevelMap, Point start, Point end)
    {
        int width = riskLevelMap.GetLength(0);
        int length = riskLevelMap.GetLength(1);

        PriorityQueue<Point, int> frontier = new PriorityQueue<Point, int>();
        Dictionary<Point, int> cost = new Dictionary<Point, int>();

        frontier.Enqueue(start, 0);
        cost.Add(start, 0);

        while (frontier.TryDequeue(out Point point, out int priority))
        {
            if (point == end)
            {
                break;
            }

            for (int i = 0; i < Neighbors.Length; i++)
            {
                Point neighbor = new Point(point.X + Neighbors[i].X, point.Y + Neighbors[i].Y);
                if (neighbor.X < 0 || neighbor.X >= width
                    || neighbor.Y < 0 || neighbor.Y >= length)
                {
                    continue;
                }

                int newCost = cost[point] + riskLevelMap[neighbor.X, neighbor.Y];
                if (!cost.ContainsKey(neighbor) || newCost < cost[neighbor])
                {
                    cost[neighbor] = newCost;
                    frontier.Enqueue(neighbor, newCost);
                }
            }
        }

        return cost;
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(15);
        int width = inputs.Length;
        int length = inputs[0].Length;

        int actualWidth = width * 5;
        int actualLength = length * 5;
        int[,] riskLevelMap = new int[actualWidth, actualLength];

        for (int a = 0; a < 5; a++)
        {
            for (int b = 0; b < 5; b++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < length; y++)
                    {
                        int riskLevel = inputs[x][y] - '0'
                            + a + b;
                        if (riskLevel > 9)
                        {
                            riskLevel -= 9;
                        }

                        riskLevelMap[x + width * a, y + length * b] = riskLevel;
                    }
                }
            }
        }

        Point start = new Point(0, 0);
        Point end = new Point(actualWidth - 1, actualLength - 1);
        Dictionary<Point, int> cost = Pathfind(riskLevelMap, start, end);

        Console.WriteLine(cost[end]);
    }
}
