using Day2;
using System.Xml.Linq;

class Program
{
    private static int targetNumber = 2020;
    static void Main(string[] args)
    {
        string lineIn;
        string scndTerm;
        string[] lineArr;
        string[] lineArr1;

        List<DataLine> list = new List<DataLine>();
        while ((lineIn = Console.ReadLine()) != null)
        {
            if (lineIn == "")
                break;
            lineArr=lineIn.Split(" ");
            lineArr1 = lineArr[0].Split("-");
            scndTerm=lineArr[1].Replace(":","");

            IRule rule = new Rule2(int.Parse(lineArr1[0]), int.Parse(lineArr1[1]), lineArr[1][0]);
            DataLine dataLine = new DataLine(rule, lineArr[2]);
            list.Add(dataLine);
        }


        long result = GetCount(list);
        Console.WriteLine(result);
    }

    private static long GetCount(List<DataLine> list)
    {
        long count = 0;
        foreach(DataLine dataLine in list)
        {
            if (dataLine.LineIsValid())
                count++;
        }

        return count;
    }
}