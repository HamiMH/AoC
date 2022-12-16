public class FastNode
{



    public string Name;
    public int Flow;
    public int Index;
    public bool IsOpened=false;
    public Dictionary<FastNode,int> Adjenced=new Dictionary<FastNode,int>();

    public FastNode(string name, int flow, int index)
    {
        this.Name = name;
        this.Flow = flow;
        this.Index = index;
    }
    public void AddAdjenced(FastNode node,int wei) { Adjenced.Add(node,wei); }



    public override bool Equals(object? obj)
    {
        return Name.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}