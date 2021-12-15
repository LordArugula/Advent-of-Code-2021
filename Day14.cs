namespace AdventOfCode2021;

public static class Day14
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(14);
        string polymerAsString = inputs[0];

        Dictionary<ElementPair, long> polymer = new Dictionary<ElementPair, long>();
        Dictionary<char, long> elementCount = new Dictionary<char, long>();
        for (int i = 0; i < polymerAsString.Length - 1; i++)
        {
            char first = polymerAsString[i];
            char second = polymerAsString[i + 1];

            var pair = new ElementPair(first, second);
            if (polymer.ContainsKey(pair))
            {
                polymer[pair]++;
            }
            else
            {
                polymer.Add(pair, 1);
            }
        }

        foreach (char element in polymerAsString)
        {
            if (elementCount.ContainsKey(element))
            {
                elementCount[element]++;
            }
            else
            {
                elementCount.Add(element, 1);
            }
        }

        IReadOnlyDictionary<ElementPair, char> pairInsertionRules
            = ParsePairInsertionRules(inputs);

        SimulatePairInsertion(polymer, elementCount, pairInsertionRules, 40);

        long max = elementCount.Max(keyPair => keyPair.Value);
        long min = elementCount.Min(keyPair => keyPair.Value);
        Console.WriteLine(max - min);
    }

    private static void SimulatePairInsertion(Dictionary<ElementPair, long> polymer, Dictionary<char, long> elementCount, IReadOnlyDictionary<ElementPair, char> pairInsertionRules, int steps)
    {
        while (steps > 0)
        {
            steps--;

            Dictionary<ElementPair, long> pairsToAdd = new Dictionary<ElementPair, long>();

            foreach (KeyValuePair<ElementPair, long> pairCount in polymer)
            {
                ElementPair pair = pairCount.Key;
                long count = pairCount.Value;
                char insertedCharacter = pairInsertionRules[pair];

                if (elementCount.ContainsKey(insertedCharacter))
                {
                    elementCount[insertedCharacter] += count;
                }
                else
                {
                    elementCount.Add(insertedCharacter, count);
                }

                ElementPair newPair1 = new ElementPair(pair.First, insertedCharacter);
                ElementPair newPair2 = new ElementPair(insertedCharacter, pair.Second);
                if (pairsToAdd.ContainsKey(pair))
                {
                    pairsToAdd[pair] -= count;
                }
                else
                {
                    pairsToAdd.Add(pair, -count);
                }

                if (pairsToAdd.ContainsKey(newPair1))
                {
                    pairsToAdd[newPair1] += count;
                }
                else
                {
                    pairsToAdd.Add(newPair1, count);
                }

                if (pairsToAdd.ContainsKey(newPair2))
                {
                    pairsToAdd[newPair2] += count;
                }
                else
                {
                    pairsToAdd.Add(newPair2, count);
                }
            }

            foreach (KeyValuePair<ElementPair, long> pairCount in pairsToAdd)
            {
                if (polymer.ContainsKey(pairCount.Key))
                {
                    polymer[pairCount.Key] += pairCount.Value;
                }
                else
                {
                    polymer.Add(pairCount.Key, pairCount.Value);
                }
            }
        }
    }

    private static IReadOnlyDictionary<ElementPair, char> ParsePairInsertionRules(string[] inputs)
    {
        Dictionary<ElementPair, char> pairInsertionRules = new Dictionary<ElementPair, char>();

        for (int i = 2; i < inputs.Length; i++)
        {
            string pairInsertionAsString = inputs[i];

            char first = pairInsertionAsString[0];
            char second = pairInsertionAsString[1];
            char inserted = pairInsertionAsString[^1];

            pairInsertionRules.Add((first, second), inserted);
        }

        return pairInsertionRules;
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(14);
        string polymerAsString = inputs[0];

        Dictionary<ElementPair, long> polymer = new Dictionary<ElementPair, long>();
        Dictionary<char, long> elementCount = new Dictionary<char, long>();
        for (int i = 0; i < polymerAsString.Length - 1; i++)
        {
            char first = polymerAsString[i];
            char second = polymerAsString[i + 1];

            var pair = new ElementPair(first, second);
            if (polymer.ContainsKey(pair))
            {
                polymer[pair]++;
            }
            else
            {
                polymer.Add(pair, 1);
            }
        }

        foreach (char element in polymerAsString)
        {
            if (elementCount.ContainsKey(element))
            {
                elementCount[element]++;
            }
            else
            {
                elementCount.Add(element, 1);
            }
        }

        IReadOnlyDictionary<ElementPair, char> pairInsertionRules
            = ParsePairInsertionRules(inputs);

        SimulatePairInsertion(polymer, elementCount, pairInsertionRules, 40);

        long max = elementCount.Max(keyPair => keyPair.Value);
        long min = elementCount.Min(keyPair => keyPair.Value);
        Console.WriteLine(max - min);
    }
}

internal record struct ElementPair(char First, char Second)
{
    public static implicit operator (char first, char second)(ElementPair value)
    {
        return (value.First, value.Second);
    }

    public static implicit operator ElementPair((char first, char second) value)
    {
        return new ElementPair(value.first, value.second);
    }
}