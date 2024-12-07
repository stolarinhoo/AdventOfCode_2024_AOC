namespace AdventOfCode_2024_AOC;

public class Day07
{
    private static readonly string Filepath = Directory.GetCurrentDirectory() + @"\input_07.txt";
    
    public static void Solution_01()
    {
        long result = 0;
        var lines = PrepareData();

        foreach (var l in lines)
        {
            if (Backtrack(l.numbers, l.target, 0, 0))
            {
                result += l.target;
            }
        }

        Console.WriteLine(result);
        
        bool Backtrack(List<long> numbers, long target, int index, long value)
        {
            if (index == numbers.Count)
            {
                if (value == target)
                {
                    return true;
                }
                return false;
            }
            
            return (Backtrack(numbers, target, index + 1, value * numbers[index]) ||
            Backtrack(numbers, target, index + 1, value + numbers[index]));
        }
    }
    
    public static void Solution_02()
    {
        long result = 0;
        var lines = PrepareData();

        foreach (var l in lines)
        {
            if (Backtrack(l.numbers, l.target, 0, 0))
            {
                result += l.target;
            }
        }
        
        Console.WriteLine(result);

        bool Backtrack(List<long> numbers, long target, int index, long value)
        {
            if (index == numbers.Count)
            {
                if (value == target)
                {
                    return true;
                }

                return false;
            }

            return (Backtrack(numbers, target, index + 1, value * numbers[index]) ||
                    Backtrack(numbers, target, index + 1, value + numbers[index]) ||
                    Backtrack(numbers, target, index + 1, long.Parse(value.ToString() + numbers[index].ToString())));
        }
    }
    
    
    private static List<(long target, List<long> numbers)> PrepareData()
    {
        var data = new List<(long, List<long>)>();
        string[] lines = File.ReadAllLines(Filepath);

        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var target = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToList();
            
            data.Add((target, numbers));
        }

        return data;
    }
}
