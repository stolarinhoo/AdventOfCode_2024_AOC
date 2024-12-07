namespace AdventOfCode_2024_AOC;

public class Day07
{
    private static readonly string Filepath = Directory.GetCurrentDirectory() + @"\input_07.txt";

    public static void Solution_01()
    {
        var lines = PrepareData();
        var targets = new HashSet<long>();

        foreach (var l in lines)
        {
            Backtrack(l.numbers, l.target, 0, 1);
        }
        
        var result = targets.Sum();
        Console.WriteLine(result);
        
        
        void Backtrack(List<long> numbers, long target, int index, long value)
        {
            if (value == target)
            {
                targets.Add(target);
                return;
            }
            if (value > target || index >= numbers.Count)
            {
                return;
            }
            
            Backtrack(numbers, target, index + 1, value * numbers[index]);
            Backtrack(numbers, target, index + 1, value + numbers[index]);
        }
    }
    
    public static void Solution_02()
    {
        var lines = PrepareData();
        var targets = new HashSet<long>();

        foreach (var l in lines)
        {
            Backtrack(l.numbers, l.target, 0, 1);
        }
        
        var result = targets.Sum();
        Console.WriteLine(result);

        
        void Backtrack(List<long> numbers, long target, int index, long value)
        {
            if (value == target)
            {
                targets.Add(target);
                return;
            }
            if (value > target || index >= numbers.Count)
            {
                return;
            }
            
            Backtrack(numbers, target, index + 1, value * numbers[index]);
            Backtrack(numbers, target, index + 1, value + numbers[index]);
            Backtrack(numbers, target, index + 1, long.Parse(value.ToString() + numbers[index].ToString()));
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
