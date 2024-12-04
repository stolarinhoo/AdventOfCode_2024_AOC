using System.ComponentModel.Design;

namespace AdventOfCode_2024_AOC;

public class Day04
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_04.txt";
    
    public static void Solution_01()
    {
        int xmas = 0;
        
        char[][] matrix = PrepareData();
        var directions = new ValueTuple<int, int>[]
        {
            (0, -1), (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1)
        };
        var dict = new Dictionary<char, char>();
        dict.Add('X', 'M');
        dict.Add('M', 'A');
        dict.Add('A', 'S');
        dict.Add('S', 'X');
        
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (matrix[row][col] == 'X')
                {
                    foreach (var dir in directions)
                    {
                        DFS(row, col, 'X', dir);
                    }
                }
            }
        }

        Console.WriteLine(xmas);
        
        void DFS(int row, int col, char next, (int, int) direction)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols || matrix[row][col] != next)
            {
                return;
            }
            
            char current = matrix[row][col];
            if (current == 'S' && next == 'S')
            {
                xmas++;
                return;
            }
            next = dict.ContainsKey(current) ? dict[current] : 'X';

            DFS(row + direction.Item1, col + direction.Item2, next, direction);
        }
    }

    public static void Solution_02()
    {
        int xmas = 0;
        char[][] matrix = PrepareData();
        
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (matrix[row][col] == 'A')
                {
                    XmasSearch(row, col);
                }
            }
        }
        
        Console.WriteLine(xmas);

        void XmasSearch(int row, int col)
        {
            if (row - 1 < 0 || row + 1 >= rows || col - 1 < 0 || col + 1 >= cols)
            {
                return;
            }
            char upperLeft = matrix[row - 1][col - 1];
            char lowerLeft = matrix[row + 1][col - 1];
            char lowerRight = matrix[row + 1][col + 1];
            char upperRight = matrix[row - 1][col + 1];

            if ((upperLeft == 'M' && lowerRight == 'S' && lowerLeft == 'M' && upperRight == 'S') ||
                (upperLeft == 'M' && lowerRight == 'S' && lowerLeft == 'S' && upperRight == 'M') ||
                (upperLeft == 'S' && lowerRight == 'M' && lowerLeft == 'M' && upperRight == 'S') ||
                (upperLeft == 'S' && lowerRight == 'M' && lowerLeft == 'S' && upperRight == 'M'))
            {
                xmas++;
            }
        }
    }

    private static char[][] PrepareData()
    {
        var lines = File.ReadAllLines(Filepath);
        char[][] matrix = new char[lines.Length][];
        
        for (int i = 0; i < lines.Length; i++)
        {
            matrix[i] = lines[i].ToCharArray();
        }

        return matrix;
    }
}
