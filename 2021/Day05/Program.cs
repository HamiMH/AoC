using System;
using System.Collections;
using System.Collections.Generic;

namespace Day05_01
{
    public class PolozkaFronty : IComparable<PolozkaFronty>, IEquatable<PolozkaFronty>
    {
        public int index;
        public string typAkce;
        public Line usecka;

        public override string ToString()
        {
            return "index: " + index + ", typAkce: " + typAkce + ", " + usecka.ToString();
        }
        public int CompareTo(PolozkaFronty pf)
        {

            if (pf.index > this.index)
                return -1;
            if (pf.index < this.index)
                return 1;

            if (this.typAkce == "add" & pf.typAkce == "rem")
                return -1;
            if (pf.typAkce == "add" & this.typAkce == "rem")
                return 1;

            return this.usecka.CompareTo(pf.usecka);
        }



        public bool Equals(PolozkaFronty pf)
        {
            if (this.CompareTo(pf) == 0)
                return true;
            else
                return false;
        }

        public PolozkaFronty(int _index, string _typAkce, Line _usecka)
        {
            this.index = _index;
            this.typAkce = _typAkce;
            this.usecka = _usecka;
        }
    }

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

    public class Line : IComparable<Line>, IEquatable<Line>
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public int vx;
        public int vy;

        static double eps = 0.000000001;
        public bool Equals(Line li)
        {
            if (this.CompareTo(li) == 0)
                return true;
            else
                return false;
        }

        public int CompareTo(Line li)
        {
            if (this.x1 < li.x1)
            {
                return -1;
            }
            if (this.x1 > li.x1)
            {
                return 1;
            }
            if (this.y1 < li.y1)
            {
                return -1;
            }
            if (this.y1 > li.y1)
            {
                return 1;
            }

            if (this.x2 < li.x2)
            {
                return -1;
            }
            if (this.x2 > li.x2)
            {
                return 1;
            }
            if (this.y2 < li.y2)
            {
                return -1;
            }
            if (this.y2 > li.y2)
            {
                return 1;
            }
            return 0;

        }
        public void Intersect(Line li)
        {
            if (x1 < li.x1)
            {
                Console.WriteLine("error");
            }

            if (x1 == li.x1 && y1 < li.y1)
            {
                Console.WriteLine("error");
            }

            // Console.WriteLine("{0}, {1}, {2}, {3}", li.x1, li.y1, li.x2, li.y2);
            int pocKroku = 0;

            int nx = 0;
            int ny = 0;

            int tmpX = li.x1;
            int tmpY = li.y1;
            if (li.vx > 0)
            {
                nx = 1;
                pocKroku = li.vx;
            }
            else if (li.vx < 0)
            {
                nx = -1;
                pocKroku = -li.vx;
            }
            if (li.vy > 0)
            {
                ny = 1;
                if (li.vy > pocKroku)
                    pocKroku = li.vy;
            }
            else if (li.vy < 0)
            {
                ny = -1;
                if (-li.vy > pocKroku)
                    pocKroku = -li.vy;
            }

            //if ((this.vx == li.vx) && this.vy == li.vy)


            if ((this.vx * nx > 0 || this.vx == nx) && (this.vy * ny > 0 || this.vy == ny))
            {

                //Console.WriteLine("li.x1: {0}, li.y1: {1}, this.x1: {2}, this.y1: {3}", li.x1, li.y1, this.x1, this.y1);
                // Console.WriteLine("nx: {0}, ny: {1}, pocKroku: {2}", nx, ny, pocKroku);

                int prusek = 0;
                for (int i = 0; i <= pocKroku; i++)
                {
                    tmpX = li.x1 + nx * i;
                    tmpY = li.y1 + ny * i;
                    if ((prusek == 0) && ((tmpX == this.x1 && tmpY == this.y1)))
                    {
                        prusek = 1;
                    }
                    if (prusek >= 1)
                    {
                        Point pnt = new Point(tmpX, tmpY);
                        Program.prusecikBody.Add(pnt);
                        prusek++;
                    }
                    if ((prusek > 2) && ((tmpX == this.x1 && tmpY == this.y1) || (tmpX == this.x2 && tmpY == this.y2)))
                    {
                        break;
                    }
                }
                /*nx = 0;
                ny = 0;
               pocKroku = 0;
                tmpX = this.x1;
                tmpY = this.y1;
               if (this.vx > 0)
               {
                   nx = 1;
                   pocKroku = this.vx;
               }
               else if (this.vx < 0)
               {
                   nx = -1;
                   pocKroku = -this.vx;
               }
               if (this.vy > 0)
               {
                   ny = 1;
                   if (this.vy > pocKroku)
                       pocKroku = this.vy;
               }
               else if (this.vy < 0)
               {
                   ny = -1;
                   if (-this.vy > pocKroku)
                       pocKroku = -this.vy;
               }

                prusek = 0;
               for (int i = 0; i <= pocKroku; i++)
               {
                   tmpX = this.x1 + nx * i;
                   tmpY = this.y1 + ny * i;
                   if ((prusek == 0)&&(tmpX == li.x1 && tmpY == li.y1)|| (tmpX == li.x2 && tmpY == li.y2))
                   {
                       prusek = 1;
                   }
                   if (prusek >= 1)
                   {
                       Point pnt = new Point(tmpX, tmpY);
                       Program.prusecikBody.Add(pnt);
                       prusek++;
                   }
                   if ((prusek>2)&&((tmpX == li.x1 && tmpY == li.y1) || (tmpX == li.x2 && tmpY == li.y2)))
                   {
                       break;
                   }
               }
                */
            }
            else
            {


                double dx1 = (double)this.x1;
                double dy1 = (double)this.y1;
                double dx2 = (double)li.x1;
                double dy2 = (double)li.y1;
                double da1 = (double)this.vx;
                double da2 = (double)li.vx;
                double db1 = (double)this.vy;
                double db2 = (double)li.vy;
                double t;
                double s;




                s = (db2 * dx1 - db2 * dx2 - da2 * dy1 + da2 * dy2) / (da2 * db1 - da1 * db2);
                t = (db1 * dx1 - db1 * dx2 - da1 * dy1 + da1 * dy2) / (da2 * db1 - da1 * db2);

                if (-eps < s & s < 1 + eps & -eps < t & t < 1 + eps)
                {

                    if (Math.Abs(Math.Round(dx1 + da1 * s) - (dx1 + da1 * s)) < 0.1)
                    {
                        //  Console.WriteLine("s {0}, t {1}, {2}, {3}", s, t, dx1 + da1 * s, dy1 + db1 * s);
                        Point pnt = new Point((int)Math.Round(dx1 + da1 * s), (int)Math.Round(dy1 + db1 * s));
                        Program.prusecikBody.Add(pnt);
                    }

                }
            }
        }

