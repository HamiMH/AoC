
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        List<string> inputCol = new List<string>();
        string lineIn1;
        //while ((lineIn1 = Console.ReadLine()) != null)
        while ((lineIn1 = Console.ReadLine()) != "eof")
        {
            //if (lineIn1 == "")
            //    break;

            inputCol.Add(lineIn1);
        }
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //int result = GetResult1(inputCol);
        int result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static int GetResult1(List<string> inputCol)
    {
        int index;
        int inputColRows = inputCol.Count;
        int indexOfTest = 1;
        int result = 0;
        for (int i = 0; i < inputColRows; i++)
        {
            index = 0;
            Node first = new Node(inputCol[i], ref index, inputCol[i].Length);
            i++;
            index = 0;
            Node second = new Node(inputCol[i], ref index, inputCol[i].Length);
            i++;

            if (Node.RightOrder(first, second) != false)
                result += indexOfTest;
            indexOfTest++;
        }
        return result;
    }


    private static int GetResult2(List<string> inputCol)
    {
        int index;
        Node tmp;
        int inputColRows = inputCol.Count;

        List<Node> packages = new List<Node>();

        for (int i = 0; i < inputColRows; i++)
        {
            if (inputCol[i] == "")
                continue;
            index = 0;
            tmp = new Node(inputCol[i], ref index, inputCol[i].Length);
            packages.Add(tmp);
        }
        index = 0;
        tmp = new Node("[[2]]", ref index, "[[2]]".Length,true);
        packages.Add(tmp);
        index = 0;
        tmp = new Node("[[6]]", ref index, "[[6]]".Length,true);
        packages.Add(tmp);
        packages.Sort();

        int result = 1;
        for (int i = 0; i < packages.Count; i++)
        {
            if (packages[i].ToFind) result *= (i + 1);
        }
        return result;
    }


}
