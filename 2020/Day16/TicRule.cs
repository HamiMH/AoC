public class TicRule
{

    public TicRule(string str)
    {
        Ranges = new List<Tuple<int, int>>();
        string[] strArr=str.Split(":");
        string[] strArr2;
        Name = strArr[0].Trim();
        strArr2 = strArr[1].Split("or");

        for (int i = 0; i < 20; i++)
            CanBeRuleOf[i] = true;

        foreach (string s in strArr2)
        {
            string ss=s.Trim();
            string []ssArr=ss.Split("-");
            Ranges.Add(new Tuple<int, int>(int.Parse(ssArr[0]), int.Parse(ssArr[1])));
        }
    }

    public string Name { get; set; }
    public List<Tuple<int, int>> Ranges { get; set; }
    public bool[] CanBeRuleOf { get; set; } = new bool[20];
    public int OrderOfRule { get; set; } = -1;

    public bool ItemIsInRanges(int item)
    {

        foreach(Tuple<int,int> tup in Ranges)
        {
            if(item>=tup.Item1 && item<= tup.Item2)
                return true;
        }
        return false;
    }

    public void ItemHasOrderN(int item,int orderN)
    {

        foreach (Tuple<int, int> tup in Ranges)
        {
            if (item >= tup.Item1 && item <= tup.Item2)
                return;
        }
        CanBeRuleOf[orderN] = false;
    }
}