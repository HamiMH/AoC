using System.Xml.Linq;

class Program
{
    private static int targetNumber=2020;
    static void Main(string[] args)
    {
        string lineIn;
        string[] lineArr;

        List<int> list = new List<int>();
        while ((lineIn = Console.ReadLine()) != null)
        {
            if (lineIn == "")
                break;
            list.Add(int.Parse(lineIn));
        }
        list.Sort();
        long result=GetMultiply(list);
        Console.WriteLine(result);
    }

    private static long GetMultiply(List<int> list)
    {
        int len=list.Count;
        int i, j,k, iNumber,jNumber,kNumber;
        for( i = 0; i < len; i++)
        {
            iNumber= list[i];
            for (j = i + 1; j < len; j++)
            {
                jNumber = list[j];
                for (k = i + 1; k < len; k++)
                {
                    kNumber = list[k];
                    if (iNumber + jNumber+ kNumber > targetNumber)
                        break;
                    if (iNumber + jNumber+ kNumber == targetNumber)
                        return iNumber * jNumber* kNumber;
                }
            }
        }

        return 0;
    }
}