using System.ComponentModel;
using System.Text;

namespace Day16cs
{
	internal struct State : IEquatable<State>
	{
		private static readonly List<(int, int)> _directions = new List<(int, int)> { (-1, 0), (0, 1), (1, 0), (0, -1) };
		public (int, int) Position;
		public (int, int) Direction { get => _directions[_directionIndex]; }
		public int Cost;
		private int _directionIndex;
		public int DirectionIndex { get => _directionIndex % 4; set { _directionIndex = (value + 4) % 4; } }

		public override int GetHashCode()
		{
			return HashCode.Combine(Position, DirectionIndex);
		}
		public bool Equals(State other)
		{
			return Position == other.Position &&
				   DirectionIndex == other.DirectionIndex;
		}
	}
	internal class Maze
	{
		private List<List<int>> _layout;
		private (int, int) _begPosition;
		private (int, int) _endPosition;
		private State _finalPosition;
		HashSet<State> _attended = new HashSet<State>();

		public Maze(List<string> inputCol)
		{
			_layout = new List<List<int>>();
			int i;
			for (i = 0; i < inputCol.Count; i++)
			{
				List<int> row = new List<int>();
				for (int j = 0; j < inputCol[i].Length; j++)
				{
					switch (inputCol[i][j])
					{
						case '#':
							row.Add(2);
							break;
						case '.':
							row.Add(0);
							break;
						case 'S':
							_begPosition = (i, j);
							row.Add(3);
							break;
						case 'E':
							_endPosition = (i, j);
							row.Add(4);
							break;
					}
				}
				_layout.Add(row);
			}
		}

		public int FindEnd()
		{
			State state = new State { Position = _begPosition, DirectionIndex = 1, Cost = 0 };

			PriorityQueue<State, int> queue = new PriorityQueue<State, int>();
			queue.Enqueue(state, 0);

			while (queue.Count > 0)
			{
				state = queue.Dequeue();
				if (_attended.Contains(state))
					continue;
				if (_layout[state.Position.Item1][state.Position.Item2] == 4)
				{
					return state.Cost;
				}
				_attended.Add(state);
				(int, int) newPos = (state.Position.Item1 + state.Direction.Item1, state.Position.Item2 + state.Direction.Item2);
				if (_layout[newPos.Item1][newPos.Item2] != 2)
				{
					State newState = new State { Position = newPos, DirectionIndex = state.DirectionIndex, Cost = state.Cost + 1 };
					queue.Enqueue(newState, newState.Cost);
				}
				State newState1 = new State { Position = state.Position, DirectionIndex = state.DirectionIndex + 1, Cost = state.Cost + 1000 };
				queue.Enqueue(newState1, newState1.Cost);

				State newState2 = new State { Position = state.Position, DirectionIndex = state.DirectionIndex - 1, Cost = state.Cost + 1000 };
				queue.Enqueue(newState2, newState2.Cost);
			}
			return -1;
		}

		public int TravelBackwardsCallDFS2(int finalCost)
		{
			_attended = new HashSet<State>(_attended,new EqualityComparerStateWithPrice());
			HashSet<State> visitedStates = new HashSet<State>();
			HashSet<(int, int)> validPosition = new HashSet<(int, int)>();
			State state0 = new State { Position = _endPosition, DirectionIndex = 0, Cost = finalCost };
			State state1 = new State { Position = _endPosition, DirectionIndex = 1, Cost = finalCost };
			State state2 = new State { Position = _endPosition, DirectionIndex = 2, Cost = finalCost };
			State state3 = new State { Position = _endPosition, DirectionIndex = 3, Cost = finalCost };
			_attended.Add(state0);
			_attended.Add(state1);
			_attended.Add(state2);
			_attended.Add(state3);
			TravelBackwardsDFS2(state0, visitedStates, validPosition);
			TravelBackwardsDFS2(state1, visitedStates, validPosition);
			TravelBackwardsDFS2(state2, visitedStates, validPosition);
			TravelBackwardsDFS2(state3, visitedStates, validPosition);
			//PrintVisited(validPosition);
			return validPosition.Count;
		}

		public bool TravelBackwardsDFS2(State state, HashSet<State> visitedStates, HashSet<(int, int)> validPosition)
		{
			if (visitedStates.Contains(state))
				return false;
			visitedStates.Add(state);

			State testingState = new State { Position = state.Position, DirectionIndex = state.DirectionIndex + 2, Cost = state.Cost };
			if (!_attended.Contains(testingState))
				return false;

			if (_layout[state.Position.Item1][state.Position.Item2] == 3)
			{
				validPosition.Add(state.Position);
				return true;
			}

			bool[] anyOk =new bool[3] { false,false,false} ;
			(int, int) newPos = (state.Position.Item1 + state.Direction.Item1, state.Position.Item2 + state.Direction.Item2);
			if (_layout[newPos.Item1][newPos.Item2] != 2)
			{
				State newState0 = new State { Position = newPos, DirectionIndex = state.DirectionIndex, Cost = state.Cost - 1 };

				if (TravelBackwardsDFS2(newState0, visitedStates, validPosition))
				{
					anyOk[0] = true;
				}
			}

			State newState1 = new State { Position = state.Position, DirectionIndex = state.DirectionIndex + 1, Cost = state.Cost - 1000 };

			if (TravelBackwardsDFS2(newState1, visitedStates, validPosition))
			{
				anyOk[1] = true;
			}
			
			State newState2 = new State { Position = state.Position, DirectionIndex = state.DirectionIndex - 1, Cost = state.Cost - 1000 };
			if (TravelBackwardsDFS2(newState2, visitedStates, validPosition))
			{
				anyOk[2] = true;
			}
			if (anyOk.Any())
				validPosition.Add(state.Position);
			return anyOk.Any();
		}
		private void PrintVisited(HashSet<(int, int)> visitedPositions)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < _layout.Count; i++)
			{
				for (int j = 0; j < _layout[i].Count; j++)
				{
					if (visitedPositions.Contains((i, j)))
						sb.Append("X");
					else
						sb.Append(_layout[i][j] == 2 ? "#" : ".");
				}
				sb.AppendLine();
			}
			Console.WriteLine(sb.ToString());
		}
	}

	internal class EqualityComparerStateWithPrice : IEqualityComparer<State>
	{
		public bool Equals(State x, State y)
		{
			return x.Position == y.Position &&
				   x.DirectionIndex == y.DirectionIndex&&
				   x.Cost==y.Cost;
		}
		public int GetHashCode(State obj)
		{
			return HashCode.Combine(obj.Position, obj.DirectionIndex,obj.Cost);
		}
	}
}