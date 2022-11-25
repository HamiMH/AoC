using System;
using System.Collections;
using System.Collections.Generic;

namespace Day09
{

    class Point : IComparable<Point>, IEquatable<Point>
    {
        int x;
        int y;

        public int CompareTo(Point pt)
        {
            if (this.x > pt.x)
                return 1;
            if (this.x < pt.x)
                return -1;
            if (this.y > pt.y)
                return 1;
            if (this.y < pt.y)
                return -1;
            return 0;
        }




        public bool Equals(Point pt)
        {
            int comp = this.CompareTo(pt);
            if (comp == 0)
                return true;
            else
                return false;
        }

        public Point(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }
    class Program
    {
        static List<List<int>> listOfLists = new List<List<int>>();
        static List<int> listOblasti = new List<int>();
        static SortedSet<Point> memoBody;
        static int lenX = 0;
        static int lenY = 0;
        static int sum = 0;
        static void Main(string[] args)
        {
            string line;

            int i = 0;
            int j;

            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                    break;
                lenX = line.Length;
                listOfLists.Add(new List<int>());
                for (j = 0; j < lenX; j++)
                {
                    listOfLists[i].Add(int.Parse(line[j].ToString()));
                }
                i++;
            }
            lenY = i;
            for (i = 0; i < lenY; i++)
            {
                for (j = 0; j < lenX; j++)
                {
                    poziceJeMinim(i, j);
                }
            }
            foreach (int index in listOblasti)
            {
                Console.Write("{0} ", index);
            }
            listOblasti.Sort();
            int multip = 1;
            for (i = listOblasti.Count - 3; i < listOblasti.Count; i++)
            {
                multip *= listOblasti[i];
            }
            Console.WriteLine("");
            Console.WriteLine("{0}", multip);
        }

        private static void poziceJeMinim(int i, int j)
        {
            if (i > 0 && listOfLists[i - 1][j] <= listOfLists[i][j])
            {
                return;
            }
            if (j > 0 && listOfLists[i][j - 1] <= listOfLists[i][j])
            {
                return;
            }
            if (i < (lenY - 1) && listOfLists[i + 1][j] <= listOfLists[i][j])
            {
                return;
            }
            if (j < (lenX - 1) && listOfLists[i][j + 1] <= listOfLists[i][j])
            {
                return;
            }
            sum++;
            sum += listOfLists[i][j];
            memoBody = new SortedSet<Point>();
            prohledejZ(i, j);
            listOblasti.Add(memoBody.Count);
        }

        private static void prohledejZ(int i, int j)
        {
            Point pt = new Point(i, j);
            if (memoBody.Contains(pt))
                return;
            memoBody.Add(pt);

            if (i > 0 && listOfLists[i - 1][j] > listOfLists[i][j] && listOfLists[i - 1][j] < 9)
            {
                prohledejZ(i - 1, j);
            }
            if (j > 0 && listOfLists[i][j - 1] > listOfLists[i][j] && listOfLists[i][j - 1] < 9)
            {
                prohledejZ(i, j - 1);
            }
            if (i < (lenY - 1) && listOfLists[i + 1][j] > listOfLists[i][j] && listOfLists[i + 1][j] < 9)
            {
                prohledejZ(i + 1, j);
            }
            if (j < (lenX - 1) && listOfLists[i][j + 1] > listOfLists[i][j] && listOfLists[i][j + 1] < 9)
            {
                prohledejZ(i, j + 1);
            }
        }
    }
}
