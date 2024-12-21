namespace AdventOfCode_2024_AOC;

public class Day18
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_18.txt";
    private readonly static List<(int, int)> Directions = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    
    public static void Solution_01()
    {
        var data = LoadBytes();
        char[][] map = CreateMap(data.Take(1024));

        var result = FindShortestPath(map);
        Console.WriteLine(result);
    }
    
    public static void Solution_02()
    {
        (int, int) result = default;
        var data = LoadBytes();
        
        var n = 1024;
        while (n < data.Count)
        {
            char[][] map = CreateMap(data.Take(n));
            if (FindShortestPath(map) is null)
            {
                result = data[n - 1];
                break;
            }
            n++;
        }

        Console.WriteLine($"{result.Item2},{result.Item1}");
    }
   
    private static int? FindShortestPath(char[][] map)
    {
        var end = (map.Length - 1, map[0].Length - 1);
            
        var heap = new PriorityQueue<((int, int), (int, int)), int>();
        var visited = new HashSet<((int, int), (int, int))>();
        heap.Enqueue(((0, 0), Directions[0]), 0);

        while (heap.TryDequeue(out ((int r, int c) p, (int r, int c) d) item, out int steps))
        {
            if (item.p == end)
            {
                return steps;
            }
            if (!visited.Add((item.p, item.d)))
            {
                continue; 
            }

            foreach ((int dr, int dc) in Directions)
            {
                (int r, int c) next = (item.p.r + dr, item.p.c + dc);

                if (next.r < 0 || next.r >= map.Length || next.c < 0 || next.c >= map[0].Length)
                {
                    continue;
                }
                if(map[next.r][next.c] == '#')
                {
                    continue;
                }
                    
                heap.Enqueue((next, (dr, dc)), steps + 1);
            }
        }

        return null;
    }

    private static char[][] CreateMap(IEnumerable<(int, int)> bytes)
    {
        int size = 71;
        char[][] map = new char[size][];
        
        for (int i = 0; i < size; i++)
        {
            var t = new char[size];
            Array.Fill(t, '.');
            map[i] = t;
        }
        
        foreach ((int br, int bc) in bytes)
        {
            map[br][bc] = '#';
        }

        return map;
    }

    private static List<(int, int)> LoadBytes()
    { 
            return File.ReadAllLines(Filepath)
            .Select(line => line.Split(','))
            .Select(p => (int.Parse(p[1]), int.Parse(p[0])))
            .ToList();
    }
}
