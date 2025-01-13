


using System.Text;

namespace Day23cs
{
	internal class LanMap
	{
		private Dictionary<string, HashSet<string>> _lanMap = new Dictionary<string, HashSet<string>>();
		private List<string> _lanNodes ;
		public LanMap(List<string> inputCol)
		{
			foreach (string ln in inputCol)
			{
				string[] parts = ln.Split("-");
				if (!_lanMap.ContainsKey(parts[0]))
				{
					_lanMap.Add(parts[0], new HashSet<string>() { parts[0] });
				}
				_lanMap[parts[0]].Add(parts[1]);

				if (!_lanMap.ContainsKey(parts[1]))
				{
					_lanMap.Add(parts[1], new HashSet<string>() { parts[1] });
				}
				_lanMap[parts[1]].Add(parts[0]);
			}

			IEnumerable<string> withT = _lanMap.Keys.Where(x =>x.StartsWith('t'));
			IEnumerable<string> withoutT = _lanMap.Keys.Where(x => !x.StartsWith('t'));
			_lanNodes = withT.Concat(withoutT).ToList();
		}

		public string GetResult2()
		{
			RecourseCreateGroups(0, new HashSet<string>());
			_maxGroup.Sort();
			return PrintList(_maxGroup);
		}

		private int _maxGroupSize = 0;
		private List<string> _maxGroup = new List<string>();
		private void RecourseCreateGroups(int index, HashSet<string> inGroup)
		{
			if (inGroup.Count > _maxGroupSize)
			{
				_maxGroupSize = inGroup.Count;
				_maxGroup = new List<string>(inGroup);
			}
			if (inGroup.Count == 0)
			{
				if (!_lanNodes[index].StartsWith('t'))
					return;
			}
			for (int i = index; i < _lanNodes.Count; i++)
			{
				string node = _lanNodes[i];
				HashSet<string> nodeNeib=_lanMap[node].ToHashSet();

				if (nodeNeib.Intersect(inGroup).Count()<inGroup.Count)
					continue;
				inGroup.Add(node);
				RecourseCreateGroups(i+1, inGroup);
				inGroup.Remove(node);
			}
		}

		private string PrintList(List<string> maxGroupList)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in maxGroupList)
			{
				if (sb.Length > 0)
					sb.Append(",");
				sb.Append(s);
			}
			return sb.ToString();
		}

		public long GetResult()
		{
			HashSet<(string, string, string)> triples = new HashSet<(string, string, string)>();
			foreach (KeyValuePair<string, HashSet<string>> kv in _lanMap)
			{
				string startNode = kv.Key;
				if (!(startNode[0] == 't'))
					continue;
				HashSet<string> startList = kv.Value;
				foreach (string nextNode in startList)
				{
					if (startNode == nextNode)
						continue;
					HashSet<string> nowList = _lanMap[nextNode];
					List<string> interSection = startList.Intersect(nowList).ToList();
					foreach (string thirdNode in interSection)
					{
						if (startNode == thirdNode || nextNode == thirdNode)
							continue;
						List<string> triple = new List<string> { startNode, nextNode, thirdNode };
						triple.Sort();
						triples.Add((triple[0], triple[1], triple[2]));
					}
				}
			}

			return triples.Count;
		}
	}
}