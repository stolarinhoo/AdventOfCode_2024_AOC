using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace AdventOfCode_2024_AOC;

public class Day13
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_13.txt";

    public static void Solution_01()
    {
        var games = PrepareData();
        long result = 0;
    
        foreach (var game in games)
        {
            result += CalculateCheapestWay(game);
        }
        
        Console.WriteLine(result);
    }

    public static void Solution_02()
    {
        var games = PrepareData(add: 10000000000000);
        long result = 0;
    
        foreach (var game in games)
        {
            result += CalculateCheapestWay(game);
        }
        
        Console.WriteLine(result);
    }

    private static long CalculateCheapestWay(((long x, long y) buttonA, (long x, long y) buttonB, (long x, long y) prize) game)
    {
        var aCost = 3; var bCost = 1;
        
        long x = (game.buttonA.x * game.prize.y - game.buttonA.y * game.prize.x) /
                 (game.buttonA.x * game.buttonB.y - game.buttonA.y * game.buttonB.x);
        long y = (game.prize.x - game.buttonB.x * x) / game.buttonA.x;
    
        if ((y * game.buttonA.x + x * game.buttonB.x) == game.prize.x && (y * game.buttonA.y + x * game.buttonB.y) == game.prize.y)
        {
            return (y * aCost) + x;
        }

        return 0;
    }
    
    private static List<((long x, long y) buttonA, (long x, long y) buttonB, (long x, long y) prize)> PrepareData(long add = 0)
    {
        var lines = File.ReadAllLines(Filepath);
        List<((long, long), (long, long), (long, long))> games = new();
        ((long x, long y) buttonA, (long x, long y) buttonB, (long x, long y) prize) game = ((0, 0), (0, 0), (0, 0));
    
        string pattern = @"(?<=X\+)(\d+)|(?<=Y\+)(\d+)";
        foreach (string line in lines)
        {
            if (line.Contains("Button A"))
            {
                var nums = Regex.Matches(line, pattern);
                game.buttonA = (long.Parse(nums[0].ToString()), long.Parse(nums[1].ToString()));
            }
            else if (line.Contains("Button B"))
            {
                var nums = Regex.Matches(line, pattern);
                game.buttonB = (long.Parse(nums[0].ToString()), long.Parse(nums[1].ToString()));
            }
            else if(line.Contains("Prize"))
            {
                var nums = Regex.Matches(line, pattern.Replace(@"\+", @"\="));
                game.prize = (long.Parse(nums[0].ToString()) + add, long.Parse(nums[1].ToString()) + add);
            }
            else
            {
                games.Add(game);
                game = ((0, 0), (0, 0), (0, 0));
            }
        }
    
        return games;
    }
}
