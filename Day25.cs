namespace AdventOfCode2021;

public static class Day25
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(25);

        int width = inputs.Length;
        int height = inputs[0].Length;

        char[,] map = new char[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = inputs[x][y];
            }
        }

        int steps = 1;
        while (Simulate(map))
        {
            steps++;
            //PrintMap(map);
        }
        Console.Write(steps);
    }

    private static void PrintMap(char[,] map)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Console.Write(map[x, y]);
            }
            Console.WriteLine();
        }
    }

    private static bool Simulate(char[,] map)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        bool moved = false;

        Queue<(int, int)> positions = new Queue<(int, int)>();
        for (int x = 0; x < width; x++)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                if (map[x, y] == '>'
                    && map[x, (y + 1) % height] == '.')
                {
                    positions.Enqueue((x, y));
                    y--;
                }
            }
        }

        while (positions.TryDequeue(out (int x, int y) position))
        {
            int x = position.x;
            int y = position.y;

            map[x, y] = '.';
            map[x, (y + 1) % height] = '>';

            moved = true;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = width - 1; x >= 0; x--)
            {
                if (map[x, y] == 'v'
                    && map[(x + 1) % width, y] == '.')
                {
                    positions.Enqueue((x, y));
                    x--;
                }
            }
        }

        while (positions.TryDequeue(out (int x, int y) position))
        {
            int x = position.x;
            int y = position.y;

            map[x, y] = '.';
            map[(x + 1) % width, y] = 'v';

            moved = true;
        }

        return moved;
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(25);

    }
}
