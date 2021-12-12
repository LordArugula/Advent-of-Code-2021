namespace AdventOfCode2021
{
    public static class InputHelper
    {
        public static string[] GetInput(int day)
        {
            return File.ReadAllLines($"input/day{day}.txt");
        }
    }
}