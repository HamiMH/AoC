using System;

public class Ticket
{
    public List<int> Items { get; set; }

    public Ticket(string v)
    {
        Items=new List<int>();
        string [] strArr=v.Split(",");
        foreach (string s in strArr)
        {
            string ss = s.Trim();
            Items.Add(int.Parse(ss));
        }
    }
}