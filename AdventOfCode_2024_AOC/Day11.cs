using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode_2024_AOC;

public class Day11
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_11.txt";
    
    public static void Solution_01()
    {
        List<long> stones = File.ReadAllText(Filepath).Split(" ").Select(long.Parse).ToList();
    
        int blinks = 25;
        while (blinks > 0)
        {
            for (int i = 0; i < stones.Count; i++)
            {
                if (stones[i] == 0)
                {
                    stones[i] = 1;
                    continue;
                }

                int digits = (int)Math.Log10(stones[i]) + 1;
                if (digits % 2 == 0)
                {
                    var stoneString = stones[i].ToString();
                    
                    var left = long.Parse(stoneString[..(digits / 2)]);
                    var right = long.Parse(stoneString[(digits / 2)..]);
                    
                    stones[i] = left;
                    stones.Insert(i++, right);
                    continue;
                }
                
                stones[i] *= 2024;
            }

            blinks--;
        }

        Console.WriteLine(stones.Count);
    }
}
