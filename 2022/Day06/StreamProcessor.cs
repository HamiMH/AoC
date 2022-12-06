internal class StreamProcessor
{

    public StreamProcessor()
    {
    }

    internal int Process(string str)
    {
        int len = str.Length;
        int i, j, k;
        bool noDouble;
        for (i = 3; i < len; i++)
        {
            noDouble = true;
            for (j = i; j >= i - 2; j--)
            {
                for (k = j - 1; k >= i - 3; k--)
                {
                    if (str[k] == str[j])
                        noDouble = false;
                }
            }
            if (noDouble)
                return i + 1;
        }
        throw new NotImplementedException();
    }

    internal int Process2(string str)
    {
        int len = str.Length;
        int i, j, k;
        bool noDouble;
        for (i = 13; i < len; i++)
        {
            noDouble = true;
            for (j = i; j >= i - 12; j--)
            {
                for (k = j - 1; k >= i - 13; k--)
                {
                    if (str[k] == str[j])
                        noDouble = false;
                }
            }
            if (noDouble)
                return i + 1;
        }
        throw new NotImplementedException();
    }


}