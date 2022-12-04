internal class Assignment
{

    private int _firstBeg;
    private int _firstEnd;
    private int _seconBeg;
    private int _seconEnd;

    internal bool IsOverlapping(string str)
    {
        FillFields(str);

        if(
            (_firstBeg<=_seconBeg && _firstEnd >= _seconEnd)
            ||
            (_seconBeg <= _firstBeg && _seconEnd >= _firstEnd))
            return true;
        else
            return false;
    }

    private void FillFields(string str)
    {
        string[] strArr = str.Split(',');
        string[] strArr1 = strArr[0].Split('-');
        string[] strArr2 = strArr[1].Split('-');

        _firstBeg = int.Parse(strArr1[0]);
        _firstEnd = int.Parse(strArr1[1]);
        _seconBeg = int.Parse(strArr2[0]);
        _seconEnd = int.Parse(strArr2[1]);
    }

    internal bool IsPartlyOverlapping(string str)
    {
        FillFields(str);

        if (_seconEnd < _firstBeg || _firstEnd < _seconBeg)
            return false;
        else
            return true;
    }
}