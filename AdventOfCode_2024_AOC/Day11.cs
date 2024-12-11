using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode_2024_AOC;

public class Day11
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_11.txt";
    
    public static void Solution_03()
    {
        List<long> input = File.ReadAllText(Filepath).Split(" ").Select(long.Parse).ToList();
        LinkedList<long> stones = new LinkedList<long>(input);
        
        
        int blinks = 25;
        var node = stones.First;
        
        for (int i = 0; i < blinks; i++)
        {
            while (node is not null)
            {
                if (node.Value == 0)
                {
                    node.Value = 1;
                }
                else if (node.Value.ToString().Length % 2 == 0)
                {
                    var s = node.Value.ToString();
                    
                    var left = long.Parse(s[..(s.Length / 2)]);
                    var right = long.Parse(s[(s.Length / 2)..]);

                    node.Value = left;
                    stones.AddAfter(node, right);
                    node = node.Next;
                }
                else
                {
                    node.Value *= 2024;
                }
                
                node = node.Next;
            }

            node = stones.First;
        }
   
        Console.WriteLine(stones.Count);
    }

    public static void Solution_02()
    {
        List<long> stones = File.ReadAllText(Filepath).Split(" ").Select(long.Parse).ToList();
        Dictionary<(long, int), long> cache = new Dictionary<(long, int), long>();

        long res = 0;

        foreach (var stone in stones)
        {
            res += Solve(stone, 75);
        }

        Console.WriteLine(res);
        
        long Solve(long stone, int blink)
        {
            if (blink == 0)
            {
                return 1;
            }
            if (!cache.ContainsKey((stone, blink)))
            {
                long result = 0;

                if (stone == 0)
                {
                    result = Solve(1, blink - 1);
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    string s = stone.ToString();
                    result += Solve(long.Parse(s[..(s.Length / 2)]), blink - 1);
                    result += Solve(long.Parse(s[(s.Length / 2)..]), blink - 1);
                }
                else
                {
                    result = Solve(stone * 2024, blink - 1);
                }

                cache[(stone, blink)] = result;
            }

            return cache[(stone, blink)];
        }
    }
}
