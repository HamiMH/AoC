internal class SnakeSimulator
{
    private const int LENOFSNAKE = 10;


    private int[,] PositionSnake = new int[LENOFSNAKE, 2];

    private HashSet<string> AttendedPlaces = new HashSet<string>();

    private Dictionary<char, int[]> Directions = new Dictionary<char, int[]>() {
        { 'U', new int[] { 0, 1 } },
        { 'D', new int[] { 0, -1 } },
        { 'R', new int[] { 1, 0 } },
        { 'L', new int[] { -1, 0 } },
    };

    private char _direction;
    private int _sizeOfStep, i, j;
    private int[] _diff = { 0, 0 };
    internal void MakeMove(string str)
    {
        _direction = str[0];
        _sizeOfStep = int.Parse(str.Substring(2));

        for (i = 0; i < _sizeOfStep; i++)
        {
            MoveHead(_direction);
            for (j = 1; j < LENOFSNAKE; j++)
                if (MoveTail(j) == false)
                    break;

            AttendedPlaces.Add(PositionSnake[LENOFSNAKE - 1, 0] + " " + PositionSnake[LENOFSNAKE - 1, 1]);
        }
    }

    private bool MoveTail(int ind)
    {
        SetDiff(ind);
        if (_diff[0] == 2 || _diff[0] == -2 || _diff[1] == 2 || _diff[1] == -2)
        {
            PositionSnake[ind, 0] += Sign(_diff[0]);
            PositionSnake[ind, 1] += Sign(_diff[1]);
            return true;
        }
        else
            return false;
    }

    private void SetDiff(int ind)
    {
        _diff[0] = PositionSnake[ind - 1, 0] - PositionSnake[ind, 0];
        _diff[1] = PositionSnake[ind - 1, 1] - PositionSnake[ind, 1];
    }

    private void MoveHead(char c)
    {
        PositionSnake[0, 0] += Directions[c][0];
        PositionSnake[0, 1] += Directions[c][1];
    }

    internal long NofAttended()
    {
        return AttendedPlaces.Count();
    }

    private int Sign(int a)
    {
        if (a > 0)
            return 1;
        else if(a==0)
            return 0; 
        else return -1;
    }
}