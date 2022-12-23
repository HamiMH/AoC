internal class ElfGarden
{
    Dictionary<string, Tuple<int, int>> Direct = new Dictionary<string, Tuple<int, int>>()
    {
        {"N",new Tuple<int, int>(0,-1) },
        {"S",new Tuple<int, int>(0,1) },
        {"E",new Tuple<int, int>(1,0) },
        {"W",new Tuple<int, int>(-1,0) },
    };

    Dictionary<string, List<Tuple<int, int>>> CheckFree = new Dictionary<string, List<Tuple<int, int>>>()
    {
        {
            "N",new List<Tuple<int, int>>(){
            { new Tuple<int, int>(-1,-1) },
            { new Tuple<int, int>(0,-1) },
            { new Tuple<int, int>(1,-1) },
                }
        },
        {
            "S",new List<Tuple<int, int>>(){
            { new Tuple<int, int>(-1,1) },
            { new Tuple<int, int>(0,1) },
            { new Tuple<int, int>(1,1) },
                }
        },
        {
            "E",new List<Tuple<int, int>>(){
            { new Tuple<int, int>(1,-1) },
            { new Tuple<int, int>(1,0) },
            { new Tuple<int, int>(1,1) },
                }
        },
        {
            "W",new List<Tuple<int, int>>(){
            { new Tuple<int, int>(-1,-1) },
            { new Tuple<int, int>(-1,0) },
            { new Tuple<int, int>(-1,1) },
                }
        },
    };
    Dictionary<Tuple<int, int>, int> FreqOfPlace = new Dictionary<Tuple<int, int>, int>();
    List<Elf> Elves;
    List<string> Order = new List<string>() { "N", "S", "W", "E" };
    Dictionary<Tuple<int, int>, Elf> UsedPlaces = new Dictionary<Tuple<int, int>, Elf>();

    public ElfGarden(List<string> inputCol)
    {
        Elves = new List<Elf>();
        int i, j, nOfCol, nOfRow;
        nOfRow = inputCol.Count;
        nOfCol = inputCol[0].Length;

        for (i = 0; i < nOfRow; i++)
            for (j = 0; j < nOfCol; j++)
                if (inputCol[i][j] == '#')
                    Elves.Add(new Elf(j, i));
    }

    internal long GetSize(int nOfSteps)
    {
        int i, j, iter;
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;
        for (iter = 0; iter < nOfSteps; iter++)
        {
            FreqOfPlace.Clear();

            if (MakeProp(iter) == false)
                return iter + 1;

            MakeMoves();

            UsedPlaces.Clear();
            foreach (Elf elf in Elves)
            {
                elf.Reset();
                UsedPlaces.Add(new Tuple<int, int>(elf.PosX, elf.PosY), elf);
            }

            //minX = int.MaxValue;
            //maxX = int.MinValue;
            //minY = int.MaxValue;
            //maxY = int.MinValue;
            //Console.WriteLine(iter);
            //foreach (Elf elf in Elves)
            //{
            //    minX = Math.Min(minX, elf.PosX);
            //    minY = Math.Min(minY, elf.PosY);
            //    maxX = Math.Max(maxX, elf.PosX);
            //    maxY = Math.Max(maxY, elf.PosY);
            //}
            //for (i = minY; i <= maxY; i++)
            //{
            //    Console.WriteLine();
            //    for (j = minX; j <= maxX; j++)
            //    {
            //        if (UsedPlaces.ContainsKey(new Tuple<int, int>(j, i)))
            //            Console.Write(UsedPlaces[new Tuple<int, int>(j, i)].MyIndexStr());
            //        else
            //            Console.Write(".; ");

            //    }

            //}
            //Console.WriteLine();
            //Console.WriteLine();
        }
        minX = int.MaxValue;
        maxX = int.MinValue;
        minY = int.MaxValue;
        maxY = int.MinValue;
        foreach (Elf elf in Elves)
        {
            minX = Math.Min(minX, elf.PosX);
            minY = Math.Min(minY, elf.PosY);
            maxX = Math.Max(maxX, elf.PosX);
            maxY = Math.Max(maxY, elf.PosY);
        }
        for (i = minY; i <= maxY; i++)
        {
            Console.WriteLine();
            for (j = minX; j <= maxX; j++)
            {
                if (UsedPlaces.ContainsKey(new Tuple<int, int>(j, i)))
                    Console.Write(UsedPlaces[new Tuple<int, int>(j, i)].MyIndexStr());
                else
                    Console.Write(".; ");

            }

        }
        Console.WriteLine();
        return (maxY - minY + 1) * (maxX - minX + 1)-Elves.Count;
    }

    private void MakeMoves()
    {
        foreach (Elf elf in Elves)
        {
            if (elf.MadeDecision == false || elf.NotMoving)
                continue;

            Tuple<int, int> tup = new Tuple<int, int>(elf.PosX + elf.MoveX, elf.PosY + elf.MoveY);
            if (FreqOfPlace[tup] > 1)
                continue;
            elf.PosX += elf.MoveX;
            elf.PosY += elf.MoveY;
        }
    }

    private bool MakeProp(int startIndex)
    {
        startIndex %= 4;
        int index = startIndex;
        UsedPlaces.Clear();
        foreach (Elf elf in Elves)
        {
            elf.Reset();
            UsedPlaces.Add(new Tuple<int, int>(elf.PosX, elf.PosY), elf);
        }
        bool noOneMoves = true;
        foreach (Elf elf in Elves)
        {
            noOneMoves&=IsNonMoving(elf);
        }
        if (noOneMoves)
            return false;
        while (true)
        {
            foreach (Elf elf in Elves)
            {

                if (elf.MadeDecision||elf.NotMoving)
                    continue;

                if (CanMove(elf, Order[index]) == false)
                    continue;


            }

            index++;
            index %= 4;
            if (index == startIndex)
                break;
        }
        return true;
    }

    private bool IsNonMoving(Elf elf)
    {
        int i, j;
        for (i = elf.PosY - 1; i <= elf.PosY + 1; i++)
            for (j = elf.PosX - 1; j <= elf.PosX + 1; j++)
            {
                if (i == elf.PosY && j == elf.PosX)
                    continue;
                if (UsedPlaces.ContainsKey(new Tuple<int, int>(j, i)))
                    return false;
            }
        elf.NotMoving = true;
        return true;
    }

    private bool CanMove(Elf elf, string dir)
    {
        List<Tuple<int, int>> checkFree = CheckFree[dir];
        foreach (Tuple<int, int> tup in checkFree)
        {
            if (UsedPlaces.ContainsKey(new Tuple<int, int>(elf.PosX + tup.Item1, elf.PosY + tup.Item2)))
                return false;
        }
        Tuple<int, int> tup1 = Direct[dir];
        Tuple<int, int> tup2 = new Tuple<int, int>(elf.PosX + tup1.Item1, elf.PosY + tup1.Item2);
        elf.MoveX = tup1.Item1;
        elf.MoveY = tup1.Item2;
        if (FreqOfPlace.ContainsKey(tup2))
            FreqOfPlace[tup2]++;
        else
            FreqOfPlace.Add(tup2, 1);

        elf.MadeDecision = true;
        return true;
    }
}

public class Elf
{
    public static int Index = 0;
    public int PosX;
    public int PosY;
    public int MoveX;
    public int MoveY;
    public int MyIndex;
    public bool MadeDecision = false;
    public bool NotMoving = false;

    public Elf(int x, int y)
    {
        PosX = x; PosY = y;
        MyIndex = Index;
        Index++;
    }

    internal string MyIndexStr()
    {
        if (MyIndex < 10)
            return "0" + MyIndex + " ";
        else return "" + MyIndex + " ";
    }

    internal void Reset()
    {
        MadeDecision = false;
        NotMoving = false;
    }
}