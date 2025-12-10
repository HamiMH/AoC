
namespace Day09cs
{
    internal class Polynom
    {
        public List<(long, long)> Points = new();
        public List<Line> Lines = new();

        public Polynom(List<(long, long)> corners)
        {
            corners.Reverse();
            for (int i = 0; i < corners.Count; i++)
            {
                (long x0, long y0) = corners[i];
                (long x1, long y1) = corners[(i + 1) % corners.Count];

                Points.Add((x0, y0));
                Lines.Add(new Line(x0, y0, x1, y1));
            }
        }

        public bool PointIsInside((long, long) point)
        {
            double sumAngles = 0;
            for(int i = 0;i< Points.Count; i++)
            {
                (long x0, long y0) = Points[i];
                (long x1, long y1) = Points[(i + 1) % Points.Count];

                if(Ccw(point, (x0,y0),  (x1,y1)))
                {
                    sumAngles += AngleBetween((x0,y0), point, (x1,y1));
                }
                else
                {
                    sumAngles -= AngleBetween((x0,y0), point, (x1,y1));
                }
            }
            return Math.Abs(Math.Abs(sumAngles)-2*Math.PI)<0.0001;
        }

        private double AngleBetween((long, long) p1, (long, long) o, (long, long) p2)
        {
            (double, double) p1o = (p1.Item1 - o.Item1, p1.Item2 - o.Item2);
            (double, double) p2o = (p2.Item1 - o.Item1, p2.Item2 - o.Item2);

            double sks= p1o.Item1 * p2o.Item1 + p1o.Item2 * p2o.Item2;
            double p1oSq = p1o.Item1 * p1o.Item1 + p1o.Item2 * p1o.Item2;
            double p2oSq = p2o.Item1 * p2o.Item1 + p2o.Item2 * p2o.Item2;

            return Math.Acos(sks/Math.Sqrt(p1oSq*p2oSq));
        }

        private bool Ccw((long, long) p1, (long, long) p2, (long, long) p3)
        {
            (long,long)p12= (p2.Item1 - p1.Item1, p2.Item2 - p1.Item2);
            (long,long)p13= (p3.Item1 - p1.Item1, p3.Item2 - p1.Item2);
            return Cross(p12,p13) > 0;
        }
        private long Cross((long, long) p1, (long, long) p2) 
        {
            return (p1.Item1 * p2.Item2) - (p1.Item2 * p2.Item1);
        }

        public bool IntersectWithAny(Line line)
        {
            foreach (var lineEnum in Lines)
            {
                if (line.IntersectWith(lineEnum))
                {
                    return true;
                }
            }
            return false;
        }

        public bool PointIsOnLine((long, long) point)
        {
            (long x, long y) = point;
            foreach (var line in Lines)
            {
                if(line.X1==x && line.Y1==y)
                {
                    return true;
                }
                if (line.IsVertical())
                {
                    if (line.X1 == x && y >= line.Y1 && y <= line.Y2)
                    {
                        return true;
                    }
                }
                else
                {
                    if (line.Y1 == y && x >= line.X1 && x <= line.X2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}