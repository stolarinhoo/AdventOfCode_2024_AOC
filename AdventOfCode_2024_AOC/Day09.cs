using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode_2024_AOC;

public class Day09
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_09.txt";
    
    public static void Solution_01()
    {
        string input = File.ReadAllText(Filepath);
        List<int> block = ToBlock(input);

        int left = 0;
        int right = block.Count - 1;
        
        long result = 0;
        int mul = 0;
        
        while (left <= right)
        {
            if (block[left] != -1)
            {
                result += mul * block[left];
                mul++;
            }
            else
            {
                while (right >= left && block[right] == -1)
                {
                    right--;
                }
                
                result += mul * block[right];
                mul++;
                right--;
            }

            left++;
        }

        Console.WriteLine(result);
    }

    public static void Solution_02()
    {
        string diskMap = File.ReadAllText(Filepath);

        var block = ToBlock(diskMap);
        List<(int index, int count, int from, int to)> sequences = GetIndexSequences(block);

        foreach (var seq in sequences)
        {
            int left = 0;

            while (left < block.Count)
            {
                int current = block[left];
                int count = 0;

                while (left < block.Count && current == block[left])
                {
                    count++;
                    left++;
                }

                if (current != -1) continue;

                if (count >= seq.count && left <= seq.to)
                {
                    int n = 0;
                    while (n < seq.count)
                    {
                        block[left - count + n] = seq.index;
                        block[seq.from + n] = -1;
                        n++;
                    }

                    break;
                }
            }
        }

        long result = CalculateChecksum2(block);
        Console.WriteLine(result);
    }

    private static long CalculateChecksum2(List<int> block)
    {
        long result = 0;
        int mult = 0;
        foreach (int number in block)
        {
            if (number == -1)
            {
                mult++;
                continue;
            }
            result += number * mult;
            mult++;
        }

        return result;
    }

    private static List<(int index, int count, int from, int to)> GetIndexSequences(List<int> block)
    {
        var result = new List<(int index, int count, int from, int to)>();
        int right = block.Count - 1;
        while (right >= 0)
        {
            int current = block[right];
            int count = 0;
            while (right >= 0 && block[right] == -1)
            {
                right--;
            }
            while (right >= 0 && block[right] == current)
            {
                count++;
                right--;
            }
            if (current != -1)
            {
                result.Add((current, count, right + 1, right + count));
            }
        }

        return result;
    }
    
    private static List<int> ToBlock(string input)
    {
        var result = new List<int>();
        int index = 0;
        bool isFile = true;
        
        foreach (char ch in input)
        {
            int amount = ch - '0';
            if(amount < 0) continue;
            
            if (isFile)
            {
                result.AddRange(Enumerable.Repeat(index, amount));
                index++;
            }
            else
            {
                result.AddRange(Enumerable.Repeat(-1, amount));
            }

            isFile = !isFile;
        }

        return result;
    }
}
