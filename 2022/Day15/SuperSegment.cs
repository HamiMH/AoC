using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class SuperSegment
    {
        public Segment line;
        public List<SuperSegment> Sublines;

        public SuperSegment(int from, int to)
        {
            line = new Segment(from, to);
            Sublines = new List<SuperSegment>();
        }
        public SuperSegment(Segment lineIn)
        {
            line = lineIn;
            Sublines = new List<SuperSegment>();
        }

        public void AddSubline(int from, int to)
        {
            Segment lineTmp = new Segment(from, to);
            Segment? inters = Segment.LineIntersect(line, lineTmp);
            if (inters == null)
                return;
            foreach (SuperSegment sp in Sublines)
                sp.AddSubline(inters.from, inters.to);
            Sublines.Add(new SuperSegment(inters));
        }

        public void AddSubline(Segment? l)
        {
            if (l != null)
                AddSubline(l.from, l.to);
        }

        public int Size()
        {
            int value = line.Size()+1;

            foreach (SuperSegment sl in Sublines)
            {
                value -= sl.Size();
            }

            return value;
        }


    }



    public class Segment
    {
        public int from;
        public int to;
        public Segment(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public int Size()
        {
            return to - from;
        }

        public static Segment? LineIntersect(Segment a, Segment b)
        {
            int from;
            int to;
            from = Math.Max(a.from, b.from);
            to = Math.Min(a.to, b.to);
            if (from <= to)
                return new Segment(from, to);
            else
                return null;
        }



    }
}
