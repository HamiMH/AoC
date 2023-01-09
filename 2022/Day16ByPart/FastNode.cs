public class FastNode
{



    public string Name;
    public int Flow;
    public int Index;
    public Dictionary<FastNode, int> Adjenced = new Dictionary<FastNode, int>();

    public FastNode(string name, int flow, int index)
    {
        this.Name = name;
        this.Flow = flow;
        this.Index = index;
    }
    public void AddAdjenced(FastNode node, int wei) { Adjenced.Add(node, wei); }



    public override bool Equals(object? obj)
    {
        return Name.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public bool IsOpened(ulong openedBitmap)
    {
        if ((openedBitmap & (((ulong)1) << Index)) > 0)
            return true;
        else
            return false;
    }

    internal int GetValueToEnd(int timeWhenIsOpened, int timeOfEnd)
    {
        int val= (timeOfEnd - timeWhenIsOpened) * Flow;
        //if (val > 400)
        //    val = val;
        return val;
    }
}