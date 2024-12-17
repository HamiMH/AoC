using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08cs
{
	public class AntinodeCalculator
	{
		Tuple<int, int> _areaSize = new Tuple<int, int>(0, 0);
		private Dictionary<char, List<Tuple<int, int>>> _antenaDictionary = new Dictionary<char, List<Tuple<int, int>>>();
		public AntinodeCalculator(List<string> inputCol)
		{
			_areaSize = new Tuple<int, int>(inputCol.First().Length,inputCol.Count);

			char c;
			for (int i = 0; i < inputCol.Count; i++)
			{
				for (int j = 0; j < inputCol[i].Length; j++)
				{
					c = inputCol[i][j];
					if (c!= '.')
					{
						if (!_antenaDictionary.ContainsKey(c))
						{
							_antenaDictionary.Add(c, new List<Tuple<int, int>>());
						}
						_antenaDictionary[c].Add(new Tuple<int, int>(j, i));
					}
				}
			}
		}

		public int GetNumberOfAntipoles()
		{

			return GetAllAntipoles().Count;
		} 
		public int GetNumberOfAntipoles2()
		{

			return GetAllAntipoles2().Count;
		}

		private bool AddAntipole(Tuple<int, int> antipole, HashSet<Tuple<int, int>> antipoles)
		{
			if (antipole.Item1 >= 0 && antipole.Item1 < _areaSize.Item1 && antipole.Item2 >= 0 && antipole.Item2 < _areaSize.Item2)
			{
				antipoles.Add(antipole);
				return true;
			}
			return false;
		}
		private HashSet<Tuple<int,int>> GetAllAntipoles()
		{
			HashSet<Tuple<int, int>> antipoles = new HashSet<Tuple<int, int>>();
			foreach (KeyValuePair<char, List<Tuple<int, int>>> keyValuePair in _antenaDictionary)
			{
				List<Tuple<int, int>> antenas = keyValuePair.Value;
				for (int i = 0; i < antenas.Count; i++)
				{
					for (int j = i + 1; j < antenas.Count; j++)
					{
						Tuple<int, int> first  = antenas[i];
						Tuple<int, int> second = antenas[j];
						Tuple<int, int> differ = Tuple.Create(first.Item1 - second.Item1, first.Item2 - second.Item2);
						Tuple<int, int> antipole1 = Tuple.Create(first.Item1 + differ.Item1, first.Item2 + differ.Item2);
						Tuple<int, int> antipole2 = Tuple.Create(second.Item1 - differ.Item1, second.Item2 - differ.Item2);
						AddAntipole(antipole1, antipoles);
						AddAntipole(antipole2, antipoles);					}
				}
			}
			return antipoles;
		} 
		
		private HashSet<Tuple<int,int>> GetAllAntipoles2()
		{
			HashSet<Tuple<int, int>> antipoles = new HashSet<Tuple<int, int>>();
			foreach (KeyValuePair<char, List<Tuple<int, int>>> keyValuePair in _antenaDictionary)
			{
				List<Tuple<int, int>> antenas = keyValuePair.Value;
				for (int i = 0; i < antenas.Count; i++)
				{
					for (int j = i + 1; j < antenas.Count; j++)
					{
						Tuple<int, int> first  = antenas[i];
						Tuple<int, int> second = antenas[j];
						Tuple<int, int> differ = Tuple.Create(first.Item1 - second.Item1, first.Item2 - second.Item2);

						for (int k = 0; ; k++)
						{
							Tuple<int, int> antipole = Tuple.Create(first.Item1 + k*differ.Item1, first.Item2 + k*differ.Item2);
							if (!AddAntipole(antipole, antipoles))
							{
								break;
							}
						} 
						for (int k = 0; ; k--)
						{
							Tuple<int, int> antipole = Tuple.Create(first.Item1 + k*differ.Item1, first.Item2 + k*differ.Item2);
							if (!AddAntipole(antipole, antipoles))
							{
								break;
							}
						}
					}
				}
			}
			return antipoles;
		}
	}
}
