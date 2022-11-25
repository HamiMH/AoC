internal class BagGraph
{
    public Dictionary<string, BagVertex> Graph = new Dictionary<string, BagVertex>();
    public BagGraph()
    {
    }

    public HashSet<BagVertex> GetSetOfTypeOfOuterBags(string nameOfBag)
    {
        BagVertex bagVertex = Graph[nameOfBag];
        HashSet<BagVertex> innerBags = new HashSet<BagVertex>();
        bagVertex.SetOfTypeOfOuterBags(innerBags);
        return innerBags;
    }

    public long GetNumberOfOuterBags(string nameOfBag)
    {
        BagVertex bagVertex = Graph[nameOfBag];
        return bagVertex.NumberOfOuterBags();
    }

    internal void AddEdge(string inputStrings)
    {
        inputStrings = inputStrings.Replace(".", "");
        inputStrings = inputStrings.Replace("bags", "bag");
        string[] parts = inputStrings.Split("contain");
        string upperBagStr = parts[0].Trim().Replace(" ", "");

        TryAddVertex(upperBagStr);
        BagVertex upperBag = Graph[upperBagStr];

        string textOfLowerBags = parts[1].Trim();
        string[] arrOfLowerBags = textOfLowerBags.Split(",");

        string strTemp;
        foreach (string str in arrOfLowerBags)
        {
            strTemp = str.Trim();
            string[] innerParts = strTemp.Split(" ");
            int number;
            if (innerParts[0].Trim() == "no")
                //number = 0;
                continue;
            else
                number = Convert.ToInt32(innerParts[0].Trim());
            string lowerBagStr = "";
            for (int i = 1; i < innerParts.Length; i++)
                lowerBagStr += innerParts[i].Trim();
            TryAddVertex(lowerBagStr);
            BagVertex lowerBag = Graph[lowerBagStr];

            upperBag.InnerBags.Add(lowerBag, number);
            lowerBag.OuterBags.Add(upperBag, number);
        }
    }

    private void TryAddVertex(string nameOfVertex)
    {
        if (!Graph.ContainsKey(nameOfVertex))
            Graph.Add(nameOfVertex, new BagVertex(nameOfVertex));
    }


}