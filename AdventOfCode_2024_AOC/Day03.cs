using System.Text.RegularExpressions;

namespace AdventOfCode_2024_AOC;

public class Day03
{
    private static readonly string Filepath = Directory.GetCurrentDirectory() + @"\input_03.txt";
    
    public static void Solution_01()
    {
        int result = 0;
        string data = string.Join("", File.ReadAllLines(Filepath));
        const string pattern = @"(?<=mul\()\d+,\d+(?=\))";

        var matches = FindMatches(pattern, data);
        
        foreach (var match in matches)
        {
            (int operand1, int operand2) = SplitOperands(match);
            result += operand1 * operand2;
        }

        Console.WriteLine(result);
    }
    
    public static void Solution_02()
    {
        int result = 0;
        string data = string.Join("", File.ReadAllLines(Filepath));
        const string pattern = @"(?<=mul\()\d+,\d+(?=\))|do\(\)|don't\(\)";
        
        var matches = FindMatches(pattern, data);

        bool doMultiply = true;
        foreach (var match in matches)
        {
            switch (match)
            {
                case "do()":
                    doMultiply = true;
                    break;
                case "don't()":
                    doMultiply = false;
                    break;
                default:
                {
                    if(doMultiply)
                    {
                        (int operand1, int operand2) = SplitOperands(match);
                        result += operand1 * operand2;
                    }
                    break;
                }
            }
        }

        Console.WriteLine(result);
    }
    
    private static IEnumerable<string> FindMatches(string pattern, string input)
    {
        var r = new Regex(pattern);
        return r.Matches(input).Select(i => i.Value);
    }

    private static ValueTuple<int, int> SplitOperands(string input)
    {
        string[] parts = input.Split(",");
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
}
