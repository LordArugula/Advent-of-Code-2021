namespace AdventOfCode2021;

public static class Day12
{
    private class Node
    {
        private readonly List<Node> nodes;
        private readonly bool isBigCave = false;

        public IReadOnlyList<Node> Nodes => nodes;
        public bool IsBigCave => isBigCave;

        public Node(bool isBigCave)
        {
            nodes = new List<Node>();
            this.isBigCave = isBigCave;
        }

        public static void Connect(Node a, Node b)
        {
            a.nodes.Add(b);
            b.nodes.Add(a);
        }
    }

    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(12);

        Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        for (int i = 0; i < inputs.Length; i++)
        {
            string[] connection = inputs[i].Split('-');
            string nodeA = connection[0];
            string nodeB = connection[1];

            if (graph.ContainsKey(nodeA))
            {
                graph[nodeA].Add(nodeB);
            }
            else
            {
                graph.Add(nodeA, new List<string>() { nodeB });
            }

            if (graph.ContainsKey(nodeB))
            {
                graph[nodeB].Add(nodeA);
            }
            else
            {
                graph.Add(nodeB, new List<string>() { nodeA });
            }
        }

        HashSet<string> visited = new HashSet<string>();
        int numPaths = GetPaths(graph, visited, "start");
        Console.WriteLine(numPaths);
    }

    private static int GetPaths(Dictionary<string, List<string>> graph,
                                HashSet<string> visited,
                                string node)
    {
        if (node == "end")
        {
            return 1;
        }

        if (IsNotRevisitable(node))
        {
            if (visited.Contains(node))
            {
                return 0;
            }
            else
            {
                visited.Add(node);
            }
        }

        int paths = 0;
        List<string> connections = graph[node];
        for (int i = 0; i < connections.Count; i++)
        {
            paths += GetPaths(graph, visited, connections[i]);
        }

        visited.Remove(node);

        return paths;
    }

    private static bool IsNotRevisitable(string node) => node.Any(c => char.IsLower(c));

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(12);

    }
}
