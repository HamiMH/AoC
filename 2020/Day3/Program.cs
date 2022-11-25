using Day3;
using System.Xml.Linq;

class Program
{
    private static int[] slopeArrX = { 1, 3, 5, 7, 1 };
    private static int[] slopeArrY = { 1, 1, 1, 1, 2 };
    static void Main(string[] args)
    {
        string lineIn;

        TreeLayout treeLayout = new TreeLayout();
        while ((lineIn = Console.ReadLine()) != null)
        {
            if (lineIn == "")
                break;
            treeLayout.AddLine(lineIn);
        }

        long result = GetCount(treeLayout);
        Console.WriteLine(result);
    }

    private static long GetCount(TreeLayout treeLayout)
    {
        long count = 0;
        long countAll = 1;

        int x, y, stepX, stepY;
        for (int i = 0; i < 5; i++)
        {
            x = slopeArrX[i];
            y = slopeArrY[i];
            while (treeLayout.IsInGrid(x, y))
            {
                if (treeLayout.GetCoordinate(x, y))
                    count++;
                x += slopeArrX[i];
                y += slopeArrY[i];
            }
            countAll *= count;
            Console.WriteLine(count);
            count = 0;
        }




        return countAll;
    }
}