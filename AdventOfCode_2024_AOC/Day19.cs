namespace AdventOfCode_2024_AOC;

public class Day19
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_19.txt";
  
    public static void Solution_01_InsufficientPerformance()
    {
        int result = 0; 
        var (towelPatterns, desiredDesigns)= PrepareData();

        foreach (string design in desiredDesigns)
        {
            var stack = new Stack<string>();
            stack.Push(design);
            bool canConstruct = false;

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (string.IsNullOrEmpty(current))
                {
                    canConstruct = true;
                    break;
                }
                foreach (string pattern in towelPatterns)
                {
                    if (current.StartsWith(pattern))
                    {
                        stack.Push(current.Substring(pattern.Length));
                    }
                }
            }
            if (canConstruct)
            {
                result++;
            }
        }

        Console.WriteLine(result);
    }
  
    private static (List<string>, List<string>) PrepareData()
    {
        (List<string> towelPatterns, List<string> desiredDesigns) result = (new List<string>(), new List<string>());
        string[] lines = File.ReadAllLines(Filepath);

        foreach (string line in lines)
        {
            if(string.IsNullOrEmpty(line)) continue;

            if (line.Contains(','))
            {
                result.towelPatterns = line.Split(", ").ToList();
            }
            else
            {
                result.desiredDesigns.Add(line);
            }
        }

        return result;
    }
}
