using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day24cs
{

	enum NodeType
	{
		X,
		Y,
		Z,
		C,
		XorXY,
		AndXY,
		AndC,
	}
	enum GateType
	{
		XOR,
		OR,
		AND
	}
	internal class Node
	{
		public override bool Equals(object? obj)
		{
			if (obj is Node other)
			{
				return Index == other.Index && Type == other.Type;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Index, Type);
		}

		public int Index { get; set; }
		public NodeType Type { get; set; }

		public Node(int index, NodeType type)
		{
			Index = index;
			Type = type;
		}



		public Node? Combine(Node other, GateType gate)
		{
			if (Index != other.Index)
				return null;

			if (Index == 0)
			{
				if (gate == GateType.AND)
					return new Node(1, NodeType.C);
				else if (gate == GateType.XOR)
					return new Node(0, NodeType.Z);
				else
					return null;
			}

			if ((Type == NodeType.X && other.Type == NodeType.Y) || (Type == NodeType.Y && other.Type == NodeType.X))
			{
				if (gate == GateType.AND)
					return new Node(Index, NodeType.AndXY);
				else if (gate == GateType.XOR)
					return new Node(Index, NodeType.XorXY);
				else
					return null;
			}
			if ((Type == NodeType.XorXY && other.Type == NodeType.C) || (Type == NodeType.C && other.Type == NodeType.XorXY))
			{
				if (gate == GateType.AND)
					return new Node(Index, NodeType.AndC);
				else if (gate == GateType.XOR)
					return new Node(Index, NodeType.Z);
				else
					return null;
			}

			if ((Type == NodeType.AndXY && other.Type == NodeType.AndC) || (Type == NodeType.AndC && other.Type == NodeType.AndXY))
			{
				if (gate == GateType.OR)
					return new Node(Index + 1, NodeType.C);
				else
					return null;
			}

			return null;

		}

	}

	internal class Rule2
	{
		public string InFirst { get; set; }
		public string InSecond { get; set; }
		public string Out { get; set; }
		public GateType Gate { get; set; }
		public bool Applied { get; set; } = false;
		public string OrigLine { get; set; }
	}

	internal class AdderRectreation
	{
		List<Rule2> _rules = new List<Rule2>();
		public Dictionary<string, Node> _strToNode = new Dictionary<string, Node>();
		public AdderRectreation(List<string> inputCol)
		{
			int i = 0;
			for (; i < inputCol.Count; i++)
			{
				string ln = inputCol[i];
				if (ln == "")
					break;
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
				
				if (ln == "bfq XOR dcf -> gbs")
					ln = "bfq XOR dcf -> z29";
				else if (ln == "grd OR rpq -> z29")
					ln = "grd OR rpq -> gbs";

				List<string> list = new List<string>() { "z08", "thm", "wss", "wrm", "z22", "hwq", "gbs", "z29", };


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

				if (splitter == "XOR")
					_rules.Add(new Rule2() { InFirst = parts2[0].Trim(), InSecond = parts2[1].Trim(), Out = parts.Last(), Gate = GateType.XOR, OrigLine = ln });
				else if (splitter == "AND")
					_rules.Add(new Rule2() { InFirst = parts2[0].Trim(), InSecond = parts2[1].Trim(), Out = parts.Last(), Gate = GateType.AND, OrigLine = ln });
				else if (splitter == "OR")
					_rules.Add(new Rule2() { InFirst = parts2[0].Trim(), InSecond = parts2[1].Trim(), Out = parts.Last(), Gate = GateType.OR, OrigLine = ln });
			}
		}

		public void CalculateNewGraph()
		{
			bool noChange = false;
			while (!noChange)
			{
				noChange = true;
				foreach (Rule2 rule in _rules)
				{
					if (rule.Applied)
						continue;
					string inFirst = rule.InFirst;
					string inSecond = rule.InSecond;
					string outV = rule.Out;
					Node? nodeFirst = GetStrToNode(inFirst);
					Node? nodeSecond = GetStrToNode(inSecond);
					if (nodeFirst == null || nodeSecond == null)
						continue;
					noChange = false;
					rule.Applied = true;
					Node? newNode = nodeFirst.Combine(nodeSecond, rule.Gate);
					if (newNode == null)
					{
						Console.WriteLine("Error1: " + rule.OrigLine);
						Console.WriteLine("Error1: " + $"{nodeFirst.Index} : {nodeFirst.Type} , {nodeSecond.Type} -> ");

					}
					else
					{
						if (outV.StartsWith('z'))
						{
							if (!newNode.Equals(GetStrToNode(outV)))
							{

								Console.WriteLine("Error2: " + rule.OrigLine);
								Console.WriteLine("Error2: " + $"{nodeFirst.Index} : {nodeFirst.Type} , {nodeSecond.Type} -> {newNode.Type}");
								continue;
							}
						}
						if (newNode.Type == NodeType.Z)
						{
							if (!newNode.Equals(GetStrToNode(outV)))
							{

								Console.WriteLine("Error3: " + rule.OrigLine);
								Console.WriteLine("Error3: " + $"{nodeFirst.Index} : {nodeFirst.Type} , {nodeSecond.Type} -> {newNode.Type}");
								continue;
							}
						}
						_strToNode[outV] = newNode;
					}
				}
			}
			//CheckAfter();
			//PrintUnused(); ;

		}

		private void PrintUnused()
		{
			HashSet<string> attended = new HashSet<string>();
			Queue<string> queue = new Queue<string>();
			queue.Enqueue("x00");
			queue.Enqueue("y00");

			while (queue.Count > 0)
			{
				string nodeName = queue.Dequeue();
				if(attended.Contains(nodeName))
					continue;
				attended.Add(nodeName);
				if(!_strToNode.ContainsKey(nodeName))
				{
					Console.WriteLine("Unused: " + nodeName);
					continue;
				}	


				foreach (Rule2 rule in _rules)
				{
					if(rule.InFirst == nodeName || rule.InSecond==nodeName)
						queue.Enqueue(rule.Out);
				}
			}
		}

		private void CheckAfter()
		{
			int counter = 0;
			foreach (Rule2 rule in _rules)
			{
				if (!rule.Applied)
				{
					if (_strToNode.ContainsKey(rule.InFirst))
					{
						Console.WriteLine("Error4:++++++++++" + rule.OrigLine + ", contains: " + rule.InFirst);
					}
					if (_strToNode.ContainsKey(rule.InSecond))
					{
						Console.WriteLine("Error4:++++++++++" + rule.OrigLine + ", contains: " + rule.InSecond);
					}
					Console.WriteLine("Error4: " + rule.OrigLine);
					counter++;
				}
			}
			Console.WriteLine("Counter: " + counter);
		}



		private Node? GetStrToNode(string name)
		{
			if (name.StartsWith('x'))
				_strToNode[name]= new Node(int.Parse(name.Substring(1)), NodeType.X);
			if (name.StartsWith('y'))
				_strToNode[name] = new Node(int.Parse(name.Substring(1)), NodeType.Y);
			if (name.StartsWith('z'))
				_strToNode[name] = new Node(int.Parse(name.Substring(1)), NodeType.Z);

			if (_strToNode.ContainsKey(name))
				return _strToNode[name];
			return null;
		}
	}
}
