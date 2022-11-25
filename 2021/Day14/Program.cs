using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {            
            Dictionary<string, long> poctyOld = new Dictionary<string, long>();
            Dictionary<string, long> poctyNew;
            Dictionary<string, string[]> template = new Dictionary<string, string[]>();
            string lineIn;
            string inStr0;
            string inStr1;
            string [] strArrToSave;
            string[] lineArr;
            int i;
            int len;
            string inputStr= Console.ReadLine();
            Console.ReadLine();
            len = inputStr.Length;
            for (i = 0; i < len - 1; i++)
            {
                string strPrac = inputStr.Substring(i, 2);
                if (! poctyOld.ContainsKey(strPrac))
                {
                    poctyOld.Add(strPrac, 1);
                }
                else
                {
                    poctyOld[strPrac]++;
                }
            }

            while ((lineIn = Console.ReadLine()) != null)
            {
                if (lineIn == "")
                    break;
                lineArr = lineIn.Split("->");
                inStr0 = lineArr[0].Trim();
                inStr1 = lineArr[1].Trim();
                strArrToSave = new string[2];
                strArrToSave[0] = char.ToString(inStr0[0]) + inStr1;
                strArrToSave[1] =  inStr1+ char.ToString(inStr0[1]);
                template.Add(inStr0, strArrToSave);
            }
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            string entKey;
            long entVal;
            string []newKeys;
            for (i = 0; i < 40; i++)
            {
                poctyNew = new Dictionary<string, long>();
                foreach (KeyValuePair<string, long> entry in poctyOld)
                {
                    entKey = entry.Key;
                    entVal = entry.Value;
                    newKeys = template[entKey];
                    if (!poctyNew.ContainsKey(newKeys[0]))
                    {
                        poctyNew.Add(newKeys[0], entVal);
                    }
                    else
                    {
                        poctyNew[newKeys[0]] += entVal;
                    }
                    if (!poctyNew.ContainsKey(newKeys[1]))
                    {
                        poctyNew.Add(newKeys[1], entVal);
                    }
                    else
                    {
                        poctyNew[newKeys[1]] += entVal;
                    }
                }
                poctyOld = poctyNew;
            }
            long[] memo = new long[26];
            foreach (KeyValuePair<string, long> entry in poctyOld)
            {
                entKey = entry.Key;
                entVal = entry.Value;
                memo[entKey[0] - 'A'] += entVal;
            }
            memo[inputStr[len-1] - 'A']++;
            long minVal = long.MaxValue;
            long maxVal = 0;
            for (i = 0; i < 26; i++)
            {
                if (memo[i] == 0)
                    continue;
                if (memo[i] > maxVal)
                    maxVal = memo[i];
                if (memo[i] < minVal)
                    minVal = memo[i];
            }

            stopwatch.Stop();
            Console.WriteLine("maxVal = {0}, minVal = {1}, diff = {2}", maxVal,minVal, maxVal-minVal);
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
