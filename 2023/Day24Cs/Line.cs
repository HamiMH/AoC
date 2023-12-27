using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day24Cs
{
    internal class Line
    {
        const decimal EPS = .0000001M;

        internal static decimal diffs = 0;

        internal Tuple<decimal, decimal, decimal> X;
        internal Tuple<decimal, decimal, decimal> Vec;

        public Line() { }
        public Line(string str)
        {
            string[] sides = str.Split("@");

            string[] beg = sides[0].Split(",");
            string[] end = sides[1].Split(",");

            //X = { stoll(beg[0]) ,stoll(beg[1]),stoll(beg[2]) };
            // Vec = { stoll(end[0]) ,stoll(end[1]),stoll(end[2]) };

            X = Tuple.Create(decimal.Parse(beg[0]), decimal.Parse(beg[1]), decimal.Parse(beg[2]));
            Vec = Tuple.Create(decimal.Parse(end[0]), decimal.Parse(end[1]), decimal.Parse(end[2]));
        }

        public decimal MyAbs(decimal inp)
        {
            if (inp < 0)
                return -inp;

            else
                return inp;
        }



        public bool IntersectLine2D(Line inLine, decimal MinP, decimal MaxP)
        {
            /*const auto[px1, py1, pz1] = X;
            const auto[u1, v1, w1] = Vec;
            const auto[px2, py2, pz2] = inLine.X;
            const auto[u2, v2, w2] = inLine.Vec;*/

            decimal x1 = X.Item1;
            decimal x2 = inLine.X.Item1;
            decimal y1 = X.Item2;
            decimal y2 = inLine.X.Item2;
            /*
            decimal x1 = X.Item1 - MinP;
             decimal x2 = inLine.X.Item2 - MinP;
             decimal y1 = X.Item2 - MinP;
             decimal y2 = inLine.X.Item2 - MinP;
            */
            decimal u1 = Vec.Item1;
            decimal u2 = inLine.Vec.Item1;
            decimal v1 = Vec.Item2;
            decimal v2 = inLine.Vec.Item2;



            decimal t1;
            decimal t2;
            //lines are paralel
            if (Math.Abs(u1 * v2 - v1 * u2) < EPS)
            {
                //if (abs((x1 - x2) / u2 - (y1 - y2) / v2) < EPS)
                if (Math.Abs((x1 - x2) * v2 - (y1 - y2) * u2) < EPS)
                {
                    if ((x1 - x2) / u2 > 0.0M)
                    {
                        t1 = 0.0M;
                        t2 = (x1 - x2) / u2;
                    }
                    else if ((x2 - x1) / u1 > 0.0M)
                    {
                        t1 = (x2 - x1) / u1;
                        t2 = 0.0M;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                t2 = ((y1 - y2) + (x2 - x1) * v1 / u1) / (v2 - v1 * u2 / u1);
                t1 = (x2 - x1 + t2 * u2) / u1;
            }

            decimal finX1 = x1 + u1 * t1;
            decimal finY1 = y1 + v1 * t1;
            decimal finX2 = x2 + u2 * t2;
            decimal finY2 = y2 + v2 * t2;

            decimal diffX = finX2 - finX1;
            decimal diffY = finY2 - finY1;
            diffs += Math.Abs(diffX);
            diffs += Math.Abs(diffY);
            if (Math.Abs(diffX) > .1M)
            { diffs += Math.Abs(diffY); }
            if (Math.Abs(diffY) > .1M)
            { diffs += Math.Abs(diffX); }



            //if (t1 < -EPS || t2 < -EPS)
            if (t1 < EPS || t2 < EPS)
                return false;  
            if (t1 >EPS && t2 > EPS)
            {

            }
            else
            {
                return false;

            }

            //if (finX2 > (MinP - EPS) && finX2 < (MaxP + EPS) && finY2 > (MinP - EPS) && finX2 < (MaxP + EPS))
            if (finX2 >= (MinP) && finX2 <= (MaxP ) && finY2 >= (MinP) && finY2 <= (MaxP ))
                return true;
            else
                return false;

        }
    };

}
