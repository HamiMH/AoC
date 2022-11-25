using System;
using System.Collections.Generic;

namespace Day20
{

    public class Point : IComparable<Point>, IEquatable<Point>
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }

        public override int GetHashCode()
        {
            return 128*y+x; // Or something like that
        }
        public int CompareTo(Point p1)
        {

            if (p1.x > this.x)
                return 1;
            if (p1.x < this.x)
                return -1;

            if (p1.y > this.y)
                return 1;
            if (p1.y < this.y)
                return -1;

            return 0;
        }



        public bool Equals(Point p1)
        {
            if (this.CompareTo(p1) == 0)
                return true;
            else
                return false;
        }

        public Point(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
    }
    class Program
    {
        static HashSet<Point> sspL1;
        static HashSet<Point> sspL2;
        static HashSet<Point> sspB1;
        static HashSet<Point> sspB2;
        static string pravidla = "";
        static int iter = 0;
        static void Main(string[] args)
        {
            List<string> inputCol = new List<string>();
            string lineIn1;
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    break;
                pravidla=lineIn1;
            }
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    break;
                inputCol.Add(lineIn1);
            }
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            int i, j, inputLenY, inputLenX,x,y;

            inputLenY = inputCol.Count;
            inputLenX = inputCol[0].Length;
            sspL1 = new HashSet<Point>();
            sspB1 = new HashSet<Point>();
            for (i = 0; i < inputLenY; i++)
            {
                lineIn1 = inputCol[i];
                for (j = 0; j < inputLenX; j++)
                {
                    if (lineIn1[j] == '#')
                        sspL1.Add(new Point(j, i));
                    else
                        sspB1.Add(new Point(j, i));
                }
                   
            }

            // Console.WriteLine("count = {0}, prlen = {1}", ssp1.Count, pravidla.Length);
            for (iter = 0; iter < 50; iter++)
            {
                sspL2 = new HashSet<Point>();
                sspB2 = new HashSet<Point>();

                for (i = -1 - iter; i <= inputLenY + iter; i++)
                    for (j = -1 - iter; j <= inputLenX + iter; j++)
                        zpracuj(j, i);
                sspL1 = sspL2;
                sspB1 = sspB2;
            }

            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("count = {0}, prlen = {1}",sspL2.Count,pravidla.Length);
        }

        private static void zpracuj(int x, int y)
        {
            Point pt = new Point(x, y);
            int i, j;
            int result = 0;
            for (i = y - 1; i <= y + 1; i++)
                for (j = x - 1; j <= x + 1; j++)
                {
                    result <<= 1;
                    if (sspL1.Contains(new Point(j, i)))
                    {
                        result |= 1;
                    }
                    else if (sspB1.Contains(new Point(j, i)))
                    {
                    }
                    else
                    {
                       result |= (iter & 1);
                    }
                }

            if (pravidla[result] == '#')
                sspL2.Add(pt);
            else                                              
                sspB2.Add(pt);

        }
    }
}
