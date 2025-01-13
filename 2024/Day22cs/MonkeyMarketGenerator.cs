
namespace Day22cs
{
	internal class MonkeyMarketGenerator
	{
		private static readonly long PRUNE = 16777216;
		private List<long> _numbersToAnalyze;

		public MonkeyMarketGenerator(List<string> inputCol)
		{
			_numbersToAnalyze = inputCol.Select(long.Parse).ToList();
		}

		public long GetResult() => _numbersToAnalyze.Select(x => GenerateNumberAfter(x, 2000)).Sum();

		public long GenerateNextNumber(long old)
		{
			old = old ^ (old * 64);
			old = old % PRUNE;
			old = old ^ (old / 32);
			old = old % PRUNE;
			old = old ^ (old * 2048);
			old = old % PRUNE;
			return old;
		}
		public long GenerateNumberAfter(long old, int n)
		{
			for (int i = 0; i < n; i++)
			{
				old = GenerateNextNumber(old);
			}
			return old;
		}
		public List<long> First2000(long first)
		{
			List<long> result = new List<long>();
			result.Add(first);
			for (int i = 0; i < 2000; i++)
			{
				first = GenerateNextNumber(first);
				result.Add(first);
			}
			return result;
		}
		private Dictionary<(long, long, long, long), long> ResolveList(List<long> list)
		{
			Dictionary<(long, long, long, long), long> result = new Dictionary<(long, long, long, long), long>();
			for (int i = 4; i < list.Count; i++)
			{
				long a = list[i - 3] % 10 - list[i - 4] % 10;
				long b = list[i - 2] % 10 - list[i - 3] % 10;
				long c = list[i - 1] % 10 - list[i - 2] % 10;
				long d = list[i] % 10 - list[i - 1] % 10;
				long val = list[i] % 10;
				if (!result.ContainsKey((a, b, c, d)))
					result.Add((a, b, c, d), val);
			}
			return result;
		}

		public long GetResult2()
		{
			List<Dictionary<(long, long, long, long), long>> tmpDictList = _numbersToAnalyze.Select(x => First2000(x)).Select(x => ResolveList(x)).ToList();
			Dictionary<(long, long, long, long), long> result = new Dictionary<(long, long, long, long), long>();
			foreach (Dictionary<(long, long, long, long), long> dict in tmpDictList)
			{
				foreach (var item in dict)
				{
					if (!result.ContainsKey(item.Key))
						result.Add(item.Key, 0);
					result[item.Key] += item.Value;
				}
			}
			return result.Values.Max();
		}
	}
}