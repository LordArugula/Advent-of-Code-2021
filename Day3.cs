namespace AdventOfCode2021;

public static class Day3
{
	public static void Part1()
	{
		string[] inputs = InputHelper.GetInput(3);
		int bitSize = inputs[0].Length;

		int[] bitsCount = new int[bitSize];

		for (int b = 0; b < bitSize; b++)
		{
			for (int i = 0; i < inputs.Length; i++)
			{
				bitsCount[b] += inputs[i][b] == '0' ? 0 : 1;
			}
		}

		char[] gammaRateBits = new char[bitSize];
		char[] epsilonRateBits = new char[bitSize];
		for (int i = 0; i < bitSize; i++)
		{
			if (bitsCount[i] < inputs.Length / 2)
			{
				gammaRateBits[i] = '0';
				epsilonRateBits[i] = '1';
			}
			else
			{
				gammaRateBits[i] = '1';
				epsilonRateBits[i] = '0';
			}
		}

		int gammaRate = Convert.ToInt32(new string(gammaRateBits), 2);
		int epsilonRate = Convert.ToInt32(new string(epsilonRateBits), 2);

		Console.WriteLine(gammaRate * epsilonRate);
	}

	public static void Part2()
	{
		string[] inputs = InputHelper.GetInput(3);

		int oxygenGeneratorRating = GetOxygenGeneratorRating(inputs);
		int co2ScrubberRating = GetCo2ScrubberRating(inputs);

		Console.WriteLine(oxygenGeneratorRating * co2ScrubberRating);
	}

	private static int GetCo2ScrubberRating(IEnumerable<string> inputs)
	{
		int bitSize = inputs.First().Length;
		for (int b = 0; b < bitSize; b++)
		{
			int i = b;
			var zeroBitFiltered = inputs.Where(x => x[i] == '0');
			var oneBitFiltered = inputs.Where(x => x[i] == '1');
			int zeroCount = zeroBitFiltered.Count();
			int oneCount = oneBitFiltered.Count();

            inputs = zeroCount > oneCount
                ? oneBitFiltered
                : zeroBitFiltered;

			if (inputs.Count() == 1)
				break;
        }

        return Convert.ToInt32(inputs.First(), 2);
	}

	private static int GetOxygenGeneratorRating(IEnumerable<string> inputs)
	{
		int bitSize = inputs.First().Length;

		for (int b = 0; b < bitSize; b++)
		{
			int i = b;
			var zeroBitFiltered = inputs.Where(x => x[i] == '0');
			var oneBitFiltered = inputs.Where(x => x[i] == '1');

			int zeroCount = zeroBitFiltered.Count();
			int oneCount = oneBitFiltered.Count();

			inputs = oneCount >= zeroCount
				? oneBitFiltered
				: zeroBitFiltered;

			if (inputs.Count() == 1)
				break;
		}

		return Convert.ToInt32(inputs.First(), 2);
	}
}
