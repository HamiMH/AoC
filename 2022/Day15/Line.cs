public class Line
{
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;


    public Line(int xMin, int xMax, int yMin, int yMax)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;
    }

    public static Line IntersectedWithSensor(Sensor sensor)
    {


        return new Line (0,0,0,0);
    }

    internal static Line? LineIntersect(Line line, Line lineTmp)
    {
        throw new NotImplementedException();
    }
}