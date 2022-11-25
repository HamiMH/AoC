using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Day24
{

    class Program
    {
        static Dictionary<string, long> minims = new Dictionary<string, long>();
        static HashSet<string> used = new HashSet<string>();
        static SortedSet<string> zeros = new SortedSet<string>();
        private static long[] strToArr(string str)
        {
            int len = str.Length;
            long[] arr = new long[len];
            for (int i = 0; i < len; i++)
                arr[i] = str[i] - '0';
            return arr;
        }

        private static string arrToString(long[] arr)
        {

            int len = arr.Length;
            StringBuilder stb = new StringBuilder(len);

            for (int i = 0; i < len; i++)
                stb.Append(Convert.ToChar('0'+arr[i]));
            return stb.ToString(); ;
        }



        static void Main(string[] args)
        {
            //int[] test = { 2, 3, 5, 9, 6 };
            // Console.WriteLine(" {0}", arrToString(test));
            //max();
            //min();
            //generate();
            /*int stackSize = 1024 * 1024 * 64;
            Thread th = new Thread(() =>
            {*/
            //long[] intArr = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            long[] intArr = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            minimize(intArr, 0);
            long[] intArr1 = { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,9, 9, 9, 9 };
            minimize(intArr1, 0);
            //},
            //stackSize);

            //th.Start();
            //th.Join();

            Console.WriteLine("{0}", zeros.Min);
            Console.WriteLine("{0}", zeros.Max);
        }

        private static void minimize(long [] intArr,int depth)
        {
            if (depth > 1000)
                return;
            if (used.Contains(arrToString(intArr)))
                return;
            long ret = runAlu(intArr);
            used.Add(arrToString(intArr));
            if (ret == 0)
            {
                zeros.Add(arrToString(intArr));
               // Console.WriteLine("{0}", arrToString(intArr));
            }
            int len = intArr.Length;
            long retPrac;
            for (int i =0; i < len; i++)
            {
                if (intArr[i] > 1)
                {
                    intArr[i]--;
                    retPrac = runAlu(intArr);
                    if (retPrac <= ret)
                    {
                        minimize(intArr,depth+1);
                    }
                    intArr[i]++;
                }
                if (intArr[i] < 9)
                {
                    intArr[i]++;
                    retPrac = runAlu(intArr);
                    if (retPrac <= ret)
                    {
                        minimize(intArr, depth + 1);
                    }
                    intArr[i]--;
                }
            }
        }

        private static void max()
        {

            long[] intArr = { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            long ret = runAlu(intArr);
            Console.WriteLine("ret = {0}", ret);
            //runProgram();
            long tmp, tmpMax;
            while (true)
            {

                for (int i = 0; i < 14; i++)
                {
                    int index = 9;
                    for (int k = 0; k <= i; k++)
                    {
                        tmpMax = long.MaxValue;

                        for (int j = 9; j >= 1; j--)
                        {
                            intArr[k] = j;
                            tmp = runAlu(intArr);
                            if (tmp < tmpMax)
                            {
                                tmpMax = tmp;
                                index = j;
                            }
                        }
                        intArr[k] = index;
                    }



                    for (int ii = 0; ii < 14; ii++)
                    {
                        Console.Write("{0}", intArr[ii]);
                    }
                    Console.Write("  {0}", runAlu(intArr));
                    Console.WriteLine("");
                }

                for (int i = 0; i < 14; i++)
                {
                    Console.Write("{0}", intArr[i]);
                }
                Console.Write("  {0}", runAlu(intArr));
                Console.WriteLine("");
            }

        }

        

        private static void min()
        {
            long[] intArr = { 2, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            long ret = runAlu(intArr);
            Console.WriteLine("ret = {0}", ret);
            //runProgram();
            long tmp, tmpMax,price,lastPrice;
            lastPrice = -1;
            string inpStr = "";
            string lastInpStr = "";
            long minResult=long.MaxValue;
            while (true)
            {

                for (int i = 1; i < 14; i++)
                {
                    int index = 1;
                    for (int k = 1; k <= i; k++)
                    {
                        tmpMax = long.MaxValue;

                        for (int j = 1; j <= 9; j++)
                        {
                            intArr[k] = j;
                            tmp = runAlu(intArr);
                            if (tmp < tmpMax)
                            {
                                tmpMax = tmp;
                                index = j;
                            }
                        }
                        intArr[k] = index;
                    }



                   /* for (int ii = 0; ii < 14; ii++)
                    {
                        Console.Write("{0}", intArr[ii]);
                    }
                    Console.Write("  {0}", runAlu(intArr));
                    Console.WriteLine("");*/
                }
                inpStr = "";
                for (int i = 0; i < 14; i++)
                {
                    inpStr= inpStr+ intArr[i];
                }
                price = runAlu(intArr);
                /*Console.Write(inpStr+"  {0}", price);
                Console.WriteLine("");*/

                if (price == 0)
                {
                    long nowVal = Int64.Parse(inpStr);
                    if (minResult > nowVal)
                        minResult = nowVal;
                }

                if (inpStr.Equals(lastInpStr)){
                    for (int i = 1; i < 14; i++)
                    {
                        intArr[i] %= 9;
                         intArr[i]++;

                    }
                }
                lastInpStr = new string(inpStr);
                //lastPrice = price;
            }

        }

        private static void runProgram()
        {
            long input = 99999999999999;
            string prac;
            long ret;
            long[] intArr = new long[14];
            int i;
            long sub = 1000000000000;
            while (input > 0)
            {
                prac = input + "";
                if (prac.Contains("0"))
                {
                    input-=sub;
                    continue;
                }
                for (i = 0; i < 14; i++)
                {
                    intArr[i] = prac[i] - '0';
                }
                ret = runAlu(intArr);
                Console.WriteLine("input = {0}, ret = {1}", input,ret);
                if (ret == 0)
                    break;
                input -= sub;
            }
            Console.WriteLine("largest = {0}", input);
        }

        private static void generate()
        {
            List<string> inputCol = new List<string>();
            string lineIn1;
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    break;
                inputCol.Add(lineIn1);
            }

            createScript(inputCol);
        }

        private static void createScript(List<string> inputCol)
        {
            FileStream fileStream = File.Create(@"C:\Users\Hami\Documents\GitHub\AoC2021\AoC2021\Day24\code.txt");
            BufferedStream buffered = new BufferedStream(fileStream);
            StreamWriter writer = new StreamWriter(buffered);
            writer.WriteLine("long x=0;");
            writer.WriteLine("long y=0;");
            writer.WriteLine("long z=0;");
            writer.WriteLine("long w=0;");
            string[] strArr;
            int iter = 0;
            foreach(string str in inputCol)
            {
                strArr = str.Split(" ");
                if (strArr.Length == 2)
                {
                    writer.WriteLine(strArr[1] + " = inputNum["+iter+"];");
                    iter++;
                }
                else
                {
                    switch (strArr[0])
                    {
                        case "add":
                            writer.WriteLine(strArr[1] + " = (" + strArr[1] + ") + (" + strArr[2] + ") ;");
                            break;
                        case "mul":
                            writer.WriteLine(strArr[1] + " = (" + strArr[1] + ") * (" + strArr[2] + ") ;");
                            break;
                        case "div":
                            writer.WriteLine("if(" + strArr[2] + " == 0){");
                            writer.WriteLine("return -100000;");
                            writer.WriteLine("}");
                            writer.WriteLine(strArr[1] + " = (" + strArr[1] + ") / (" + strArr[2] + ") ;");
                            break;
                        case "mod":
                            writer.WriteLine("if(" + strArr[1] + " < 0){");
                            writer.WriteLine("return -100000;");
                            writer.WriteLine("}");
                            writer.WriteLine("if(" + strArr[2] + " <= 0){");
                            writer.WriteLine("return -100000;");
                            writer.WriteLine("}");
                            writer.WriteLine(strArr[1] + " = (" + strArr[1] + ") % (" + strArr[2] + ") ;");
                            break;
                        case "eql":
                            writer.WriteLine("if(" + strArr[1] + " == " + strArr[2] + "){");
                            writer.WriteLine("  "+strArr[1] +" = 1;");
                            writer.WriteLine("}else{");
                            writer.WriteLine("  " + strArr[1] + " = 0;");
                            writer.WriteLine("}");
                            break;
                    }
                }
            }
            writer.WriteLine("return z;");
            writer.Close();
            buffered.Close();
            fileStream.Close();
        }

        private static long runAlu(long [] inputNum)
        {
            long x = 0;
            long y = 0;
            long z = 0;
            long w = 0;
            w = inputNum[0];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (13);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (10);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[1];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (11);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (16);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[2];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (11);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (0);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[3];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (10);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (13);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[4];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-14);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (7);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[5];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-4);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (11);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[6];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (11);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (11);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[7];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-3);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (10);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[8];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (12);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (16);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[9];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-12);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (8);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[10];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (1 == 0)
            {
                return -100000;
            }
            z = (z) / (1);
            x = (x) + (13);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (15);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[11];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-12);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (2);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[12];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-15);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (5);
            y = (y) * (x);
            z = (z) + (y);
            w = inputNum[13];
            x = (x) * (0);
            x = (x) + (z);
            if (x < 0)
            {
                return -100000;
            }
            if (26 <= 0)
            {
                return -100000;
            }
            x = (x) % (26);
            if (26 == 0)
            {
                return -100000;
            }
            z = (z) / (26);
            x = (x) + (-12);
            if (x == w)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            y = (y) * (0);
            y = (y) + (25);
            y = (y) * (x);
            y = (y) + (1);
            z = (z) * (y);
            y = (y) * (0);
            y = (y) + (w);
            y = (y) + (10);
            y = (y) * (x);
            z = (z) + (y);
            return z;

        }
    }
}
