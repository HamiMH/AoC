using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09cs
{
	class DiskMap
	{
		private List<int> _diskList;
		private List<int> _diskFilling;
		private int _diskFillingLen;
		public DiskMap(List<string> inputCol)
		{

			_diskList = inputCol.First().ToCharArray().Select(c => (c - '0' )).ToList();
			_diskFilling = new List<int>(_diskList.Sum());

			bool file = true;
			int index = 0;
			foreach (int len in _diskList)
			{
				int tmp;
				if (file)
				{
					tmp = index;
					index++;
					file = false;
				}
				else
				{
					tmp = -1;
					file = true;
				}
				_diskFilling.AddRange(Enumerable.Repeat(tmp, len));
			}
			_diskFillingLen = _diskFilling.Count;
		}

		internal long CheckSum()
		{
			long sum = 0;
			for (int i = 0; i < _diskFillingLen; i++)
			{
				sum+=_diskFilling[i] * i;
			}
			return sum;
		}

		internal void RunCompression()
		{
			int frontInd = 0;
			int backInd = _diskFillingLen-1;
			int front, back;

			while(frontInd<backInd)
			{
				front= _diskFilling[frontInd];
				back = _diskFilling[backInd];
				if (front>=0)
				{
					frontInd++;
					continue;
				}
				if (back<0)
				{
					backInd--;
					continue;
				}
				_diskFilling[frontInd] = back;
				_diskFilling[backInd] = -1;
				frontInd++;
				backInd--;
			}
			_diskFillingLen = backInd + 1;
		}
	}
}
