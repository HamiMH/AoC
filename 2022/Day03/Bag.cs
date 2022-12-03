internal class Bag
{

    public int[] Arr = new int[53];
    public Bag()
    {
    }

    private static long ValOfItem(char c)
    {
        if(c>='a' && c<='z')
            return (long)(c-'a'+1);
        else
            return (long)(c - 'A' + 27);
    }

    internal long GetPrio()
    {
        //IEnumerable<char> ienumC =_firstHalf.Intersect(_secondHalf);
        for(int i = 0;i<53;i++)
            if (Arr[i] == 3)
                return i;

        throw new Exception("GetPrio");
    }

    internal void SetContent(string col)
    {
        Array.Fill(Arr, 0);

        int lenOfBag=col.Length/2;
        int len=col.Length;
        char c;
        for(int i = 0; i < len; i++)
        {
            if (i < lenOfBag)
                Arr[ValOfItem(col[i])] |= 1;
            else
                Arr[ValOfItem(col[i])] |= 2;
        }

    }

    internal static long GetBadgePrio(Bag[] bags)
    {
        int len = bags.Length;
        int i, j;
        bool allIn;
        for(i = 0; i < 53; i++)
        {
            allIn = true;
            for (j = 0; j < len; j++)
            {
                if (bags[j].Arr[i]==0)
                    allIn = false;
            }
            if (allIn)
                return i;
        }
        throw new NotImplementedException();
    }
}