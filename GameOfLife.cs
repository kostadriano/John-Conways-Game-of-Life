using System;
using System.Threading;

class GameOfLife
{
    static int size = 25;
    static void Main(string[] args)
    {
        int[,] matrix = new int[size, size];
        int[,] tempMatrix = new int[size, size];
        Random rd = new Random();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = rd.Next(2);
            }
        }

        while (true)
        {
            Console.Clear();
            Show(matrix);
            Thread.Sleep(200);

            tempMatrix = (int[,])matrix.Clone();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int neighbours = CalculateNeighbours(tempMatrix, i, j);

                    if (OverPopulation(tempMatrix[i, j], neighbours))
                        matrix[i, j] = 0;

                    if (UnderPopulation(tempMatrix[i, j], neighbours))
                        matrix[i, j] = 1;
                }
            }
        }
    }
    
    static bool OverPopulation(int element, int neighbours)
    {
        return ((element == 1 && (neighbours < 2 || neighbours > 3) ? true : false));
    }

    static bool UnderPopulation(int element, int neighbours)
    {
        return ((element == 0 && neighbours == 3) ? true : false);
    }
    
    static void Show(int[,] M)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.BackgroundColor = M[i, j] == 1 ? ConsoleColor.White : ConsoleColor.Black;
                Console.Write("  ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static int CalculateNeighbours(int[,] M, int x, int y)
    {
        int neighbours = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (j + y < 0 || j + y >= size || i + x < 0 || i + x >= size || (i + x == x && j + y == y))
                    continue;

                if (M[i + x, j + y] == 1)
                    neighbours++;
            }
        }
        return neighbours;
    }
}
