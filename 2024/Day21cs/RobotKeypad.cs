//using System.Collections.Generic;

//namespace Day21cs
//{
//	internal class RobotKeypad
//	{
//		private static readonly List<List<char>> KEYPAD = new List<List<char>>()
//		{
//			new List<char> { ' ', '^', '0' },
//			new List<char> { '<', 'v', '>' },
//		};
//		private (int, int) _position = (2, 0); // 5

//		private DoorKeypad _doorKeypad;
//		internal RobotKeypad(DoorKeypad doorKeypad)
//		{
//			_doorKeypad = doorKeypad;
//		}
//		public long RunSimulation()
//		{
//			int targetButton;
//			long targetSum = 0;
//			while (_doorKeypad.TryGetNext(out targetButton))
//			{
//				targetSum += GoFor(targetButton);
//			}

//			return targetSum;

//		}
//		public long GoFor(int targetButton)
//		{

//			PriorityQueue<(int, int), long> queue = new PriorityQueue<(int, int), long>();

//			queue.Enqueue(_position, 0);
//			while (queue.Count > 0)
//			{
//				long steps;
//				(int, int) position;
//				queue.TryDequeue(out position, out steps);

//				if (_doorKeypad.PointsTo(position) == targetButton)
//				{
//					return steps;
//				}
//				else
//				{
//					(int, int) outPosition;
//					if (Move('<', position, out outPosition))
//					{
//						queue.Enqueue(outPosition, steps + 1);
//					}
//					if (Move('^', position, out outPosition))
//					{
//						queue.Enqueue(outPosition, steps + 1);
//					}
//					if (Move('>', position, out outPosition))
//					{
//						queue.Enqueue(outPosition, steps + 1);
//					}
//					if (Move('v', position, out outPosition))
//					{
//						queue.Enqueue(outPosition, steps + 1);
//					}
//				}
//			}
//			return long.MinValue;
//		}
//		public bool Move(char c, (int, int) inPosition, out (int, int) outPosition)
//		{
//			switch (c)
//			{
//				case '<':
//					if (inPosition.Item1 > 0)
//					{
//						inPosition.Item1--;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				case '>':
//					if (inPosition.Item1 < 2)
//					{
//						inPosition.Item1++;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				case '^':
//					if (inPosition.Item2 > 0)
//					{
//						inPosition.Item2--;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				case 'v':
//					if (inPosition.Item2 < 1)
//					{
//						inPosition.Item2++;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				default:
//					throw new ArgumentException("Invalid direction");
//			}
//		}
//	}
//	internal class DoorKeypad
//	{
//		//v<<A>>^A<A>AvA<^AA>A<vAAA>^A.
//		private string targetSequence;

//		private static readonly List<List<int>> KeypadDistances = new List<List<int>>()
//		{
//			new List<int>(){0,2,1,2,3,2,3,4,3,4,1 },
//			new List<int>(){2,0,1,2,1,2,3,2,3,4,3 },
//			new List<int>(){1,1,0,1,2,1,2,3,2,3,2 },
//			new List<int>(){2,2,1,0,3,2,1,4,3,2,1 },
//			new List<int>(){3,1,2,3,0,1,2,1,2,3,4 },
//			new List<int>(){2,2,1,2,1,0,1,2,1,2,3 },
//			new List<int>(){3,3,2,1,2,1,0,3,2,1,2 },
//			new List<int>(){4,2,3,4,1,2,3,0,1,2,5 },
//			new List<int>(){3,3,2,3,2,1,2,1,0,1,4 },
//			new List<int>(){4,4,3,2,3,2,1,2,1,0,3 },
//			new List<int>(){1,3,2,1,4,3,2,5,4,3,0 },
//		}; 
//		private static readonly List<List<int>> KeypadDistances2 = new List<List<int>>()
//		{
//			new List<int>(){1,9,4,-,-,-,-,-,-,-,4 },//0
//			new List<int>(){-,-,4,5,4,-,-,5,-,-,- },//1
//			new List<int>(){6,8,-,4,9,4,-,-,5,-,- },//2
//			new List<int>(){-,9,8,-,-,9,4,10,-,5,6 },//3
//			new List<int>(){-,6,-,-,-,4,5,4,-,-,- },//4
//			new List<int>(){7,-,6,-,8,-,4,9,4,-,- },//5
//			new List<int>(){-,-,-,6,9,8,-,-,9,4,7 },//6
//			new List<int>(){-,7,-,-,6,-,-,-,4,-,5 },//7
//			new List<int>(){-,-,7,-,-,6,-,8,-,4,- },//8
//			new List<int>(){-,-,-,7,-,-,6,9,8,-,- },//9
//			new List<int>(){8,-,9,4,10,-,-,-,-,-,- },//A
//		};

//		private static readonly List<List<int>> KEYPAD = new List<List<int>>()
//		{
//			new List<int> { 7, 8, 9 },
//			new List<int> { 4, 5, 6 },
//			new List<int> { 1, 2, 3 },
//			new List<int> { -1, 0, 10 },
//		};
//		private (int, int) _position = (2, 3); // 5

//		private int _index = 0;


//		private bool IsSymetrical(List<List<int>> keypadDistances)
//		{
//			for (int i = 0; i < keypadDistances.Count; i++)
//			{
//				for (int j = 0; j < keypadDistances[i].Count; j++)
//				{
//					if (keypadDistances[i][j] != keypadDistances[j][i])
//					{
//						return false;
//					}
//				}
//			}
//			return true;
//		}

//		public bool TryGetNext(out int outVal)
//		{
//			if (_index < targetSequence.Length)
//			{
//				char c = targetSequence[_index];
//				if (c == 'A')
//					outVal = 10;
//				else
//					outVal = c - '0';
//				_index++;
//				return true;
//			}
//			outVal = -1;
//			return false;
//		}

//		public bool Move(char c, (int, int) inPosition, out (int, int) outPosition)
//		{
//			switch (c)
//			{
//				case '<':
//					if (inPosition.Item1 > 0)
//					{
//						inPosition.Item1--;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				case '>':
//					if (inPosition.Item1 < 2)
//					{
//						inPosition.Item1++;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				case '^':
//					if (inPosition.Item2 > 0)
//					{
//						inPosition.Item2--;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				case 'v':
//					if (inPosition.Item2 < 3)
//					{
//						inPosition.Item2++;
//						outPosition = inPosition;
//						return true;
//					}
//					outPosition = (-1, 1);
//					return false;
//				default:
//					throw new ArgumentException("Invalid direction");
//			}
//		}
//		public int PointsTo((int, int) inPosition)
//		{
//			return KEYPAD[inPosition.Item1][inPosition.Item2];
//		}

//		public DoorKeypad(string targetSequence)
//		{
//			Console.WriteLine(IsSymetrical(KeypadDistances));
//			this.targetSequence = targetSequence;
//		}
//	}
//}