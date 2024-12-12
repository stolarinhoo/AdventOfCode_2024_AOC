namespace AdventOfCode_2024_AOC;

public class Day12
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_12.txt";

    public static void Solution_01()
    {
        char[][] map = PrepareData();
        int rows = map.Length;
        int cols = map[0].Length;
        var visited = new HashSet<(int, int)>();

        int result = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (!visited.Contains((r, c)))
                {
                    var region = new HashSet<(int, int)>();
                    DFS(r, c, map, visited, map[r][c], region);
                    result += region.Count * CalculatePerimeter(region);
                }
            }
        }

        Console.WriteLine(result);

        int CalculatePerimeter(HashSet<(int r, int c)> region)
        {
            int fence = 0;
            var directions = new HashSet<(int r, int c)>() { (0, -1), (-1, 0), (0, 1), (1, 0) };
            foreach (var plant in region)
            {
                foreach (var dir in directions)
                {
                    int dr = plant.r + dir.r;
                    int dc = plant.c + dir.c;

                    if (!region.Contains((dr, dc)))
                    {
                        fence++;
                    }
                }
            }
            
            return fence;
        }
    }
    
    public static void Solution_02()
    {
        char[][] map = PrepareData();
        int rows = map.Length;
        int cols = map[0].Length;
        var visited = new HashSet<(int, int)>();
    
        int result = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (!visited.Contains((r, c)))
                {
                    
                    var region = new HashSet<(int, int)>();
                    DFS(r, c, map, visited, map[r][c], region);
                    result += region.Count * CalculateSides(region);
                }
            }
        }
    
        Console.WriteLine(result);
    
        int CalculateSides(HashSet<(int r, int c)> region)
        {
            var perimeter = new HashSet<((int r, int c), (int r, int c))>();
            var directions = new HashSet<(int r, int c)>() { (0, -1), (-1, 0), (0, 1), (1, 0) };
            foreach (var square in region)
            {
                foreach (var dir in directions)
                {
                    int dr = square.r + dir.r;
                    int dc = square.c + dir.c;
    
                    if (!region.Contains((dr, dc)))
                    {
                        perimeter.Add((square, (dr, dc)));
                    }
                }
            }
            
            var sides = new HashSet<((int, int), (int, int))>();
            foreach (var (p1, p2) in perimeter)
            {
                bool isSideUnique = true;

                foreach (var (dr, dc) in new HashSet<(int, int)>() { (1, 0), (0, 1) })
                {
                    var p1n = (p1.r + dr, p1.c + dc);
                    var p2n = (p2.r + dr, p2.c + dc);
    
                    if (perimeter.Contains((p1n, p2n)))
                    {
                        isSideUnique = false;
                    }
                }
                if (isSideUnique)
                {
                    sides.Add((p1, p2));
                }
            }
            
            return sides.Count;
        }
    }
    
    
    private static void DFS(int r, int c, char[][] map, HashSet<(int, int)> visited, char plant, HashSet<(int, int)> region)
    {
        if (r < 0 || r >= map.Length ||
            c < 0 || c >= map[0].Length ||
            visited.Contains((r, c)) ||
            map[r][c] != plant)
        {
            return;
        }
        
        visited.Add((r, c));
        region.Add((r, c));
    
        DFS(r, c - 1, map, visited, plant, region);
        DFS(r - 1, c, map, visited, plant, region);
        DFS(r, c + 1, map, visited, plant, region);
        DFS(r + 1, c, map, visited, plant, region);
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
