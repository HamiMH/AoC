using System.Text;

internal class Simulator
{
    private int _addition;
    private int _time = 0;
    private int _value = 1;
    private int _totalScore = 0;
    private int _col;
    private StringBuilder Stb = new StringBuilder();

    internal void ExecCommand(string str)
    {
        if (str == "noop")
            DoTimeIncrement();
        else
        {
            _addition = int.Parse(str.Substring(5));
            DoTimeIncrement();
            DoTimeIncrement();
            DoAddition();
        }
    }

    private void DoAddition()
    {
        _value += _addition;
    }

    private void DoTimeIncrement()
    {
        _col = _time % 40;
        if (_col == 0)
            Stb.AppendLine();
        if (InRange(_col, _value))
            Stb.Append('#');
        else
            Stb.Append('.');

         _time++;
        if (_time <= 220 && (_time - 20) % 40 == 0)
            _totalScore += (_time * _value);
    }

    public int GetTotalScore()
    {
        return _totalScore;
    }

    private bool InRange(int a, int b)
    {
        if (a == b || a + 1 == b || a == b + 1)
            return true;
        else
            return false;
    }

    internal string GetRastr()
    {
        return Stb.ToString();
    }
}
