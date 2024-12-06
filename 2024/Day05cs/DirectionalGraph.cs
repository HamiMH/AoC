
namespace Day05cs
{
	internal class DirectionalGraph
	{
		private Dictionary<int,List<int>> _incomEdges = new Dictionary<int, List<int>>();
		private List<List<int>> _rules = new List<List<int>>();

		internal void AddEdge(string str)
		{
			string[] parts = str.Split("|");
			int from = int.Parse(parts[0]);
			int into = int.Parse(parts[1]);
			if (!_incomEdges.ContainsKey(from))
				_incomEdges[from] = new List<int>();
			if (!_incomEdges.ContainsKey(into))
				_incomEdges[into] = new List<int>();

			_incomEdges[into].Add(from);
		}

		internal void AddRule(string str)
		{
			List<int> rule = str.Split(",").Select(int.Parse).ToList();
			_rules.Add(rule);
		}


		internal int ValidateRulesNoSwap()
		{
			return _rules.Sum(ValidateRuleNoSwap);
		}
		
		internal int ValidateRulesWithSwap()
		{
			return _rules.Sum(x=> { return ValidateRuleNoSwap(x) == 0 ?  ValidateRuleWithSwap(x):0; });
		}

		public int ValidateRuleNoSwap(List<int> rule)
		{
			HashSet<int> visited = new HashSet<int>();
			HashSet<int> relevant = rule.ToHashSet();

			foreach (int r in rule)
			{
				List<int> incoming = _incomEdges[r];
				foreach (int i in incoming)
				{
					if (!relevant.Contains(i))
						continue;
					if (!visited.Contains(i))
						return 0;
				}
				visited.Add(r);
			}
			return rule[rule.Count / 2];
		}

		private int ValidateRuleWithSwap(List<int> rule)
		{
			HashSet<int> visited = new HashSet<int>();
			HashSet<int> relevant = rule.ToHashSet();
			List<int> outRule = new List<int>();

			while (0 < rule.Count)
			{
				int r = rule.First();
				List<int> incoming = _incomEdges[r];
				int newFirst= -1;
				foreach (int j in incoming)
				{
					if (!relevant.Contains(j))
						continue;
					if (!visited.Contains(j))
						newFirst = j;
				}
				if (newFirst > 0)
				{
					rule.Remove(newFirst);
					rule.Insert(0, newFirst);
					continue;
				}
				rule.Remove(r);
				visited.Add(r);
				outRule.Add(r);
			}
			return outRule[outRule.Count / 2];
		}
	}
}