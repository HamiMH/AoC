internal class HeadTailSimulator
{

    private int[] PositionHead = { 0, 0 };
    private int[] PositionTail = { 0, 0 };



    private HashSet<string> AttendedPlaces = new HashSet<string>();

    private Dictionary<char, int[]> Directions = new Dictionary<char, int[]>() {
        { 'U', new int[] { 0, 1 } },
        { 'D', new int[] { 0, -1 } },
        { 'R', new int[] { 1, 0 } },
        { 'L', new int[] { -1, 0 } },
    };

    private char _direction;
    private int _sizeOfStep, i;
    private int[] _diff = { 0, 0 };
    internal void MakeMove(string str)
    {
        _direction = str[0];
        _sizeOfStep = int.Parse(str.Substring(2));

        for (i = 0; i < _sizeOfStep; i++)
        {
            MoveHead(_direction);
            MoveTail();
            //}
            AttendedPlaces.Add(PositionTail[0] + " " + PositionTail[1]);
        }
    }

    private void MoveTail()
    {
        SetDiff();
        if (_diff[0] == 0)
        {
            PositionTail[1] += _diff[1] / 2;
        }
        else if (_diff[1] == 0)
        {
            PositionTail[0] += _diff[0] / 2;
        }
        else if (_diff[0] == 2 || _diff[0] == -2)
        {
            PositionTail[0] += _diff[0] / 2;
            PositionTail[1] += _diff[1];
        }
        else if (_diff[1] == 2 || _diff[1] == -2)
        {
            PositionTail[0] += _diff[0];
            PositionTail[1] += _diff[1] / 2;
        }
    }

    private void SetDiff()
    {
        _diff[0] = PositionHead[0] - PositionTail[0];
        _diff[1] = PositionHead[1] - PositionTail[1];
    }

    private void MoveHead(char c)
    {
        PositionHead[0] += Directions[c][0];
        PositionHead[1] += Directions[c][1];
    }

    internal long NofAttended()
    {
        return AttendedPlaces.Count();
    }
}