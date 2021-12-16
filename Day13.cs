namespace AdventOfCode2021;

public static class Day13
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(13);

        HashSet<Point> points = new HashSet<Point>();
        List<Point> foldInstructions = new List<Point>();
        ParseInput(inputs, points, foldInstructions);

        Point foldPoint = foldInstructions[0];

        Point[] movedPoints = points
            .Where(p => p.X >= foldPoint.X && p.Y >= foldPoint.Y)
            .ToArray();

        foreach (Point point in movedPoints)
        {
            points.Remove(point);
            points.Add(new Point(Math.Abs(2 * foldPoint.X - point.X), Math.Abs(2 * foldPoint.Y - point.Y)));
        }
        Console.WriteLine(points.Count);
    }

    private static void ParseInput(string[] inputs, HashSet<Point> points, List<Point> foldInstructions)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            string input = inputs[i];
            int delimIndex = input.IndexOf(',');

            if (delimIndex == -1)
            {
                ParseFoldInstruction(foldInstructions, input);
            }
            else
            {
                Point p = ParsePoint(delimIndex, input);
                points.Add(p);
            }
        }
    }

    private static void ParseFoldInstruction(List<Point> foldInstructions, string input)
    {
        int delimIndex = input.LastIndexOf('=');
        if (delimIndex == -1)
        {
            return;
        }

        if (input[delimIndex - 1] == 'x')
        {
            foldInstructions.Add(new Point(int.Parse(input[(delimIndex + 1)..]), 0));
        }
        else
        {
            foldInstructions.Add(new Point(0, int.Parse(input[(delimIndex + 1)..])));
        }
    }

    private static Point ParsePoint(int delimIndex, string pointAsString)
    {
        return new Point(int.Parse(pointAsString[..delimIndex]),
            int.Parse(pointAsString[(delimIndex + 1)..]));
    }

    public static void Part2()
    {

        string[] inputs = InputHelper.GetInput(13);

        HashSet<Point> points = new HashSet<Point>();
        List<Point> foldInstructions = new List<Point>();
        ParseInput(inputs, points, foldInstructions);

        for (int i = 0; i < foldInstructions.Count; i++)
        {
            Point foldPoint = foldInstructions[i];

            Point[] movedPoints = points
                .Where(p => p.X >= foldPoint.X && p.Y >= foldPoint.Y)
                .ToArray();

            foreach (Point point in movedPoints)
            {
                points.Remove(point);
                points.Add(new Point(Math.Abs(2 * foldPoint.X - point.X), Math.Abs(2 * foldPoint.Y - point.Y)));
            }
        }

        int maxX = 0;
        int maxY = 0;
        foreach (var point in points)
        {
            if (point.X > maxX)
            {
                maxX = point.X;
            }

            if (point.Y > maxY)
            {
                maxY = point.Y;
            }
        }

        bool[,] pointMap = new bool[maxX + 1, maxY + 1];
        foreach (var point in points)
        {
            pointMap[point.X, point.Y] = true;
        }

        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                Console.Write(pointMap[x, y] ? '#' : '.');
            }
            Console.WriteLine();
        }
    }
}
