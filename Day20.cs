namespace AdventOfCode2021;

public static class Day20
{
    public static void Part1()
    {
        string[] inputs = InputHelper.GetInput(20);

        string algorithmString = inputs[0];

        const int enhancementSteps = 2;
        char[,] image = ReadImage(inputs, enhancementSteps);

        for (int i = 0; i < enhancementSteps; i++)
        {
            EnhanceImage(image, algorithmString);
        }

        int litPixelCount = CountLitPixels(image, enhancementSteps);
        Console.WriteLine(litPixelCount);
    }

    private static void PrintImage(char[,] image)
    {
        int height = image.GetLength(0);
        int width = image.GetLength(1);

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Console.Write(image[x, y]);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static char[,] ReadImage(string[] inputs, int marginSize)
    {
        int height = inputs.Length - 2;
        int width = inputs[2].Length;

        marginSize = marginSize * 2 + 1;
        int margins = 4 * marginSize;
        char[,] image = new char[width + margins, height + margins];

        for (int x = 0; x < image.GetLength(0); x++)
        {
            for (int y = 0; y < image.GetLength(1); y++)
            {
                image[x, y] = '.';
            }
        }

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                image[x + marginSize, y + marginSize] = inputs[x + 2][y];
            }
        }

        return image;
    }

    private static void EnhanceImage(char[,] image, string algorithmString)
    {
        int height = image.GetLength(0);
        int width = image.GetLength(1);

        char[,] outputImage = new char[height, width];

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                string binaryString = GetBinaryStringFromPixel(x, y, image);
                int outputIndex = Convert.ToInt32(binaryString, fromBase: 2);
                char pixel = algorithmString[outputIndex];
                outputImage[x, y] = pixel;
            }
        }

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                image[x, y] = outputImage[x, y];
            }
        }
    }

    private static string GetBinaryStringFromPixel(int x, int y, char[,] image)
    {
        char[] binaryString = new char[9];
        int i = 0;
        for (int dx = -1; dx <= +1; dx++)
        {
            for (int dy = -1; dy <= +1; dy++)
            {
                if (x + dx < 0 || x + dx >= image.GetLength(0)
                    || y + dy < 0 || y + dy >= image.GetLength(1))
                {
                    binaryString[i] = '0';
                }
                else
                {
                    binaryString[i] = image[x + dx, y + dy] == '.' ? '0' : '1';
                }
                i++;
            }
        }
        return new string(binaryString);
    }

    private static int CountLitPixels(char[,] twoEnhanceStepImage, int marginSize)
    {
        int count = 0;

        int height = twoEnhanceStepImage.GetLength(0);
        int width = twoEnhanceStepImage.GetLength(1);

        for (int x = marginSize; x < height - marginSize; x++)
        {
            for (int y = marginSize; y < width - marginSize; y++)
            {
                if (twoEnhanceStepImage[x, y] == '#')
                {
                    count++;
                }
            }
        }
        return count;
    }

    public static void Part2()
    {
        string[] inputs = InputHelper.GetInput(20);

        string algorithmString = inputs[0];

        const int enhancementSteps = 50;
        char[,] image = ReadImage(inputs, enhancementSteps);

        for (int i = 0; i < enhancementSteps; i++)
        {
            EnhanceImage(image, algorithmString);
        }

        int litPixelCount = CountLitPixels(image, enhancementSteps);
        Console.WriteLine(litPixelCount);
    }
}
