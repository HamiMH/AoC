using System;

namespace Day08
{
    class Program
    {
        static int[] memoCetnost;
        static int[] map = new int[7];
        static string strLen2 = "";
        static string strLen4 = "";
        static void Main(string[] args)
        {
            string line;
            string[] arrLine;
            string[] seznamCisel;
            string[] seznamVstupu;

            int i = 0;
            int j;
            int outSum = 0;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                    break;
                memoCetnost = new int[7];
                Array.Fill(map, -1);
                arrLine = line.Split("|");
                seznamCisel = (arrLine[0].Trim()).Split(" ");
                for (i = 0; i < seznamCisel.Length; i++)
                {
                    if (seznamCisel[i].Length == 2)
                        strLen2 = seznamCisel[i];
                    if (seznamCisel[i].Length == 4)
                        strLen4 = seznamCisel[i];
                    for (j = 0; j < seznamCisel[i].Length; j++)
                    {
                        memoCetnost[seznamCisel[i][j] - 'a']++;
                    }
                }
                decode();

                seznamVstupu = (arrLine[1].Trim()).Split(" ");

                int outNum = 0;
                for (i = 0; i < seznamVstupu.Length; i++)
                {
                    int outBinSchemeNumber = 1 << 7;
                    int outNumber = 0;
                    for (j = 0; j < seznamVstupu[i].Length; j++)
                    {
                        char a = seznamVstupu[i][j];
                        int b = map[seznamVstupu[i][j] - 'a'];
                        outBinSchemeNumber |= 1 << b;
                        //Console.Write("{0} ",map[seznamVstupu[i][j]-'a']); 
                    }
                    outNumber = schemeToNumber(outBinSchemeNumber);
                    // Console.WriteLine("outNumber");
                    outNum *= 10;
                    outNum += outNumber;
                }
                outSum += outNum;
            }

            Console.WriteLine("");
            Console.WriteLine("{0}", outSum);
        }

        private static int schemeToNumber(int outBinSchemeNumber)
        {
            if (outBinSchemeNumber == 0b11110111)
                return 0;
            if (outBinSchemeNumber == 0b10100100)
                return 1;
            if (outBinSchemeNumber == 0b11011101)
                return 2;
            if (outBinSchemeNumber == 0b11101101)
                return 3;
            if (outBinSchemeNumber == 0b10101110)
                return 4;
            if (outBinSchemeNumber == 0b11101011)
                return 5;
            if (outBinSchemeNumber == 0b11111011)
                return 6;
            if (outBinSchemeNumber == 0b10100101)
                return 7;
            if (outBinSchemeNumber == 0b11111111)
                return 8;
            if (outBinSchemeNumber == 0b11101111)
                return 9;


            throw new Exception();
        }

        private static void decode()
        {
            int i, j;
            for (i = 0; i < 7; i++)
            {
                if (memoCetnost[i] == 6)
                {
                    map[i] = 1;
                }
                else if (memoCetnost[i] == 4)
                {
                    map[i] = 4;
                }
                else if (memoCetnost[i] == 9)
                {
                    map[i] = 5;
                }
            }
            for (j = 0; j < strLen2.Length; j++)
            {
                if (map[strLen2[j] - 'a'] == -1)
                {
                    map[strLen2[j] - 'a'] = 2;
                }
            }

            for (j = 0; j < strLen4.Length; j++)
            {
                if (map[strLen4[j] - 'a'] == -1)
                {
                    map[strLen4[j] - 'a'] = 3;
                }
            }
            for (i = 0; i < 7; i++)
            {
                if (memoCetnost[i] == 8 && map[i] == -1)
                {
                    map[i] = 0;
                }
                if (memoCetnost[i] == 7 && map[i] == -1)
                {
                    map[i] = 6;
                }
            }
        }
    }
}
