
namespace Day24cs
{
	internal class Rule
	{
		public string InFirst { get; set; }
		public string InSecond { get; set; }
		public string Out { get; set; }
		public Func<bool, bool, bool> Gate { get; set; }
		public bool Applied { get; set; }= false;
	}
	internal class GateSimulator
	{
		Dictionary<string, bool> _state = new Dictionary<string, bool>();
		List<Rule> _rules = new List<Rule>();
		public GateSimulator(List<string> inputCol)
		{
			Random ran= new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
			int i = 0;
			for (; i < inputCol.Count; i++)
			{
				string ln = inputCol[i];
				if (ln == "")
					break;



				string[] parts = ln.Split(": ");

				_state.Add(parts.First(), ran.Next(2)==0);
				//_state.Add(parts.First(),true);

				//if (parts.Last() == "0")
				//{
				//	_state.Add(parts.First(), false);
				//}
				//else
				//{
				//	_state.Add(parts.First(), true);
				//}
			}
			i++;
			for (; i < inputCol.Count; i++)
			{


				string ln = inputCol[i];


				if (ln == "vqp AND frr -> z08")
					ln = "vqp AND frr -> thm";
				else if (ln == "frr XOR vqp -> thm")
					ln = "frr XOR vqp -> z08";

				if (ln == "x14 AND y14 -> wss")
					ln = "x14 AND y14 -> wrm";
				else if (ln == "x14 XOR y14 -> wrm")
					ln = "x14 XOR y14 -> wss";


				if (ln == "y22 AND x22 -> z22")
					ln = "y22 AND x22 -> hwq";
				else if (ln == "cmn XOR cdf -> hwq")
					ln = "cmn XOR cdf -> z22";

				//if (ln == "bfq XOR dcf -> gbs")
				//	ln = "bfq XOR dcf -> z29";
				//else if (ln == "grd OR rpq -> z29")
				//	ln = "grd OR rpq -> gbs";

				string[] parts = ln.Split(" -> ");

				string splitter;
				if (parts.First().Contains("XOR"))
					splitter = "XOR";
				else if (parts.First().Contains("AND"))
					splitter = "AND";
				else if (parts.First().Contains("OR"))
					splitter = "OR";
				else
					throw new Exception("Unknown splitter");

				string[] parts2 = parts.First().Split(splitter);
				
				if(splitter== "XOR")
					_rules.Add(new Rule() { InFirst = parts2[0].Trim(), InSecond = parts2[1].Trim(), Out = parts.Last(), Gate = (a, b) => a ^ b });
				else if (splitter == "AND")
					_rules.Add(new Rule() { InFirst = parts2[0].Trim(), InSecond = parts2[1].Trim(), Out = parts.Last(), Gate = (a, b) => a & b });
				else if (splitter == "OR")
					_rules.Add(new Rule() { InFirst = parts2[0].Trim(), InSecond = parts2[1].Trim(), Out = parts.Last(), Gate = (a, b) => a | b });
			}
		}

		public long GetResult1()
		{
			Simulate();
			List<(string,bool)> stateList=_state.Select(x => (x.Key,x.Value)).ToList();
			List<(string, bool)> stateListZ = stateList.Where(x => x.Item1.StartsWith('z')).ToList();
			return LongValue(stateListZ);
		} 

		private long LongValue(List<(string, bool)> list)
		{
			long result = 0;
			list.Sort((x, y) => y.Item1.CompareTo(x.Item1));
			foreach ((string, bool) tuple in list)
			{
				result = (result << 1) + (tuple.Item2 ? 1 : 0);
			}
			return result;
		}
		public long GetResult2()
		{
			Simulate();
			List<(string,bool)> stateList=_state.Select(x => (x.Key,x.Value)).ToList();
			List<(string,bool)> stateListX= stateList.Where(x => x.Item1.StartsWith('x')).ToList();
			List<(string,bool)> stateListY= stateList.Where(x => x.Item1.StartsWith('y')).ToList();
			List<(string,bool)> stateListZ= stateList.Where(x => x.Item1.StartsWith('z')).ToList();		
			long target = LongValue(stateListX) + LongValue(stateListY);
			Console.WriteLine("Target: "+Convert.ToString(target, 2).PadLeft(46, '0'));
			Console.WriteLine("Real  : "+Convert.ToString(LongValue(stateListZ), 2).PadLeft(46, '0'));
			Console.WriteLine("Xor   : "+Convert.ToString(target^LongValue(stateListZ), 2).PadLeft(46, '0'));
			return LongValue(stateListZ);
		}

		private void Simulate()
		{
			bool noChange = false;
			while (!noChange)
			{
				noChange = true;
				foreach (Rule rule in _rules)
				{
					if (rule.Applied)
						continue;

					if (!(_state.ContainsKey(rule.InFirst) && _state.ContainsKey(rule.InSecond)))
						continue;					
					_state[rule.Out] = rule.Gate(_state[rule.InFirst], _state[rule.InSecond]);
					rule.Applied = true;
					noChange = false;
				}
			}
		}
	}
}