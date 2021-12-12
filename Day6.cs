namespace AdventOfCode2021;

public static class Day6
{
    public static void Part1()
    {
        List<int> lanternfish = GetLanternfishAges();
        SimulateLanterfish(lanternfish, 80);

        Console.WriteLine(lanternfish.Count);
    }

    private static List<int> GetLanternfishAges()
    {
        string[] inputs = InputHelper.GetInput(6);
        List<int> lanternfish = inputs[0]
            .Split(',')
            .Select(x => int.Parse(x))
            .ToList();
        return lanternfish;
    }

    private static void SimulateLanterfish(List<int> lanternfish, int days)
    {
        while (days > 0)
        {
            days--;

            int newFish = 0;
            for (int i = 0; i < lanternfish.Count; i++)
            {
                if (lanternfish[i] == 0)
                {
                    newFish++;
                    lanternfish[i] = 7;
                }

                lanternfish[i]--;
            }

            for (int i = 0; i < newFish; i++)
            {
                lanternfish.Add(8);
            }
        }
    }

    public static void Part2()
    {
        List<int> lanternfish = GetLanternfishAges();
        SimulateLanterfish(lanternfish, 256);

        Console.WriteLine(lanternfish.Count);
    }
}