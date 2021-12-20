namespace AdventOfCode2021;

public static class Day5
{
    private class LineSegment
    {
        public Point a;
        public Point b;

        public static LineSegment Parse(string input)
        {
            ReadOnlySpan<char> inputAsSpan = input.AsSpan();

            int firstSep = inputAsSpan.IndexOf(' ');
            int lastSep = inputAsSpan.LastIndexOf(' ');

            ReadOnlySpan<char> pointASpan = inputAsSpan[..firstSep];
            ReadOnlySpan<char> pointBSpan = inputAsSpan[(lastSep + 1)..];

            int sepAIndex = pointASpan.IndexOf(',');
            int sepBIndex = pointBSpan.IndexOf(',');

            return new LineSegment
            {
                a = new Point(int.Parse(pointASpan[..sepAIndex]),
                    int.Parse(pointASpan[(sepAIndex + 1)..])),
                b = new Point(int.Parse(pointBSpan[..sepBIndex]),
                    int.Parse(pointBSpan[(sepBIndex + 1)..]))
            };
        }

        public bool IsHorizontal()
        {
            return a.Y == b.Y;
        }

        public bool IsVertical()
        {
            return a.X == b.X;
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
        float dy = lineSegment.b.Y - lineSegment.a.Y;
        float dx = lineSegment.b.X - lineSegment.a.X;

        int steps = Math.Abs(dx) > Math.Abs(dy) 
            ? (int)Math.Abs(dx) 
            : (int)Math.Abs(dy);

        if (steps == 0)
        {
            DrawPoint(heatmap, lineSegment.a);
            return;
        }

        float x = lineSegment.a.X;
        float y = lineSegment.a.Y;
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