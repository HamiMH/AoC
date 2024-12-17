
using System.Text;

namespace Day15cs
{
	internal class MovementSimulator
	{
		private static readonly Dictionary<char, (int, int)> _directions = new Dictionary<char, (int, int)> { { '^', (-1, 0) }, { '>', (0, 1) }, { 'v', (1, 0) }, { '<', (0, -1) } };
		private List<List<int>> _layout;
		private (int, int) _robPosition;
		private string _moves;

		public MovementSimulator(List<string> inputCol, bool secPart)
		{
			_layout = new List<List<int>>();
			int i;
			for (i = 0; i < inputCol.Count; i++)
			{
				if (inputCol[i].Length == 0)
					break;
				List<int> row = new List<int>();
				for (int j = 0; j < inputCol[i].Length; j++)
				{
					switch (inputCol[i][j])
					{
						case '#':
							row.Add(2);
							if (secPart)
								row.Add(2);
							break;
						case 'O':
							row.Add(1);
							if (secPart)
								row.Add(-1);
							break;
						case '.':
							row.Add(0);
							if (secPart)
								row.Add(0);
							break;
						case '@':
							_robPosition = (i, 2 * j);
							row.Add(3);
							if (secPart)
								row.Add(0);
							break;
					}
				}
				_layout.Add(row);
			}
			i++;
			StringBuilder sb = new StringBuilder();
			for (; i < inputCol.Count; i++)
			{
				sb.Append(inputCol[i]);
			}
			_moves = sb.ToString();
		}

		public void Simulate(bool secPart)
		{
			foreach (var move in _moves)
			{
				if (secPart)
					TryMove2(_robPosition, _directions[move]);
				else
					TryMove(_robPosition, _directions[move]);
			}
		}

		public long GetScore()
		{
			long score = 0;
			for (int i = 0; i < _layout.Count; i++)
			{
				for (int j = 0; j < _layout[i].Count; j++)
				{
					if (_layout[i][j] == 1)
						score += (i * 100) + j;
				}
			}
			return score;
		}


		private bool TryMove((int, int) position, (int, int) direction)
		{
			(int, int) newPosition = (position.Item1 + direction.Item1, position.Item2 + direction.Item2);

			if (_layout[newPosition.Item1][newPosition.Item2] == 2)
			{
				return false;
			}
			if (_layout[newPosition.Item1][newPosition.Item2] == 1)
			{
				if (!TryMove(newPosition, direction))
					return false;
			}
			Move(position, direction);

			return true;
		}

		private void Move((int, int) position, (int, int) direction)
		{
			(int, int) newPosition = (position.Item1 + direction.Item1, position.Item2 + direction.Item2);
			int valOfPosition = _layout[position.Item1][position.Item2];
			_layout[position.Item1][position.Item2] = 0;
			_layout[newPosition.Item1][newPosition.Item2] = valOfPosition;
			if (valOfPosition == 3)
				_robPosition = newPosition;
		}

		private void TryMove2((int, int) position, (int, int) direction)
		{
			if (direction.Item2 != 0)
				TryLeftRightMove(position, direction);
			else
				TryUpDownMove(position, direction);
		}
		private bool TryLeftRightMove((int, int) position, (int, int) direction)
		{
			(int, int) newPosition = (position.Item1 + direction.Item1, position.Item2 + direction.Item2);

			if (_layout[newPosition.Item1][newPosition.Item2] == 2)
			{
				return false;
			}
			if (_layout[newPosition.Item1][newPosition.Item2] == 1 || _layout[newPosition.Item1][newPosition.Item2] == -1)
			{
				if (!TryLeftRightMove(newPosition, direction))
					return false;
			}
			Move(position, direction);

			return true;
		}

		private void TryUpDownMove((int, int) position, (int, int) direction)
		{
			(int, int) newPosition = (position.Item1 + direction.Item1, position.Item2 + direction.Item2);

			if (CanMoveUpDown(position, direction))
				MoveUpDown(position, direction);
		}

		private bool CanMoveUpDown((int, int) position, (int, int) direction)
		{
			(int, int) newPosition = (position.Item1 + direction.Item1, position.Item2 + direction.Item2);

			switch (_layout[newPosition.Item1][newPosition.Item2])
			{
				case 2:
					return false;
				case 1:
					return CanMoveUpDown(newPosition, direction) && CanMoveUpDown((newPosition.Item1, newPosition.Item2 + 1), direction);
				case -1:
					return CanMoveUpDown(newPosition, direction) && CanMoveUpDown((newPosition.Item1, newPosition.Item2 - 1), direction);
				default:
					return true;
			}
		}

		private void MoveUpDown((int, int) position, (int, int) direction)
		{
			(int, int) newPosition = (position.Item1 + direction.Item1, position.Item2 + direction.Item2);
			if (_layout[newPosition.Item1][newPosition.Item2] == 1)
			{
				MoveUpDown(newPosition, direction);
				MoveUpDown((newPosition.Item1, newPosition.Item2 + 1), direction);
			}
			if (_layout[newPosition.Item1][newPosition.Item2] == -1)
			{
				MoveUpDown(newPosition, direction);
				MoveUpDown((newPosition.Item1, newPosition.Item2 - 1), direction);
			}
			int valOfPosition = _layout[position.Item1][position.Item2];
			_layout[position.Item1][position.Item2] = 0;
			_layout[newPosition.Item1][newPosition.Item2] = valOfPosition;
			if (valOfPosition == 3)
				_robPosition = newPosition;
		}



		public void PrintLayout()
		{
			foreach (var row in _layout)
			{
				foreach (var cell in row)
				{
					switch (cell)
					{
						case 0:
							Console.Write('.');
							break;
						case 1:
							Console.Write('[');
							break;
						case -1:
							Console.Write(']');
							break;
						case 2:
							Console.Write('#');
							break;
						case 3:
							Console.Write('@');
							break;
					}
				}
				Console.WriteLine();
			}
		}
	}
}