using Day15;

public class Sensor
{
    public Tuple<int, int> SenC;
    public Tuple<int, int> BeaC;
    public int Dia;




    public Sensor(string str, BeaconMap beaconMap)
    {
        int x, y;
        str = str.Replace("Sensor at ", "").Trim();
        string[] strArr = str.Split(": closest beacon is at ");
        string[] strArr2 = strArr[0].Split(", ");
        x = int.Parse(strArr2[0].Trim().Substring(2));
        y = int.Parse(strArr2[1].Trim().Substring(2));
        beaconMap._xMin = Math.Min(beaconMap._xMin, x);
        beaconMap._xMax = Math.Max(beaconMap._xMax, x);
        beaconMap._yMin = Math.Min(beaconMap._yMin, y);
        beaconMap._yMax = Math.Max(beaconMap._yMax, y);
        SenC = new Tuple<int, int>(x,y );
        strArr2 = strArr[1].Split(", ");
        x = int.Parse(strArr2[0].Trim().Substring(2));
        y = int.Parse(strArr2[1].Trim().Substring(2));
        beaconMap._xMin = Math.Min(beaconMap._xMin, x);
        beaconMap._xMax = Math.Max(beaconMap._xMax, x);
        beaconMap._yMin = Math.Min(beaconMap._yMin, y);
        beaconMap._yMax = Math.Max(beaconMap._yMax, y);
        BeaC = new Tuple<int, int>(int.Parse(strArr2[0].Trim().Substring(2)), int.Parse(strArr2[1].Trim().Substring(2)));
        Dia = ManhDist(SenC, BeaC);
    }

    public bool IsInRange(int x,int y)
    {
        int distFromSen = Abs(x - SenC.Item1)+ Abs(y - SenC.Item2);
        if(distFromSen>Dia)
            return false;
        else
            return true;
    }

    public  Segment? GetSegmentOfLine(int nOfLine)
    {
        Segment? line = null;
        int distFromSen = Abs(nOfLine - SenC.Item2);
        int distFromEdge=Dia-distFromSen;
        
        if (distFromEdge<0)
            return line;
        line = new Segment(SenC.Item1 - distFromEdge, SenC.Item1 + distFromEdge);

        return line;
    }

    public List<Tuple<int,int>> GetOutlinePositions()
    {
        List<Tuple<int,int>> outlinePositions = new List<Tuple<int,int>>();

        int rangeOfOutline = Dia + 1;
        int x=SenC.Item1;
        int y=SenC.Item2;

        int i = -rangeOfOutline;
        int j = 0;

        for (; i < 0; i++, j++)
        {
            outlinePositions.Add(new Tuple<int, int>(x+i, y+j));
        }
        for (; i < rangeOfOutline; i++, j--)
        {
            outlinePositions.Add(new Tuple<int, int>(x + i, y + j));
        }
        for (; i < 0; i--, j--)
        {
            outlinePositions.Add(new Tuple<int, int>(x + i, y + j));
        }
        for (; i < -rangeOfOutline; i--, j++)
        {
            outlinePositions.Add(new Tuple<int, int>(x + i, y + j));
        }



        return outlinePositions;
    }

    //public void AddOutlineLines(List<Line> listLine)
    //{
    //    int rangeOfOutline = Dia + 1;
    //    int x = SenC.Item1;
    //    int y = SenC.Item2;
    //    listLine.Add(new Line(x-rangeOfOutline, y, x,y+rangeOfOutline));
    //    listLine.Add(new Line( x,y+rangeOfOutline, x + rangeOfOutline, y));
    //    listLine.Add(new Line(x + rangeOfOutline, y, x, y - rangeOfOutline));
    //    listLine.Add(new Line(x, y - rangeOfOutline, x - rangeOfOutline, y));

    //}


    private int Abs(int a)  {if (a >= 0) return a; else return -a; }
    private int ManhDist(Tuple<int, int> a, Tuple<int, int> b)  { return Abs(a.Item1 - b.Item1) + Abs(a.Item2 - b.Item2); }
}