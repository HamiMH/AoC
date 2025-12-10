namespace Day09cs
{
    internal class Line
    {
        public long X1 { get; set; }
        public long Y1 { get; set; }
        public long X2 { get; set; }
        public long Y2 { get; set; }
        public bool IsVertical()
        {
            return X1 == X2;
        }
        public Line(long x1, long y1, long x2, long y2)
        {
            X1 = Math.Min(x1,x2);
            Y1 = Math.Min(y1, y2);
            X2 = Math.Max(x1, x2);
            Y2 = Math.Max(y1, y2);
        }

        public bool IntersectWith(Line other)
        {
            if(IsVertical()==other.IsVertical())
            {
                return false;
            }


            if(IsVertical())
            {
                if(other.X1<X1 && other.X2>X1 && Y1<other.Y1 && Y2>other.Y1)
                {
                    return true;
                }               
            }
            else
            {
                if(X1<other.X1 && X2>other.X1 && other.Y1<Y1 && other.Y2>Y1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}