internal class RrcRount
{

    private char _myChoice;
    private char _opChoice;
    private char _myAction;

    public RrcRount(string str)
    {
        string[] strArr = str.Split(" ");
        Set(ref _opChoice, strArr[0].Trim());
        Set(ref _myChoice, strArr[1].Trim());
        SetA(ref _myAction, strArr[1].Trim());
    }


    private void SetA(ref char choice, string v)
    {
        switch (v[0])
        {
            case 'A':
            case 'X':
                choice = 'l';
                break;
            case 'B':
            case 'Y':
                choice = 'd';
                break;
            case 'C':
            case 'Z':
                choice = 'w';
                break;
            default:
                throw new ArgumentException();
        }
    }

    internal long GetPoints1()
    {
        return Value1() + Fight();
    }

     internal long GetPoints2()
    {
        switch (_myAction)
        {
            case 'w':
                switch (_opChoice)
                {
                    case 'r':
                        return 6 + 2;
                    case 'p':
                        return 6 + 3;
                    case 's':
                        return 6 + 1;
                }
                break;
            case 'd':
                switch (_opChoice)
                {
                    case 'r':
                        return 3 + 1;
                    case 'p':
                        return 3 + 2;
                    case 's':
                        return 3 + 3;
                }
                break;
            case 'l':
                switch (_opChoice)
                {
                    case 'r':
                        return 0 + 3;
                    case 'p':
                        return 0 + 1;
                    case 's':
                        return 0 + 2;
                }
                break;
        }
        throw new ArgumentException();
    }

    private void Set(ref char choice, string v)
    {
        switch (v[0])
        {
            case 'A':
            case 'X':
                choice = 'r';
                break;
            case 'B':
            case 'Y':
                choice = 'p';
                break;
            case 'C':
            case 'Z':
                choice = 's';
                break;
            default:
                throw new ArgumentException();
        }
    }

    private long Fight()
    {
        switch (_myChoice)
        {
            case 'r':
                switch (_opChoice)
                {
                    case 'r':
                        return 3;
                    case 'p':
                        return 0;
                    case 's':
                        return 6;
                }
                break;
            case 'p':
                switch (_opChoice)
                {
                    case 'r':
                        return 6;
                    case 'p':
                        return 3;
                    case 's':
                        return 0;
                }
                break;
            case 's':
                switch (_opChoice)
                {
                    case 'r':
                        return 0;
                    case 'p':
                        return 6;
                    case 's':
                        return 3;
                }
                break;
        }
        throw new Exception("Fight");
    }

    private long Value1()
    {
        switch (_myChoice)
        {
            case 'r':
                return 1;
            case 'p':
                return 2;
            case 's':
                return 3;
        }
        throw new Exception("Value");
    }
}