        public override string ToString()
        {
            return x1 + ", " + y1 + " -> " + x2 + ", " + y2;

        }
        public Line(int _x1, int _y1, int _x2, int _y2)
        {
            if (_x1 < _x2)
            {
                x1 = _x1;
                y1 = _y1;
                x2 = _x2;
                y2 = _y2;
                vx = x2 - x1;
                vy = y2 - y1;
            }
            else if (_x1 > _x2)
            {
                x1 = _x2;
                y1 = _y2;
                x2 = _x1;
                y2 = _y1;
                vx = x2 - x1;
                vy = y2 - y1;
            }
            else
            {
                if (_y1 <= _y2)
                {
                    x1 = _x1;
                    y1 = _y1;
                    x2 = _x2;
                    y2 = _y2;
                    vx = x2 - x1;
                    vy = y2 - y1;
                }
                else if (_y1 > _y2)
                {
                    x1 = _x2;
                    y1 = _y2;
                    x2 = _x1;
                    y2 = _y1;
                    vx = x2 - x1;
                    vy = y2 - y1;
                }
                else
                {
                    x1 = _x2;
                    y1 = _y2;
                    x2 = _x1;
                    y2 = _y1;
                    vx = x2 - x1;
                    vy = y2 - y1;
                }
            }
        }
    }
    public class Program
    {
        public static SortedSet<Point> prusecikBody;
        public static SortedSet<PolozkaFronty> prioFronta;
        public static SortedSet<Line> aktivniUsecky;
        static void StepOne()
        {
            List<string> inputCol = new List<string>();
            string lineIn1;
            while ((lineIn1 = Console.ReadLine()) != null)
            {
                if (lineIn1 == "")
                    break;
                inputCol.Add(lineIn1);
            }

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            string[] lineArr = new string[2];
            string[] lineArr2 = new string[2];
            string direct;
            int len;
            int x1;
            int y1;
            int x2;
            int y2;
            Point p1;
            Point p2;
            Line li;
            prusecikBody = new SortedSet<Point>();
            prioFronta = new SortedSet<PolozkaFronty>();
            aktivniUsecky = new SortedSet<Line>();
            foreach (string lineIn in inputCol)
            {


                lineArr = lineIn.Split("->");
                lineArr2 = lineArr[0].Trim().Split(",");
                x1 = Convert.ToInt32(lineArr2[0].Trim(), 10);
                y1 = Convert.ToInt32(lineArr2[1].Trim(), 10);

                lineArr2 = lineArr[1].Trim().Split(",");
                x2 = Convert.ToInt32(lineArr2[0].Trim(), 10);
                y2 = Convert.ToInt32(lineArr2[1].Trim(), 10);

                //if (x1 != x2 && y1 != y2)
                //   continue;
                //Console.WriteLine("{0}, {1}, {2}, {3}", x1,y1,x2,y2);
                li = new Line(x1, y1, x2, y2);
                PolozkaFronty pf1 = new PolozkaFronty(li.x1, "add", li);
                PolozkaFronty pf2 = new PolozkaFronty(li.x2, "rem", li);
                //Console.WriteLine("pf1: {0}", pf1.ToString());
                //Console.WriteLine("pf2: {0}", pf2.ToString());
                prioFronta.Add(pf1);
                prioFronta.Add(pf2);
            }

       

            PolozkaFronty pf;
            /* foreach (PolozkaFronty ppf in prioFronta)
             {
                 Console.WriteLine("fe: {0}", ppf.ToString());
             }
            */
            while (prioFronta.Count > 0)
            {
                pf = prioFronta.Min;

                if (pf.typAkce == "add")
                {
                    porovnejSUseckami(pf.usecka);
                    aktivniUsecky.Add(pf.usecka);
                }
                else if (pf.typAkce == "rem")
                {
                    aktivniUsecky.Remove(pf.usecka);
                }

                prioFronta.Remove(pf);
            }
            /*foreach (Point pt in prusecikBody)
            {
                Console.WriteLine("ft: {0}", pt.ToString());
            }*/
            Console.WriteLine("{0}", prusecikBody.Count);
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }

        /*  for (int j = 0; j < LineList.Count; j++)
          {
              Console.WriteLine("{0}", LineList[j].ToString());
          }
          Console.WriteLine("");*/

        //Console.WriteLine("Multiply of x = {0} and y = {1}: {2}", x, y, x * y);


        private static void porovnejSUseckami(Line usecka)
        {
            foreach (Line aktUsecka in aktivniUsecky)
            {
                usecka.Intersect(aktUsecka);
            }
        }

        static void Main(string[] args)
        {
            StepOne();
        }
    }
}
