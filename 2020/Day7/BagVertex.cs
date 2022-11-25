using System.Collections.Generic;

public class BagVertex
{
    public bool Equals(BagVertex x, BagVertex y)
    {
        return x.Name == y.Name;
    }

    public int GetHashCode(BagVertex x)
    {
        return x.Name.GetHashCode();
    }

    public string Name { get; set; }
    public Dictionary<BagVertex, int> InnerBags { get; set; } = new Dictionary<BagVertex, int>();
    public Dictionary<BagVertex, int> OuterBags { get; set; } = new Dictionary<BagVertex, int>();

    public BagVertex(string name)
    {
        Name = name;
    }

    public void SetOfTypeOfOuterBags(HashSet<BagVertex> innerBags)
    {
        //HashSet<BagVertex> innerBags = new HashSet<BagVertex>();
        innerBags.Add(this);
        foreach (BagVertex bagVertex in OuterBags.Keys)
            bagVertex.SetOfTypeOfOuterBags(innerBags);
    }

    internal long NumberOfOuterBags()
    {
        long total = 1;

        BagVertex bagVertex = null;
        int value;
        foreach(KeyValuePair<BagVertex,int> keyValuePair in InnerBags)
        {
            bagVertex=keyValuePair.Key;
            value=keyValuePair.Value;
            total += bagVertex.NumberOfOuterBags() * ((long)value);
        }

        return total;
    }
}