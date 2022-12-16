using Day15;

public class BeaconMap
{
    private HashSet<string> Counted = new HashSet<string>();
    public List<Sensor> Sensors;

    public int _xMin = int.MaxValue;
    public int _xMax = int.MinValue;
    public int _yMin = int.MaxValue;
    public int _yMax = int.MinValue;

    public BeaconMap(List<string> inputCol)
    {
        Sensors = new List<Sensor>();
        foreach (string str in inputCol)
            Sensors.Add(new Sensor(str, this));
    }

    internal int GetEmpty(int nOfLine)
    {
        SuperSegment superLine = new SuperSegment(_xMin - (_xMax - _xMin), _xMax + (_xMax - _xMin));
        int slSizeBefore = superLine.Size();
        foreach (Sensor sensor in Sensors)
        {
            superLine.AddSubline(sensor.GetSegmentOfLine(nOfLine));
        }
        int slSizeAfter = superLine.Size();
        int res = slSizeBefore - slSizeAfter;

        foreach (Sensor sensor in Sensors)
        {
            if (sensor.BeaC.Item2 == nOfLine)
            {
                if (Counted.Contains(sensor.BeaC.Item1 + " " + sensor.BeaC.Item2) == false)
                {
                    Counted.Add(sensor.BeaC.Item1 + " " + sensor.BeaC.Item2);
                    res--;
                }

            }
            if (sensor.SenC.Item2 == nOfLine)
            {
                if (Counted.Contains(sensor.SenC.Item1 + " " + sensor.SenC.Item2) == false)
                {
                    Counted.Add(sensor.SenC.Item1 + " " + sensor.SenC.Item2);
                    res--;
                }

            }
        }
        return res;
    }

    public long GetValOfFree(long boundary)
    {
        int lenOfSensors = Sensors.Count;
        int i, j;
        bool outOfRanges = true;
        for (i = 0; i < lenOfSensors; i++)
        {
            List<Tuple<int, int>> listOfOutline = Sensors[i].GetOutlinePositions();
            foreach (Tuple<int, int> tuple in listOfOutline)
            {
                if(TupleOutOfSpace(tuple, boundary))
                    continue;
                outOfRanges = true;
                for (j = 0; j < lenOfSensors; j++)
                {
                    if (i == j) continue;
                    if (Sensors[j].IsInRange(tuple.Item1, tuple.Item2))
                    {
                        outOfRanges = false;
                        break;
                    }
                }
                if (outOfRanges)
                {
                    return ((long)tuple.Item1) * boundary + tuple.Item2;
                }

            }
        }
        return -1;
    }

    private bool TupleOutOfSpace(Tuple<int, int> tuple,long boundary)
    {
        if(tuple.Item1 < 0 || tuple.Item1 > boundary || tuple.Item2 < 0 || tuple.Item2 > boundary) //4000000
            return true;
        else
            return false;
    }
}