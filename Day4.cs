namespace AdventOfCode2021;

public static class Day4
{
    internal class BingoBoard
    {
        private readonly int[,] board = new int[5, 5];
        private readonly bool[,] markers = new bool[5, 5];

        public int this[int x, int y]
        {
            get => board[x, y];
            set => board[x, y] = value;
        }

        /// <summary>
        /// Marks the number if it exists on the board.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>True if the number exists on the board.</returns>
        public bool MarkNumber(int number)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (board[x, y] == number)
                    {
                        markers[x, y] = true;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the board has any row or column has all five numbers marked.
        /// </summary>
        /// <returns>True if any row or column has all five numbers marked.</returns>
        public bool IsWinner()
        {
            for (int a = 0; a < 5; a++)
            {
                if (CheckRow(a) || CheckColumn(a))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckRow(int row)
        {
            for (int column = 0; column < 5; column++)
            {
                if (!markers[row, column])
                    return false;
            }
            return true;
        }

        private bool CheckColumn(int column)
        {
            for (int row = 0; row < 5; row++)
            {
                if (!markers[row, column])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Calculates the sum of all unmarked numbers.
        /// </summary>
        /// <returns>The sum of all unmarked numbers.</returns>
        public int GetUnmarkedSum()
        {
            int score = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (!markers[x, y])
                    {
                        score += board[x, y];
                    }
                }
            }
            return score;
        }
    }

    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(4);

        IEnumerable<int> bingoBalls = GetBingoBalls(inputs);

        List<BingoBoard> bingoBoards = BuildBingoBoards(inputs);

        foreach (int number in bingoBalls)
        {
            for (int i = 0; i < bingoBoards.Count; i++)
            {
                BingoBoard bingoBoard = bingoBoards[i];
                if (bingoBoard.MarkNumber(number)
                    && bingoBoard.IsWinner())
                {
                    Console.WriteLine(bingoBoard.GetUnmarkedSum() * number);
                    return;
                }
            }
        }
    }

    private static IEnumerable<int> GetBingoBalls(string[] inputs)
    {
        return inputs[0]
                    .Split(',')
                    .Select(n => int.Parse(n));
    }

    private static List<BingoBoard> BuildBingoBoards(string[] inputs)
    {
        List<BingoBoard> bingoBoards = new List<BingoBoard>();

        for (int line = 2; line < inputs.Length; line++)
        {
            BingoBoard board = new BingoBoard();
            for (int i = 0; i < 5; i++)
            {
                IEnumerable<int> numbers = inputs[line++]
                    .Split(' ')
                    .Where(n => n != "")
                    .Select(n => int.Parse(n));

                int n = 0;
                foreach (int number in numbers)
                {
                    board[n, i] = number;
                    n++;
                }
            }

            bingoBoards.Add(board);
        }

        return bingoBoards;
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(4);
        IEnumerable<int> bingoBalls = GetBingoBalls(inputs);
        List<BingoBoard> bingoBoards = BuildBingoBoards(inputs);

        foreach (int number in bingoBalls)
        {
            for (int i = 0; i < bingoBoards.Count; i++)
            {
                BingoBoard bingoBoard = bingoBoards[i];
                if (bingoBoard.MarkNumber(number)
                    && bingoBoard.IsWinner())
                {
                    if (bingoBoards.Count == 1)
                    {
                        Console.WriteLine(bingoBoard.GetUnmarkedSum() * number);
                        return;
                    }

                    bingoBoards.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
