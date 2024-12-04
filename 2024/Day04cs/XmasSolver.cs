using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04cs
{
	internal class XmasSolver
	{
		private List<string> _inputCol;

		private readonly string _strTemplate;
		private List<Tuple<int, int>> _gridTemplate;

		private List<List<Tuple<int, int>>> _directions = new List<List<Tuple<int, int>>>();
		private List<List<List<int>>> _rotationMatrixes90 = new List<List<List<int>>>()
		{
			new List<List<int>>()
			{
				new List<int>(){1, 0},
				new List<int>(){0, 1}
			},
			new List<List<int>>()
			{
				new List<int>(){0, 1},
				new List<int>(){-1, 0}
			},
			new List<List<int>>()
			{
				new List<int>(){-1, 0},
				new List<int>(){0, -1}
			},
			new List<List<int>>()
			{
				new List<int>(){0, -1},
				new List<int>(){1, 0}
			}
			};

		private List<List<List<int>>> _rotationMatrixes45 = new List<List<List<int>>>()
		{

			new List<List<int>>()
			{
				new List<int>(){1, 1},
				new List<int>(){-1, 1}
			},
			new List<List<int>>()
			{
				new List<int>(){-1, 1},
				new List<int>(){-1, -1}
			},
			new List<List<int>>()
			{
				new List<int>(){-1, -1},
				new List<int>(){1, -1}
			},
			new List<List<int>>()
			{
				new List<int>(){1, -1},
				new List<int>(){1, 1}
			},

		};


		private void GeneretaDirections(List<Tuple<int, int>> gridTemplate, List<List<List<int>>> rotationMatrixes)
		{
			foreach (var rotationMatrix in rotationMatrixes)
			{
				List<Tuple<int, int>> tempList = new List<Tuple<int, int>>();
				foreach (var gridItem in _gridTemplate)
				{
					tempList.Add(new Tuple<int, int>(gridItem.Item1 * rotationMatrix[0][0] + gridItem.Item2 * rotationMatrix[0][1], gridItem.Item1 * rotationMatrix[1][0] + gridItem.Item2 * rotationMatrix[1][1]));
				}
				_directions.Add(tempList);
			}
		}
		public XmasSolver(List<string> inputCol, List<Tuple<int, int>> gridTemplate, string strTemplate,bool rotation45,bool rotation90)
		{
			_inputCol = inputCol;
			_gridTemplate = gridTemplate;
			_strTemplate = strTemplate;
			_directions = new List<List<Tuple<int, int>>>();

			if (rotation90)
				GeneretaDirections(gridTemplate, _rotationMatrixes90);
			if (rotation45)
				GeneretaDirections(gridTemplate, _rotationMatrixes45);
		}

		public long GetAmount()
		{
			long sum = 0;
			for (int i = 0; i < _inputCol.Count; i++)
			{
				for (int j = 0; j < _inputCol[0].Length; j++)
				{
					sum += nOfXmaxFrom(i, j);
				}
			}

			return sum;
		}

		private char GetChar(int i, int j)
		{
			if (i < 0 || i >= _inputCol.Count || j < 0 || j >= _inputCol[0].Length)
			{
				return ' ';
			}
			return _inputCol[i][j];
		}

		private long nOfXmaxFrom(int x, int y)
		{
			bool allOk;
			long sum = 0;
			foreach (var direction in _directions)
			{
				allOk = true;
				for (int i = 0; i < direction.Count; i++)
				{
					if (GetChar(x + direction[i].Item1, y + direction[i].Item2) != _strTemplate[i])
					{
						allOk = false;
						break;
					}
				}
				if (allOk)
					sum++;
			}
			return sum;
		}
	}
}
