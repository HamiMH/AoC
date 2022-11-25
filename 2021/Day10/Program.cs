using System;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {

        static List<long> compScore = new List<long>();
        static void Main(string[] args)
        {
            string lineIn;
            int sum = 0;

            while ((lineIn = Console.ReadLine()) != null)
            {
                if (lineIn == "")
                    break;

                sum += zpracuj(lineIn);
            }

            Console.WriteLine("sum:{0}",sum);
            compScore.Sort();
            Console.WriteLine("midscore:{0}", compScore[compScore.Count/2]);
        }

        private static char flip(char zavorka)
        {
            if (zavorka == '(')
                return ')';

            if (zavorka == ')')
                return '(';

            if (zavorka == '[')
                return ']';

            if (zavorka == ']')
                return '[';

            if (zavorka == '{')
                return '}';

            if (zavorka == '}')
                return '{';

            if (zavorka == '<')
                return '>';

            if (zavorka == '>')
                return '<';

            return '0';
        }

        private static int price(char zavorka)
        {
            if (zavorka == '(' || zavorka == ')')
                return 3;

            if (zavorka == '[' || zavorka == ']')
                return 57;

            if (zavorka == '{' || zavorka == '}')
                return 1197;


            if (zavorka == '<' || zavorka == '>')
                return 25137;

            return 0;
        }

        private static long priceScore(char zavorka)
        {
            if (zavorka == '(' || zavorka == ')')
                return 1;

            if (zavorka == '[' || zavorka == ']')
                return 2;

            if (zavorka == '{' || zavorka == '}')
                return 3;

            if (zavorka == '<' || zavorka == '>')
                return 4;

            return 0;
        }
        private static int zpracuj(string lineIn)
        {
            int i;
            int len;
            char zav;
            char zavOut;
            len = lineIn.Length;
            Stack<char> zavorky = new Stack<char>();
            for (i = 0; i < len; i++)
            {                
                zav = lineIn[i];
                if (zav == '(' || zav == '[' || zav == '{' || zav == '<')
                {
                    zavorky.Push(zav);
                }
                else
                {
                    if (zavorky.Count == 0)
                    {
                        return price(zav);
                    }
                    else
                    {
                        zavOut = zavorky.Pop();
                        if (zavOut != flip(zav))
                        {
                            return price(zav);
                        }
                    }
                }

            }
            compScore.Add(computeRest(zavorky));
            return 0;
        }

        private static long computeRest(Stack<char> zavorky)
        {
            long outScore = 0;
            while (zavorky.Count>0)
            {
                outScore *= 5;
                outScore += priceScore(zavorky.Pop());
            }
            return outScore;
        }
    }
}
