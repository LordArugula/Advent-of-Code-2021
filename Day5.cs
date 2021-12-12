namespace AdventOfCode2021;

public static class Day5
{
    [System.Diagnostics.DebuggerDisplay("({x}, {y})")]
    private struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private class LineSegment
    {
        public Point a;
        public Point b;

        public static LineSegment Parse(string input)
        {
            ReadOnlySpan<char> inputAsSpan = input.AsSpan();

            int firstSep = inputAsSpan.IndexOf(' ');
            int lastSep = inputAsSpan.LastIndexOf(' ');

            ReadOnlySpan<char> pointASpan = inputAsSpan.Slice(0, firstSep);
            ReadOnlySpan<char> pointBSpan = inputAsSpan.Slice(lastSep + 1);

            int sepAIndex = pointASpan.IndexOf(',');
            int sepBIndex = pointBSpan.IndexOf(',');

            return new LineSegment
            {
                a = new Point(int.Parse(pointASpan.Slice(0, sepAIndex)),
                    int.Parse(pointASpan.Slice(sepAIndex + 1))),
                b = new Point(int.Parse(pointBSpan.Slice(0, sepBIndex)),
                    int.Parse(pointBSpan.Slice(sepBIndex + 1)))
            };
        }

        public bool IsHorizontal()
        {
            return a.y == b.y;
        }

        public bool IsVertical()
        {
            return a.x == b.x;
        }

        public bool IsAxisAligned()
        {
            return IsHorizontal() || IsVertical();
        }
    }

    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(5);

        Dictionary<Point, int> heatmap = new Dictionary<Point, int>();

        for (int i = 0; i < inputs.Length; i++)
        {
            LineSegment lineSegment = LineSegment.Parse(inputs[i]);
            if (lineSegment.IsAxisAligned())
            {
                DrawLine(heatmap, lineSegment);
            }
        }
        Console.WriteLine(heatmap.Count(pair => pair.Value > 1));
    }

    private static void DrawLine(Dictionary<Point, int> heatmap, LineSegment lineSegment)
    {
        float dy = lineSegment.b.y - lineSegment.a.y;
        float dx = lineSegment.b.x - lineSegment.a.x;

        int steps = Math.Abs(dx) > Math.Abs(dy) 
            ? (int)Math.Abs(dx) 
            : (int)Math.Abs(dy);

        if (steps == 0)
        {
            DrawPoint(heatmap, lineSegment.a);
            return;
        }

        float x = lineSegment.a.x;
        float y = lineSegment.a.y;
        dx /= steps;
        dy /= steps;

        for (int i = 0; i <= steps; i++)
        {
            DrawPoint(heatmap, new Point((int)Math.Round(x), (int)Math.Round(y)));
            x += dx;
            y += dy;
        }
    }

    private static void DrawPoint(Dictionary<Point, int> heatmap, Point point)
    {
        if (heatmap.ContainsKey(point))
        {
            heatmap[point]++;
        }
        else
        {
            heatmap.Add(point, 1);
        }
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(5);

        Dictionary<Point, int> heatmap = new Dictionary<Point, int>();

        for (int i = 0; i < inputs.Length; i++)
        {
            LineSegment lineSegment = LineSegment.Parse(inputs[i]);
                DrawLine(heatmap, lineSegment);
        }
        Console.WriteLine(heatmap.Count(pair => pair.Value > 1));
    }
}