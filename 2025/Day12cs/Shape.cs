



namespace Day12cs
{
    internal class Shape
    {
        public List<string> Rows { get; } = new List<string>();

        public List <List <string>> Variants= new List<List <string>> ();

        internal void AddLine(string line)
        {
            Rows.Add(line);
        }

        public void Init()
        {
            Variants.Clear ();
            AddVariant(Rows);
            AddVariant(Flip(Rows));
            List<string> rotate1 = Rotate(Rows);
            AddVariant(rotate1);
            AddVariant(Flip(rotate1));

            List<string> rotate2 = Rotate(rotate1);
            AddVariant(rotate2);
            AddVariant(Flip(rotate2));

            List<string> rotate3 = Rotate(rotate2);
            AddVariant(rotate3);
            AddVariant(Flip(rotate3));
        }

        private List<string> Flip(List<string> rows)
        {
            List<string> result = new List<string>();
            foreach (string row in rows)
            {
                char[] arr = row.ToCharArray();
                Array.Reverse(arr);
                result.Add(new string (arr));
            }
            return result;
        }

        private void AddVariant(List<string> rows)
        {
            foreach (List<string> variant in Variants)
            {
                if (Compare(variant, rows)) return;
            }
            Variants.Add(rows);
        }

        private List<string> Rotate(List<string> rows)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < rows.First().Length; i++)
            {
                string row = "";
                for (int j = rows.Count - 1; j >= 0; j--)
                {
                    row += rows[j][i];
                }
                result.Add(row);
            }
            return result;
        }

        public bool Compare (List<string> first, List<string> second)
        {
            if (first.Count != second.Count) return false;
            for (int i = 0; i < first.Count; i++)
            {
                if (first[i] != second[i]) return false;
            }
            return true;
        }
    }
}