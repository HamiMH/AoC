using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class SuperLine
    {
        public Line line;
        public List<SuperLine> Sublines;

        public SuperLine(int xMin, int xMax, int yMin, int yMax)
        {
            line = new Line(xMin, xMax, yMin, yMax);
            Sublines = new List<SuperLine>();
        }
        public SuperLine(Line lineIn)
        {
            line = lineIn;
            Sublines = new List<SuperLine>();
        }

        public void AddSubline(int xMin, int xMax, int yMin, int yMax)
        {
            Line lineTmp = new Line(xMin, xMax, yMin, yMax);
            Line? inters = Line.LineIntersect(line, lineTmp);
            if (inters == null)
                return;
            foreach (SuperLine sp in Sublines)
                sp.AddSubline(inters.xMin, inters.xMax, inters.yMin, inters.yMax);
            Sublines.Add(new SuperLine(inters));
        }

        public void AddSubline(Line? l)
        {
            if (l != null)
                AddSubline(l.xMin, l.xMax, l.yMin, l.yMax);
        }

        public int Size()
        {
            //int value = line.Size()+1;
            int value = 1;

            foreach (SuperLine sl in Sublines)
            {
                value -= sl.Size();
            }

            return value;
        }


    }



    //public class Line
    //{
    //    public int from;
    //    public int to;
    //    public Line(int from, int to)
    //    {
    //        this.from = from;
    //        this.to = to;
    //    }

    //    public int Size()
    //    {
    //        return to - from;
    //    }

    //    public static Line? LineIntersect(Line a, Line b)
    //    {
    //        int from;
    //        int to;
    //        from = Math.Max(a.from, b.from);
    //        to = Math.Min(a.to, b.to);
    //        if (from <= to)
    //            return new Line(from, to);
    //        else
    //            return null;
    //    }



    //}
}
