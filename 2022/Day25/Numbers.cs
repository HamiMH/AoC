using System.Text;

internal class Numbers
{
    public const long MAXLEN = 30;
    public long[] Pows=new long[MAXLEN];
    public long[] MaxNumOfLen=new long[MAXLEN];

    List<long[]> Numbs= new List<long[]>(); 
    public Numbers(List<string> inputCol)
    {
        int i, j,strLen;
        long[] numb;
        Pows[0] = 1;
        MaxNumOfLen[0] = 2;
        for (i=1;i< MAXLEN; i++)
        {
            Pows[i] = Pows[i-1]*5;
            MaxNumOfLen[i]=2* Pows[i] + MaxNumOfLen[i-1];
        }

        foreach(string str in inputCol)
        {
            strLen=str.Length;
             numb= new long[strLen];
            for (i = 0; i < strLen; i++)
            {
                j = strLen - 1 - i;

                switch (str[i])
                {
                    case '1':
                        numb[j] = 1;
                        break;
                    case '2':
                        numb[j] = 2;
                        break;
                    case '0':
                        numb[j] = 0;
                        break;
                    case '-':
                        numb[j] = -1;
                        break;
                    case '=':
                        numb[j] = -2;
                        break;
                }
            }
            Numbs.Add(numb);
        }

    }

    public long FiveToNormal(long[] number5)
    {
        long number10 = 0;
        long lenOfNum5 = number5.Length;
        for (int i = 0; i < lenOfNum5; i++)
            number10 += number5[i] * Pows[i];

        return number10;
    }

    

    public long[] NormalToFive(long number10)
    {
        List<long>number5= new List<long>();
        long remaining = number10;
        long tmp;
        long sign;
        long targetMax;
        bool isActive = false;
        for (long i = MAXLEN - 1; i > 0; i--)
        {
            targetMax = MaxNumOfLen[i - 1];
            if (Math.Abs( remaining) > targetMax)
            {
                isActive = true;
                sign = Math.Sign(remaining);

                tmp = sign;
                while (true)
                {
                    if (Math.Abs(remaining - tmp * Pows[i]) <= targetMax)
                        break;
                    tmp+=sign;
                }

                number5.Add(tmp);
                remaining-= tmp * Pows[i];
            }else if (isActive)
            {
                number5.Add(0);

            }
        }
        number5.Add(remaining);
        return number5.ToArray();
    }

    public string PrintNumber5(long[] number5)
    {
        long lenOfNum5 = number5.Length;
        StringBuilder stb=new StringBuilder();
        for (int i = 0; i < lenOfNum5; i++)
        {
            switch (number5[i])
            {
                case 1:
                    stb.Append('1');
                    break;
                case 2:
                    stb.Append('2');
                    break;
                case 0:
                    stb.Append('0');
                    break;
                case -1:
                    stb.Append('-');
                    break;
                case -2:
                    stb.Append('=');
                    break;
                default:
                    throw new Exception();

            }
        }
        return stb.ToString();  
    }

    public long GetWHoleSum()
    {
        long sum = 0;
        long tmp;
        foreach (long[] number5 in Numbs)
        {
            tmp = FiveToNormal(number5); ;
            sum += tmp;
            //Console.WriteLine( PrintNumber5(NormalToFive(tmp)));
        }
        return sum;

    }

    internal string Sum5()
    {
        return PrintNumber5(NormalToFive(GetWHoleSum()));
    }
}