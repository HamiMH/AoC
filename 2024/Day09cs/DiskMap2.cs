using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09cs
{

	class DiskItem
	{
		public int Index { get; set; }
		public int Size { get; set; }
	}

	class DiskMap2
	{
		private List<int> _diskList;
		private LinkedList<DiskItem> _diskFilling;
		private int _diskFillingLen;
		public DiskMap2(List<string> inputCol)
		{

			_diskList = inputCol.First().ToCharArray().Select(c => (c - '0')).ToList();
			_diskFilling = new LinkedList<DiskItem>();

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
				_diskFilling.AddLast(new DiskItem() { Index = tmp, Size = len });
			}
			_diskFillingLen = _diskFilling.Count;
		}

		internal long CheckSum()
		{
			long sum = 0;
			long index = 0;
			foreach (DiskItem item in _diskFilling)
			{
				for (int i = 0; i < item.Size; i++)
				{
					if (item.Index >= 0)
						sum += item.Index * index;
					index++;
				}
			}
			return sum;
		}

		internal void RunCompression()
		{
			int frontInd = 0;
			int backInd = _diskFillingLen - 1;
			LinkedListNode<DiskItem> back = _diskFilling.Last;

			PrintLL(_diskFilling);
			while (back != _diskFilling.First)
			{
				if (back.Value.Index < 0)
				{
					back = back.Previous;
					continue;
				}

				int backSize = back.Value.Size;
				LinkedListNode<DiskItem> front = _diskFilling.First;
				while (true)
				{
					if (front == back)
					{
						back = back.Previous;
						break;
					}
					if (front.Value.Index >= 0)
					{
						front = front.Next;
						continue;
					}
					int frontSize = front.Value.Size;
					if (frontSize < backSize)
					{
						front = front.Next;
						continue;
					}
					DiskItem newItem = new DiskItem() { Index = back.Value.Index, Size = backSize };
					DiskItem newEmpty = new DiskItem() { Index = -1, Size = frontSize - backSize };

					_diskFilling.AddAfter(front, newItem);
					if (newEmpty.Size > 0)
						_diskFilling.AddAfter(front.Next, newEmpty);
					_diskFilling.Remove(front);

					back.Value.Index = -1;
					while (back.Previous.Value.Index < 0)
					{
						back.Value.Size += back.Previous.Value.Size;
						_diskFilling.Remove(back.Previous);
					}
					while (back.Next != null && back.Next.Value.Index < 0)
					{
						back.Value.Size += back.Next.Value.Size;
						_diskFilling.Remove(back.Next);
					}
					LinkedListNode<DiskItem> newBack = back.Previous;
					//_diskFilling.Remove(back);   upraviot
					back = newBack;
					break;
				}
				PrintLL(_diskFilling);
			}
		}

		private void PrintLL(LinkedList<DiskItem> diskFilling)
		{
			//foreach (DiskItem item in diskFilling)
			//{
			//	Console.Write($"({item.Index,2}, {item.Size,2})  ");
			//}
			//Console.WriteLine();
		}
	}
}
