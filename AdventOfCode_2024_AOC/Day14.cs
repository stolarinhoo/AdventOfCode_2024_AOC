using System.Text.RegularExpressions;

namespace AdventOfCode_2024_AOC;

public class Day14
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_14.txt";

    public static void Solution_01()
    {
        var robots = PrepareData();
        int rows = 103;
        int cols = 101;
        
        for (int s = 1; s <= 100; s++)
        {
            for (int i = 0; i < robots.Count; i++)
            {
                var robot = robots[i];
                robot.pos = CalculatePosition(robot, rows, cols);
                robots[i] = robot;
            }
        }
        
        int horizontalMid = (cols / 2);
        int verticalMid = (rows / 2);
        var leftUp = robots.Where(item => item.pos.r < verticalMid && item.pos.c < horizontalMid).ToList();
        var rightUp = robots.Where(item => item.pos.r < verticalMid && item.pos.c > horizontalMid).ToList();
        var leftDown = robots.Where(item => item.pos.r > verticalMid && item.pos.c < horizontalMid).ToList();
        var rightDown = robots.Where(item => item.pos.r > verticalMid && item.pos.c > horizontalMid).ToList();

        int result = leftUp.Count * rightUp.Count * leftDown.Count * rightDown.Count;
        Console.WriteLine(result);
    }
    
    public static void Solution_02()
    {
        var robots = PrepareData();
        int rows = 103;
        int cols = 101;
        
        var states = new List<(int second, double ratio)>();
        
        for (int s = 1; s <= 100000; s++)
        {
            for (int i = 0; i < robots.Count; i++)
            {
                var robot = robots[i];
                robot.pos = CalculatePosition(robot, rows, cols);
                robots[i] = robot;
            }

            states.Add((s, CalculateNeighborRatio(robots.Select(r => r.pos).ToHashSet())));
        }

        var sortedStates = states
            .OrderByDescending(i => i.ratio)
            .ThenBy(i => i.second)
            .ToList();

        Console.WriteLine(sortedStates.First().second);
    }
    
    private static double CalculateNeighborRatio(HashSet<(int r, int c)> robots)
    {
        int hasNeighbourhood = 0;
        var directions = new HashSet<(int, int)>()
        {
            (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1),  (1, 0),  (1, 1)
        };

        foreach (var robot in robots)
        {
            foreach ((int dr, int dc) in directions)
            {
                var neighborPos = (robot.r + dr, robot.c + dc);
                if (robots.Contains(neighborPos))
                {
                    hasNeighbourhood++;
                    break;
                }
            }
        }

        return hasNeighbourhood / (double)robots.Count;
    }
    
    private static (int, int) CalculatePosition(((int r, int c) pos, (int r, int c) vel) robot, int rows, int cols)
    {
        return (((robot.pos.r + robot.vel.r) % rows + rows) % rows, ((robot.pos.c + robot.vel.c) % cols + cols) % cols);
    }
    
    private static void PrintMatrix(List<((int r, int c) pos, (int r, int c) vel)> robots, int rows, int cols)
    {
        int[][] matrix = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            matrix[i] = new int[cols];
        }
        
        foreach (var robot in robots)
        {
            int r = robot.pos.r;
            int c = robot.pos.c;

            if (matrix[r][c] < 2)
            {
                matrix[r][c]++;
            }
        }
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Console.Write(matrix[r][c] == 0 ? "." : matrix[r][c].ToString());
            }
            Console.WriteLine();
        }
    }
    
    private static List<((int r, int c) pos, (int r, int c) vel)> PrepareData()
    {
        List<((int, int), (int, int))> robots = new();
        var lines = File.ReadAllLines(Filepath);

        ((int, int) position, (int, int) velocity) robot;
        string pattern = @"(?<=p\=)[\-]?\d+,\-?\d+|(?<=v\=)[\-]?\d+,\-?\d+";
        foreach (string line in lines)
        {
            if(string.IsNullOrEmpty(line)) continue;
            var numbers = Regex.Matches(line, pattern);
            var pos = numbers[0].Value.Split(",").Select(int.Parse).ToArray();
            var vel = numbers[1].Value.Split(",").Select(int.Parse).ToArray();
            robot.position = (pos[1], pos[0]);
            robot.velocity = (vel[1], vel[0]);
            robots.Add(robot);
        }
        return robots;
    }
}
