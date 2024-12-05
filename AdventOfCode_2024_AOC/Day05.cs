using System.Data.Common;

namespace AdventOfCode_2024_AOC;

public class Day05
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_05.txt";
    
    public static void Solution_01()
    {
        int result = 0;
        (HashSet<string> rules, List<string[]> updates) = PrepareData();

        foreach (var update in updates)
        {
            if (IsUpdateCorrect(update, rules))
            {
                result += int.Parse(update[update.Length / 2]);
            }
        }
        
        Console.WriteLine(result);
    }
    
    public static void Solution_02()
    {
        int result = 0;
        (HashSet<string> rules, List<string[]> updates) = PrepareData();
        var incorrectUpdates = new List<string[]>();
        
        foreach (var update in updates)
        {
            if (!IsUpdateCorrect(update, rules))
            {
                incorrectUpdates.Add(update);
            }
        }

        foreach (var update in incorrectUpdates)
        {
            for (int i = 0; i < update.Length; i++)
            {
                for (int j = i + 1; j < update.Length; j++)
                {
                    string rule = $"{update[i]}|{update[j]}";
                    if(!rules.Contains(rule))
                    {
                        Swap(i, j, update);
                        if (rules.Contains(rule))
                        {
                            break;
                        }
                    }
                }
            }

            result += int.Parse(update[update.Length / 2]);
        }
        
        Console.WriteLine(result);
    }
    
    private static bool IsUpdateCorrect(string[] update, HashSet<string> rules)
    {
        for (int i = 0; i < update.Length; i++)
        {
            for (int j = i + 1; j < update.Length; j++)
            {
                string rule = $"{update[i]}|{update[j]}";
                if (!rules.Contains(rule))
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private static void Swap(int left, int right, IList<string> array)
    {
        (array[left], array[right]) = (array[right], array[left]);
    }

    private static (HashSet<string>, List<string[]>) PrepareData()
    {
        var rules = new HashSet<string>();
        var updates = new List<string[]>();
        string[] lines = File.ReadAllLines(Filepath);

        foreach (var line in lines)
        {
            if (line.Contains('|'))
            {
                rules.Add(line);
            }
            else if (line.Contains(','))
            {
                updates.Add(line.Split(','));
            }
        }

        return (rules, updates);
    }
}