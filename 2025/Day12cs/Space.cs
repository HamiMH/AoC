namespace Day12cs
{
    internal class Space
    {
        int Xsize;
        int Ysize;
        List<int> Counts;
        public Space(string line)
        {
            string[] parts = line.Split(':');

            string[] sizes = parts[0].Trim().Split('x');
            Xsize = int.Parse(sizes[0]);
            Ysize = int.Parse(sizes[1]);

            string[]counts=parts[1].Trim().Split(" ");
            Counts=counts.Select(c=>int.Parse(c)).ToList();
        }
    }
}