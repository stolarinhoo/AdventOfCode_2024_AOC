using System.Threading.Channels;

namespace AdventOfCode_2024_AOC;

public class Day08
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_08.txt";

    public static void Solution_01()
    {
        char[][] matrix = PrepareData();
        var antennas = ScanAntennas(matrix);
        var antinodes = new HashSet<(int, int)>();

        foreach (var nodes in antennas.Values)
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    (int dr, int dc) = (nodes[j].row - nodes[i].row, nodes[j].col - nodes[i].col);
                    var antinode1 = (nodes[j].row + dr, nodes[j].col + dc);
                    var antinode2 = (nodes[i].row - dr, nodes[i].col - dc);
                    antinodes.Add(antinode1);
                    antinodes.Add(antinode2);
                }
            }
        }

        int result = antinodes.Count(item => IsWithinBounds(matrix, item));
        Console.WriteLine(result);
    }
    
    public static void Solution_02()
    {
        char[][] matrix = PrepareData();
        var antennas = ScanAntennas(matrix);
        var antinodes = new HashSet<(int, int)>();

        foreach (var nodes in antennas.Values)
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    (int dr, int dc) = (nodes[j].row - nodes[i].row, nodes[j].col - nodes[i].col);
                    
                    var antinode1 = (nodes[j].row, nodes[j].col);
                    var antinode2 = (nodes[i].row, nodes[i].col);
                    antinodes.Add(antinode1);
                    antinodes.Add(antinode2);
                    
                    while (IsWithinBounds(matrix, antinode1))
                    {
                        antinode1 = (antinode1.row + dr, antinode1.col + dc);
                        antinodes.Add(antinode1);
                    }
                    
                    while (IsWithinBounds(matrix, antinode2))
                    {
                        antinode2 = (antinode2.row - dr, antinode2.col - dc);
                        antinodes.Add(antinode2);
                    }
                }
            }
        }

        int result = antinodes.Count(item => IsWithinBounds(matrix, item));
        Console.WriteLine(result);
    }

    
    private static bool IsWithinBounds(char[][] matrix, (int row, int col) node)
    {
        int rows = matrix.Length; int cols = matrix[0].Length;
        return (node.row >= 0 && node.row < rows && node.col >= 0 && node.col < cols);
    }
    
    private static Dictionary<char, List<(int row, int col)>> ScanAntennas(char[][] matrix)
    {
        var antennas = new Dictionary<char, List<(int, int)>>();
        int rows = matrix.Length; int cols = matrix[0].Length;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                char a = matrix[r][c];
                if (a != '.')
                {
                    if (!antennas.ContainsKey(a))
                    {
                        antennas[a] = new List<(int, int)>();
                    }
                    antennas[a].Add((r, c));
                }
            }
        }

        return antennas;
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
