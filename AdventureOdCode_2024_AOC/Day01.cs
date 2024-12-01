namespace AdventureOdCode_2024_AOC;

public class Day01
{
    private static readonly string Filepath = Directory.GetCurrentDirectory() + @"\input_01.txt";

    public static void Solution_01()
    {
        int result = 0;
        var lines = File.ReadAllLines(Filepath);
        
        var heap1 = new PriorityQueue<int, int>();
        var heap2 = new PriorityQueue<int, int>();

        foreach (var line in lines)
        {
            var values = line.Split("   ");
            heap1.Enqueue(int.Parse(values[0]), int.Parse(values[0]));
            heap2.Enqueue(int.Parse(values[1]), int.Parse(values[1]));
        }
        
        while (heap1.Count > 0 && heap2.Count > 0)
        {
            int val1 = heap1.Dequeue();
            int val2 = heap2.Dequeue();
            result += Math.Abs(val1 - val2);
        }

        Console.WriteLine(result);
    }

    public static void Solution_02()
    {
        int result = 0;
        var lines = File.ReadAllLines(Filepath);
        var counter = new Dictionary<string, int>();
        
        foreach (var line in lines)
        {
            var values = line.Split("   ");
            if (!counter.ContainsKey(values[1]))
            {
                counter[values[1]] = 0;
            }

            counter[values[1]]++;
        }
        
        foreach (var line in lines)
        {
            var values = line.Split("   ");
            int occurrences = counter.ContainsKey(values[0]) ? counter[values[0]] : 0;
            result += int.Parse(values[0]) * occurrences;
        }

        Console.WriteLine(result);
    }
}