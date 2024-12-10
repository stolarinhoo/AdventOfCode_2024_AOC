using System.ComponentModel.Design;

namespace AdventOfCode_2024_AOC;

public class Day10
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_10.txt";
    
    public static void Solution_01()
    {
        int result = 0;
        int[][] matrix = PrepareData();
        int rows = matrix.Length;
        int cols = matrix[0].Length;
            
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (matrix[r][c] == 0)
                {
                    result += DFS(r, c, new HashSet<(int, int)>(), -1);
                }
            }
        }
        Console.WriteLine(result);
        
        int DFS(int r, int c, HashSet<(int, int)> visited, int prevVal)
        {
            if (r < 0 || c < 0 || r >= rows || c >= cols ||
                visited.Contains((r, c)) || matrix[r][c] != prevVal + 1)
            {
                return 0;
            }
            if (matrix[r][c] == 9)
            {
                return 1;
            }
            visited.Add((r, c));

            return (DFS(r + 1, c, visited, matrix[r][c]) +
                    DFS(r - 1, c, visited, matrix[r][c]) +
                    DFS(r, c + 1, visited, matrix[r][c]) +
                    DFS(r, c - 1, visited, matrix[r][c]));
        }
    }
    
    public static void Solution_02()
    {
        int result = 0;
        int[][] matrix = PrepareData();
        int rows = matrix.Length;
        int cols = matrix[0].Length;
            
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (matrix[r][c] == 0)
                {
                    result += DFS(r, c, new HashSet<(int, int)>(), -1);
                }
            }
        }
        Console.WriteLine(result);
        
        int DFS(int r, int c, HashSet<(int, int)> visited, int prevVal)
        {
            if (r < 0 || c < 0 || r >= rows || c >= cols ||
                visited.Contains((r, c)) || matrix[r][c] != prevVal + 1)
            {
                return 0;
            }
            if (matrix[r][c] == 9)
            {
                return 1;
            }
            visited.Add((r, c));

            int res = 0;
            res += DFS(r + 1, c, visited, matrix[r][c]);
            res += DFS(r - 1, c, visited, matrix[r][c]);
            res += DFS(r, c + 1, visited, matrix[r][c]);
            res += DFS(r, c - 1, visited, matrix[r][c]);

            visited.Remove((r, c));
            return res;
        }
    }
    
    private static int[][] PrepareData()
    {
        var lines = File.ReadAllLines(Filepath);
        int[][] matrix = new int[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            matrix[i] = Array.ConvertAll(lines[i].ToCharArray(), c => c - '0');

        }

        return matrix;
    }
}
