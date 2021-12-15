namespace AdventOfCode2021;

public static class Day14
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(14);

        LinkedList<char> polymer = new LinkedList<char>(inputs[0]);

        IReadOnlyDictionary<ElementPair, char> pairInsertionRules
            = ParsePairInsertionRules(inputs);

        SimulatePairInsertion(polymer, pairInsertionRules, 10);

        IOrderedEnumerable<IGrouping<char, char>> orderedElementGroups = polymer
            .GroupBy(e => e)
            .OrderByDescending(e => e.Count());

        Console.WriteLine(orderedElementGroups.First().Count() - orderedElementGroups.Last().Count());
    }

    private static void SimulatePairInsertion(LinkedList<char> polymer, IReadOnlyDictionary<ElementPair, char> pairInsertionRules, int steps)
    {
        while (steps > 0)
        {
            steps--;

            LinkedListNode<char> current = polymer.First;
            if (current == null)
            {
                break;
            }

            while (current.Next != null)
            {
                LinkedListNode<char> next = current.Next;
                char first = current.Value;
                char second = next.Value;

                if (pairInsertionRules.ContainsKey(new ElementPair(first, second)))
                {
                    char inserted = pairInsertionRules[new ElementPair(first, second)];
                    polymer.AddAfter(current, inserted);
                }

                current = next;
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

        LinkedList<char> polymer = new LinkedList<char>(inputs[0]);

        IReadOnlyDictionary<ElementPair, char> pairInsertionRules
            = ParsePairInsertionRules(inputs);

        SimulatePairInsertion(polymer, pairInsertionRules, 40);

        IOrderedEnumerable<IGrouping<char, char>> orderedElementGroups = polymer
            .GroupBy(e => e)
            .OrderByDescending(e => e.Count());

        Console.WriteLine(orderedElementGroups.First().Count() - orderedElementGroups.Last().Count());
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