namespace AdventureOdCode_2024_AOC;

public class Day02
{
    private static readonly string Filepath = Directory.GetCurrentDirectory() + @"\input_02.txt";
    
    public static void Solution_01()
    {
        var lines = File.ReadAllLines(Filepath);
        int result = 0;

        foreach (var line in lines)
        {
            var report = line.Split(' ').Select(int.Parse).ToList();

            if (IsReportSafe(report))
            {
                result++;
            }
        }

        Console.WriteLine(result);
    }
    
    public static void Solution_02()
    {
        var lines = File.ReadAllLines(Filepath);
        int result = 0;

        foreach (var line in lines)
        {
            var report = line.Split(' ').Select(int.Parse).ToList();
            bool isSafe = false;
            
            foreach (var subset in GenerateSubsets(report))
            {
                if (IsReportSafe(subset))
                {
                    isSafe = true;
                }
            }
            if (isSafe)
            {
                result++;
            }
        }

        Console.WriteLine(result);
    }

    private static bool IsReportSafe(List<int> report)
    {
        if (report[0] < report[^1])
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                if (report[i + 1] < report[i] + 1 || report[i + 1] > report[i] + 3)
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                if (report[i + 1] > report[i] - 1 || report[i + 1] < report[i] - 3)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private static List<List<int>> GenerateSubsets(List<int> numbers)
    {
        var result = new List<List<int>>();
        for (int i = 0; i < numbers.Count; i++)
        {
            var subset = new List<int>(numbers);
            subset.RemoveAt(i);
            result.Add(subset);
        } 
        
        return result;
    }
}
