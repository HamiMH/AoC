internal abstract class RpsBase
{
    protected int Win { get; } = 6;
    protected int Draw { get; } = 3;
    protected int Loss { get; } = 0;

    protected int Rock { get; } = 1;
    protected int Paper { get; } = 2;
    protected int Sciss { get; } = 3;
    protected Dictionary<string, int> _dict=new Dictionary<string, int>();

    
    public int GetPoints(string str)
    {
        return _dict[str];
    }
}