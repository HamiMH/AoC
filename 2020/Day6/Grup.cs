using System.Diagnostics.Metrics;

public class Grup
{
    public ICounter Counter { get; }
    public Grup(ICounter counter)
    {
        Counter = counter;
    }

    public List<string> Rows { get; set; } = new List<string>();


    internal void AddData(string str)
    {
        Rows.Add(str);
        Counter.Reset();
    }

    internal long GrCount()
    {

        return Counter.GetCount(this);
    }
}