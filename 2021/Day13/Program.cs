using System;
using System.Collections;
using System.Collections.Generic;

namespace Day13
{

    public class Point : IComparable<Point>, IEquatable<Point>
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return "(" + x + "," + y + ")";
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
        public static SortedSet<Point> bodyOrig;
        static void Main(string[] args)
        {
            string lineIn;
            string[] lineArr = new string[2];
            bodyOrig = new SortedSet<Point>();

            while ((lineIn = Console.ReadLine()) != null)
            {
                if (lineIn == "")
                    break;
                lineArr = lineIn.Split(",");
                bodyOrig.Add(new Point(int.Parse(lineArr[0]), int.Parse(lineArr[1])));
            }
            SortedSet<Point> bodyNew= new SortedSet<Point>(); ;
            while ((lineIn = Console.ReadLine()) != null)
            {
                if (lineIn == "")
                    break;
                lineArr = lineIn.Split(" ");
                lineArr = lineArr[2].Split("=");
                bodyNew = new SortedSet<Point>(); 
                foldToBy(bodyNew, lineArr[0], int.Parse(lineArr[1]));
                bodyOrig = bodyNew;

            }
            int[,] memo = new int[10, 60];
            foreach(Point pnt in bodyOrig)
            {
                memo[pnt.y, pnt.x] = 1;
            }
            for(int i =0; i< 10; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    if(memo[i, j] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                    
                }
                Console.WriteLine("");
            }
            
            Console.WriteLine("count = {0}.",bodyNew.Count);
        }

        private static void foldToBy(SortedSet<Point> bodyNew, string ax, int val)
        {
            foreach(Point pnt in bodyOrig)
            {
                if (ax == "x")
                {
                    if(pnt.x>val)
                        bodyNew.Add(new Point(2 * val - pnt.x, pnt.y));
                    else
                        bodyNew.Add(new Point(pnt.x, pnt.y));

                }
                else
                {
                    if (pnt.y > val)
                        bodyNew.Add(new Point(pnt.x,2 * val - pnt.y));
                    else
                        bodyNew.Add(new Point(pnt.x, pnt.y));
                }
            }


        }
    }
}
