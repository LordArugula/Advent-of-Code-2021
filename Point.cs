namespace AdventOfCode2021;

public record struct Point(int X, int Y)
{
    public static implicit operator (int x, int y)(Point value)
    {
        return (value.X, value.Y);
    }

    public static implicit operator Point((int x, int y) value)
    {
        return new Point(value.x, value.y);
    }
}
