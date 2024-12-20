using System.Text.RegularExpressions;

namespace AdventOfCode_2024_AOC;

public class Day17
{
    private readonly static string Filepath = Directory.GetCurrentDirectory() + @"\input_17.txt";

    public static void Solution_01()
    {
        var (a, b, c, program) = PrepareData();
        var result = Run(a, b, c, program);

        Console.WriteLine(string.Join(',', result));
    }

    private static List<int> Run(int a, int b, int c, List<int> program)
    {
        var result = new List<int>();

        for (int i = 0; i < program.Count; i += 2)
        {
           int operand = program[i + 1];

           if (program[i] == 0) // adv
           {
               a = a >> Combo(operand);
           }
           else if (program[i] == 1) // bxl
           {
               b = b ^ operand;
           }
           else if (program[i] == 2) // bst
           {
               b = Combo(operand) & 7;
           }
           else if (program[i] == 3) // jnz
           {
               if (a != 0)
               {
                   i = operand - 2;
               }
           }
           else if (program[i] == 4) // bxc
           {
               b ^= c;
           }
           else if (program[i] == 5) // out
           {
               result.Add(Combo(operand) & 7);
           }
           else if (program[i] == 6) // bdv
           {
               b = a >> Combo(operand);
           }
           else if (program[i] == 7) // cdv
           {
               c = a >> Combo(operand);
           }
           
        }

        return result;

        int Combo(int operand)
        {
            return operand switch
            {
                0 or 1 or 2 or 3 => operand,
                4 => a,
                5 => b,
                6 => c,
                _ => operand
            };

        }
    }

    private static (int a, int b, int c, List<int> p) PrepareData()
    {
        (int a, int b, int c, List<int> p) result = default;
        string[] lines = File.ReadAllLines(Filepath);

        foreach (string line in lines)
        {
            if (line.Contains("A"))
            {
                result.a = int.Parse(Regex.Match(line, @"\d+").ToString());
            }
            else if (line.Contains("B"))
            {
                result.b = int.Parse(Regex.Match(line, @"\d+").ToString());
            }
            else if (line.Contains("C"))
            {
                result.c = int.Parse(Regex.Match(line, @"\d+").ToString());
            }
            else if (line.Contains("Program"))
            {
                result.p = line.Split(' ')[1].Split(',').Select(int.Parse).ToList();
            }
        }

        return result;
    }
}