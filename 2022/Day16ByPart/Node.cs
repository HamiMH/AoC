public class Node 
{



    public string Name;
    public int Flow;
    public int Index;
    public bool IsOpened=false;
    public List<Node> Adjenced=new List<Node>();

    public int Distance=-1;
    public bool Attended=false;

    public Node(string name, int flow, int index)
    {
        this.Name = name;
        this.Flow = flow;
        this.Index = index;
    }
    public void AddAdjenced(Node node) { Adjenced.Add(node); }



    public override bool Equals(object? obj)
    {
        return Name.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

}