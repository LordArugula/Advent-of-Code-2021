namespace AdventOfCode2021;

public static class Day6
{
    public static void Part1()
    {
        ulong[] lanternfishAges = GetLanternfishAges();

        SimulateLanternfish(lanternfishAges, 80);

        Console.WriteLine(CountLanternfishes(lanternfishAges));
    }

    private static ulong CountLanternfishes(ulong[] lanternfishAges)
    {
        ulong count = 0;
        for (int i = 0; i < lanternfishAges.Length; i++)
        {
            count += lanternfishAges[i];
        }
        return count;
    }

    private static ulong[] GetLanternfishAges()
    {
        string[] inputs = InputHelper.GetInput(6);
        var lanternfishAgesInput = inputs[0].Split(',');

        ulong[] lanternfishAges = new ulong[9];
        for (int i = 0; i < lanternfishAgesInput.Length; i++)
        {
            int bucket = lanternfishAgesInput[i] switch
            {
                "0" => 0,
                "1" => 1,
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                "7" => 7,
                "8" => 8,
                _ => 0,
            };
            lanternfishAges[bucket]++;
        }

        return lanternfishAges;
    }

    private static void SimulateLanternfish(ulong[] lanternfish, int days)
    {
        while (days > 0)
        {
            days--;

            ulong newFish = lanternfish[0];

            for (int i = 1; i < 9; i++)
            {
                lanternfish[i - 1] = lanternfish[i];
            }
            lanternfish[6] += newFish;
            lanternfish[8] = newFish;
        }
    }

    public static void Part2()
    {
        ulong[] lanternfishAges = GetLanternfishAges();

        SimulateLanternfish(lanternfishAges, 256);

        Console.WriteLine(CountLanternfishes(lanternfishAges));
    }
}