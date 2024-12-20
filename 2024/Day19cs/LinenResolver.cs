


using System;
using System.Collections;

namespace Day19cs
{
	internal class LinenResolver
	{
		private List<string> _itemsToResolve;
		private List<string> _bag;
		Dictionary<string, long> MEMO = new Dictionary<string, long>(20000);

		public LinenResolver(List<string> inputCol)
		{
			_bag = inputCol.First().Split(", ").ToList();
			_itemsToResolve = inputCol.Skip(2).ToList();
		}
		internal List<long> Combinations()
		{
			List<long> result = new List<long>();
			foreach (string item in _itemsToResolve)
			{
				result.Add(ItemHasCombs(item));
			}
			return result;
		}

		private long ItemHasCombs(string item)
		{
			if (MEMO.ContainsKey(item))
				return MEMO[item];
			long combs = 0;
			for (int i = 0; i < _bag.Count; i++)
			{
				if (item[0] != _bag[i][0])
					continue;
				if (item == _bag[i])
				{
					MEMO[item] = 1;
					combs++;
				}
				else if (item.StartsWith(_bag[i]))
					combs += ItemHasCombs(item.Substring(_bag[i].Length));
			}
			MEMO[item] = combs;
			return combs;
		}
	}
}