namespace AdventOfCode2021;

public static class Day17
{
    public struct Bounds
    {
        public readonly int xMin;
        public readonly int xMax;
        public readonly int yMin;
        public readonly int yMax;

        public Bounds(int xMin, int yMin, int xMax, int yMax)
        {
            this.xMin = xMin;
            this.yMin = yMin;
            this.xMax = xMax;
            this.yMax = yMax;
        }

        /// <summary>
        /// Checks if the point x, y is inside or on the bounds.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(int x, int y)
        {
            return x >= xMin && x <= xMax && y >= yMin && y <= yMax;
        }
    }

    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(17);
        Bounds bounds = ParseBounds(inputs[0]);
        int highestY = OongaBoongaBruteForceFindMaxYPosition(bounds);

        Console.WriteLine(highestY);
    }

    private static int OongaBoongaBruteForceFindMaxYPosition(Bounds bounds)
    {
        int highestY = int.MinValue;
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                if (Simulate(x, y, bounds, out int maxY))
                {
                    if (maxY > highestY)
                    {
                        highestY = maxY;
                    }
                }
            }
        }

        return highestY;
    }

    private static bool Simulate(int vx, int vy, Bounds bounds, out int maxY)
    {
        int x = 0;
        int y = 0;

        maxY = y;

        int maxSteps = 1000;
        int steps = 0;
        while (!bounds.Contains(x, y))
        {
            if (steps == maxSteps)
            {
                return false;
            }
            steps++;

            x += vx;
            y += vy;
            if (y > maxY)
            {
                maxY = y;
            }

            if (vx < 0)
            {
                vx = Math.Min(vx + 1, 0);
            }
            else if (vx > 0)
            {
                vx = Math.Max(vx - 1, 0);
            }

            vy -= 1;
        }
        return true;
    }

    private static Bounds ParseBounds(string input)
    {
        ReadOnlySpan<char> span = input.AsSpan();
        int xStringIndex = span.IndexOf("x");
        int yStringIndex = span.IndexOf("y");
        ReadOnlySpan<char> xString = span.Slice(xStringIndex + 2, span.Length - yStringIndex - 3);
        ReadOnlySpan<char> yString = span.Slice(yStringIndex + 2);

        int xDotDotIndex = xString.IndexOf('.');
        int x1 = int.Parse(xString[..xDotDotIndex]);
        int x2 = int.Parse(xString[(xDotDotIndex + 2)..]);

        int yDotDotIndex = yString.IndexOf('.');
        int y1 = int.Parse(yString[..yDotDotIndex]);
        int y2 = int.Parse(yString[(yDotDotIndex + 2)..]);
        return new Bounds(x1, y1, x2, y2);
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(17);

        Bounds bounds = ParseBounds(inputs[0]);

        int count = 0;
        count = OongaBoongaBruteForceCountSuccessfulInitialVelocities(bounds, count);
        Console.WriteLine(count);
    }

    private static int OongaBoongaBruteForceCountSuccessfulInitialVelocities(Bounds bounds, int count)
    {
        for (int x = 0; x < 1000; x++)
        {
            for (int y = -1000; y < 1000; y++)
            {
                if (Simulate(x, y, bounds, out _))
                {
                    count++;
                }
            }
        }

        return count;
    }
}
