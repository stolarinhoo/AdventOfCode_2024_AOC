using System.Text;

namespace AdventOfCode_2024_AOC;

public class Day15
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_15.txt";
    private readonly static Dictionary<char, (int, int)> Directions = new()
    {
        { '^', (-1, 0) }, { '>', (0, 1) }, { 'v', (1, 0) }, { '<', (0, -1) }
    };

    public static void Solution_01_Iterative()
    {
        (char[][] map, string moves) = PrepareData();
        var robotPos = FindStartPosition(map);
        
        foreach (char move in moves)
        {
            (int dr, int dc) = Directions[move];
            (int r, int c) newPos = (robotPos.r + dr, robotPos.c + dc);

            switch (map[newPos.r][newPos.c])
            {
                case '#':
                    continue;
                case 'O':
                {
                    (int r, int c) box = (newPos.r, newPos.c);
                    (int r, int c) next = (newPos.r + dr, newPos.c + dc);
                
                    while (map[next.r][next.c] != '#')
                    {
                        if (map[next.r][next.c] == '.')
                        {
                            Swap(map, box, next);
                            Swap(map, robotPos, newPos);
                            robotPos = newPos;
                            break;
                        
                        }
                        next = (next.r + dr, next.c + dc);
                    }
                    continue;
                }
                default:
                    Swap(map, robotPos, newPos);
                    robotPos = newPos;
                    break;
            }
        }
        
        Console.WriteLine(SumGpsCoordinates(map));
    }

    public static void Solution_01_Recursive()
    {
        (char[][] map, string moves) = PrepareData();
        var robotPos = FindStartPosition(map);

        foreach (char move in moves)
        {
            (int dr, int dc) dir = Directions[move];
            if (CanPush(map, robotPos, dir))
            {
                Push(map, robotPos, dir);
                robotPos = (robotPos.r + dir.dr, robotPos.c + dir.dc);
            }
        }
        
        Console.WriteLine(SumGpsCoordinates(map));
    }
    
    public static void Solution_02()
    {
        (char[][] oldMap, string moves) = PrepareData();
        char[][] map = EnlargeMap(oldMap);
        var robotPos = FindStartPosition(map);

        foreach (char move in moves)
        {
            (int dr, int dc) dir = Directions[move];
            if (CanPush(map, robotPos, dir))
            {
                Push(map, robotPos, dir);
                robotPos = (robotPos.r + dir.dr, robotPos.c + dir.dc);
            }
        }
        
        Console.WriteLine(SumGpsCoordinates(map));
    }

    private static bool CanPush(char[][] map, (int r, int c) pos, (int r, int c) dir)
    {
        (int r, int c) nextPos = (pos.r + dir.r, pos.c + dir.c);
        
        if (map[nextPos.r][nextPos.c] == '#')
        {
            return false;
        }
        else if (map[nextPos.r][nextPos.c] == '.')
        {
            return true;
        }
        else if (map[nextPos.r][nextPos.c] == 'O')
        {
            return CanPush(map, nextPos, dir);
        }
        else if (dir.r == 0)
        {
            return CanPush(map, nextPos, dir);
        }
        else
        {
            if (map[nextPos.r][nextPos.c] == '[')
            {
                return CanPush(map, nextPos, dir) && 
                       CanPush(map, (nextPos.r, nextPos.c + 1), dir);
            }
            else if (map[nextPos.r][nextPos.c] == ']')
            {
                return CanPush(map, nextPos, dir) && 
                       CanPush(map, (nextPos.r, nextPos.c - 1), dir);
            }
        }

        return true;
    }
    
    private static void Push(char[][] map, (int r, int c) pos, (int r, int c) dir)
    {
        (int r, int c) nextPos = (pos.r + dir.r, pos.c + dir.c);

        if (map[nextPos.r][nextPos.c] == '#')
        {
            return;
        }
        else if (map[nextPos.r][nextPos.c] == '.')
        {
            Swap(map, pos, nextPos);
        }
        else if (map[nextPos.r][nextPos.c] == 'O')
        {
            Push(map, nextPos, dir);
            Swap(map, pos, nextPos);
        }
        else if (dir.r == 0)
        {
            Push(map, nextPos, dir);
            Swap(map, pos, nextPos);
        }
        else
        {
            if (map[nextPos.r][nextPos.c] == '[')
            {
                Push(map, nextPos, dir);
                Push(map, (nextPos.r, nextPos.c + 1), dir);
            }
            else if(map[nextPos.r][nextPos.c] == ']')
            {
                Push(map, nextPos, dir);
                Push(map, (nextPos.r, nextPos.c - 1), dir);
            }
                
            Swap(map, pos, nextPos);
        }
    }
    
    private static char[][] EnlargeMap(char[][] map)
    {
        var newMap = new char[map.Length][];
        for (var r = 0; r < map.Length; r++)
        {
            newMap[r]  = new char[map[0].Length * 2];
            for (var c = 0; c < map[0].Length; c++)
            {
                int nc = c * 2;
                switch (map[r][c])
                {
                    case '#':
                        newMap[r][nc] = '#';
                        newMap[r][nc + 1] = '#';
                        continue;
                    case 'O':
                        newMap[r][nc] = '[';
                        newMap[r][nc + 1] = ']';
                        continue;
                    case '.':
                        newMap[r][nc] = '.';
                        newMap[r][nc + 1] = '.';
                        continue;
                    case '@':
                        newMap[r][nc] = '@';
                        newMap[r][nc + 1] = '.';
                        continue;
                }
            }
        }
        
        return newMap;
    }
    
    private static void Swap(char[][] matrix, (int r, int c) p1, (int r, int c) p2)
    {
        (matrix[p1.r][p1.c], matrix[p2.r][p2.c]) = (matrix[p2.r][p2.c], matrix[p1.r][p1.c]);
    }
    
    private static int SumGpsCoordinates(char[][] matrix)
    {
        var result = 0;
        for (var r = 0; r < matrix.Length; r++)
        {
            for (var c = 0; c < matrix[0].Length; c++)
            {
                if (matrix[r][c] == 'O' || matrix[r][c] == '[')
                {
                    result += (r * 100) + c;
                }
            }
        }
        return result;
    }

    private static void PrintMap(char[][] matrix)
    {
        foreach (char[] t in matrix)
        {
            for (var c = 0; c < matrix[0].Length; c++)
            {
                Console.Write(t[c]);
            }
            Console.WriteLine();
        }
    }
    
    private static (int r, int c) FindStartPosition(char[][] map)
    {
        for (var r = 0; r < map.Length; r++)
        {
            for (var c = 0; c < map[0].Length; c++)
            {
                if (map[r][c] == '@')
                {
                    return (r, c);
                }
            }
        }
        return (0, 0);
    }
    
    private static (char[][] matrix, string moves) PrepareData()
    {
        string[] lines = File.ReadAllLines(Filepath);
        char[][] map = new char[lines.Count(i => i.Contains('#'))][];
        var builder = new StringBuilder();
            
        for (var i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains('#'))
            {
                map[i] = line.ToCharArray();
            }
            else if (line.Contains('>') || line.Contains('<') || line.Contains('^') || line.Contains('v'))
            {
                builder.Append(line);
            }
        }
        return (map, builder.ToString());
    }
}
