namespace AdventOfCode_2024_AOC;

public class Day06
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_06.txt";

    private static Dictionary<char, (int, int)> directions = new()
    {
        { '^', (-1, 0) }, { '>', (0, 1) }, { 'v', (1, 0) }, { '<', (0, -1) }
    };

    private static Dictionary<char, char> nextDirections = new()
    {
        { '^', '>' }, { '>', 'v' }, { 'v', '<' }, { '<', '^' },
    };


    public static void Solution_1()
    {
        int path = 1;
        var matrix = PrepareData();
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        var startPosition = FindStartPosition(matrix);
        PathFinder(startPosition.row, startPosition.col, '^', (-1, 0), new HashSet<(int, int)>());

        Console.WriteLine(path);

        void PathFinder(int row, int col, char nextDir, (int, int) direction, HashSet<(int, int)> visited)
        {
            if (row + direction.Item1 < 0 || row + direction.Item1 >= rows ||
                col + direction.Item2 < 0 || col + direction.Item2 >= cols)
            {
                return;
            }

            char nextChar = matrix[row + direction.Item1][col + direction.Item2];

            if (nextChar == '#')
            {
                nextDir = nextDirections[nextDir];
                direction = directions[nextDir];
            }
            
            if (!visited.Contains((row, col)))
            {
                path++;
                visited.Add((row, col));
            }

            PathFinder(row + direction.Item1, col + direction.Item2, nextDir, direction, visited);
        }
    }


    public static void Solution_2()
    {
        int loops = 0;
        var matrix = PrepareData();
        int rows = matrix.Length; int cols = matrix[0].Length;
        var startPosition = FindStartPosition(matrix);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i][j] != '#' && matrix[i][j] != '^')
                {
                    matrix[i][j] = '#';
                    if (LoopDetection(startPosition.row, startPosition.col, '^', (-1, 0),
                            new HashSet<(int, int, char)>()))
                    {
                        loops++;
                    }
                    matrix[i][j] = '.';
                }
            }
        }

        Console.WriteLine(loops);
        
        bool LoopDetection(int row, int col, char nextDir, (int, int) direction, HashSet<(int, int, char)> visited)
        {
            if (row + direction.Item1 < 0 || row + direction.Item1 >= rows ||
                col + direction.Item2 < 0 || col + direction.Item2 >= cols)
            {
                return false;
            }

            if (visited.Contains((row, col, nextDir)))
            {
                return true;
            }
            visited.Add((row, col, nextDir));

            char nextChar = matrix[row + direction.Item1][col + direction.Item2];
            if (nextChar == '#')
            {
                nextDir = nextDirections[nextDir];
                direction = directions[nextDir];

                return LoopDetection(row, col, nextDir, direction, visited);
            }

            return LoopDetection(row + direction.Item1, col + direction.Item2, nextDir, direction, visited);
        }
    }

    
    private static (int row, int col) FindStartPosition(char[][] matrix)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (matrix[row][col] == '^')
                {
                    return (row, col);
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
