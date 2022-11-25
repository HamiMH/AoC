
public class Counter2 : ICounter
{
    private HashSet<char>? _hs;
    public int GetCount(Grup group)
    {
        if (_hs == null)
        {
            HashSet<char> tmpHs; 
            _hs = new HashSet<char>();
            foreach (char c in group.Rows.First())
                _hs.Add(c);

            foreach (string row in group.Rows)
            {
                tmpHs = new HashSet<char>();
                foreach (char c in row)
                {
                    if (_hs.Contains(c))
                        tmpHs.Add(c);
                }
                _hs = tmpHs;
            }
        }
        Console.WriteLine(_hs.Count);
        return _hs.Count;
    }

    public void Reset()
    {
        _hs = null;
    }
}
