public class Counter1 : ICounter
{
    private HashSet<char>? _hs;
    public int GetCount(Grup group)
    {
        if (_hs == null)
        {
            _hs = new HashSet<char>();
            foreach (string row in group.Rows)
            {
                foreach (char c in row)
                    _hs.Add(c);
            }
        }
        return _hs.Count;
    }

    public void Reset()
    {
        _hs = null;
    }
}