namespace AdventOfCode_2024_AOC;

public class Day16
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_16.txt";
    
    public static void Solution_01()
    {
        var map = PrepareData();
        int rows = map.Length; int cols = map[0].Length;
        var pos = FindStartPosition(map);

        var solutions = new List<int>();
        var visited = new HashSet<(int, int)>();
        var cache = new Dictionary<(int, int), int>();

        DFS(pos.r, pos.c, '^', 1000);
        
        void DFS(int r, int c, char symbol, int score)
        {
            if (r < 0 || r >= rows || c < 0 || c >= cols || map[r][c] == '#' || visited.Contains((r, c)))
            {
                return;
            }
            if(cache.ContainsKey((r, c)) && cache[(r, c)] <= score)
            {
                return;
            }
            if (map[r][c] == 'E')
            {
                solutions.Add(score);
                return;
            }

            cache[(r, c)] = score;
            visited.Add((r, c));
            
            DFS(r - 1, c, '^', symbol == '^' ? score + 1 : score + 1001);
            DFS(r, c + 1, '>', symbol == '>' ? score + 1 : score + 1001);
            DFS(r + 1, c, 'v', symbol == 'v' ? score + 1 : score + 1001);
            DFS(r, c - 1, '<', symbol == '<' ? score + 1 : score + 1001);
            visited.Remove((r, c));
            
        }
        
        Console.WriteLine(solutions.OrderBy(i => i).First());
    }
    
    private static (int r, int c) FindStartPosition(char[][] map)
    {
        for (var r = 0; r < map.Length; r++)
        {
            for (var c = 0; c < map[0].Length; c++)
            {
                if (map[r][c] == 'S')
                {
                    return (r, c);
                }
            }
        }
        return (0, 0);
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
