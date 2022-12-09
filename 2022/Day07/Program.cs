
using Day07;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

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
        //long result = GetResult1(inputCol);
        long result = GetResult2(inputCol);
        sw.Stop();

        Console.WriteLine(result);
        Console.WriteLine("Time was: " + sw.ElapsedMilliseconds + " ms.");
    }

    private static long GetResult1(List<string> inputCol)
    {

        NodeBase nb = GetFilledGraph( inputCol);
        long result = 0;
         nb.SmartDfs(ref result);
        return result;
    }

    private static long GetResult2(List<string> inputCol)
    {
        NodeBase nb = GetFilledGraph(inputCol);
        long result = 0;
        nb.SmartDfs(ref result);
        long freeSpace = 70000000 - nb.Size;
        long spaceToFree = 30000000-freeSpace;
        result=long.MaxValue;
        nb.SmartDfsSmallest(ref result, spaceToFree);
        return result;
    }

    private static NodeBase GetFilledGraph(List<string> inputCol)
    {
        NodeBase root=new NodeDir("/", 0);
        NodeBase currentNode=root;
        string strTmp;
        string[] strTmpArr;
        foreach(string str in inputCol)
        {
            if (str.Contains('$'))
            {
                strTmp= str.Replace("$","").Trim();
                if (strTmp.Length == 2)
                    continue;
                strTmp = strTmp.Replace("cd ", "").Trim();
                switch (strTmp)
                {
                    case "/":
                        currentNode = root;
                        break;
                    case "..":
                        currentNode = currentNode.GetAncestor();
                        break;
                        default:
                        currentNode=currentNode.GetChild(strTmp);
                        break;
                }
            }
            else
            {
                strTmpArr = str.Split(" ");
                if (strTmpArr[0] == "dir")
                    currentNode.AddChild(strTmpArr[1], 0);
                else
                    currentNode.AddChild(strTmpArr[1], int.Parse(strTmpArr[0]));
            }
        }

        return root;
    }



}
