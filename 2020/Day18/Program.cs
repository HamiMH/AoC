
using Day18;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        List<string> inputCol = new List<string>();
        string lineIn1;
        while ((lineIn1 = Console.ReadLine()) != null)
        //while ((lineIn1 = Console.ReadLine()) != "eof")
        {
            if (lineIn1 == "")
                break;

            inputCol.Add(lineIn1);
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        long result = SumOfLines(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long SumOfLines(List<string> inputCol)
    {
        long result = 0;
        int index;
        string tmpStr;
       foreach(string str in inputCol)
        {
            tmpStr = str.Replace(" ", "");
            index = 0;
            //result+=CalculateLine(tmpStr, ref index);
            result+=(new UltimateParser(tmpStr)).Calculate();
        }
       return result;
    }

    private static long CalculateLine(string str, ref int v)
    {
        long result = 0;
        if (str[v] == '(')
        {
            v++;
            result = CalculateLine(str, ref v);
            v++;
        }
        else
        {
            result = (str[v] - '0');
            v++;
        }
        char cmd = ' ';
        long value = -1;
        for (; v < str.Length; v++)
        {
            value = -1;
            if (str[v] == '(')
            {
                v++;
                value = CalculateLine(str, ref v);
            }
            else if (str[v] == '+')
                cmd = '+';
            else if (str[v] == '*')
                cmd = '*';
            else if (str[v] == ')')
            {
                return result;
            }
            else
            {
                value = (str[v] - '0');
            }
            if (value != -1)
            {
                if (cmd == '+')
                    result += value;
                else if (cmd == '*')
                    result *= value;
                else
                    throw new Exception("Wrong command");
            }
        }

        return result;
    }

    private static long CalculateLineV2(string str, ref int v)
    {
        long result = 0;
        if (str[v] == '(')
        {
            v++;
            result = CalculateLineV2(str, ref v);
            v++;
        }
        else
        {
            result = (str[v] - '0');
            v++;
        }
        char cmd = ' ';
        long value = -1;
        for (; v < str.Length; v++)
        {
            value = -1;
            if (str[v] == '(')
            {
                v++;
                value = CalculateLineV2(str, ref v);
            }
            else if (str[v] == '+')
                cmd = '+';
            else if (str[v] == '*')
                cmd = '*';
            else if (str[v] == ')')
            {
                return result;
            }
            else
            {
                value = (str[v] - '0');
            }
            if (value != -1)
            {
                if (cmd == '+')
                    result += value;
                else if (cmd == '*')
                    result *= value;
                else
                    throw new Exception("Wrong command");
            }
        }

        return result;
    }
}