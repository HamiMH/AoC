
using System.Collections.Generic;

namespace Day17cs
{
	internal class ComputerSimulator
	{
		static readonly int A=4;
		static readonly int B =5;
		static readonly int C =6;
		private List<long> RegisterCombo= new List<long>() { 0,1,2,3,-1,-1,-1,-1};
		private List<long> RegisterLiteral = new List<long>() { 0,1,2,3,4,5,6,7};
		public List<Action<int>> Operations = new List<Action<int>>();
		public List<int> OutpuList = new List<int>();	

		private List<int> Program;
		private int Pointer = 0;

		public ComputerSimulator(List<string> inputCol)
		{
			RegisterCombo[4] = long.Parse(inputCol[0].Split(": ").Last());
			RegisterCombo[5] = long.Parse(inputCol[1].Split(": ").Last());
			RegisterCombo[6] = long.Parse(inputCol[2].Split(": ").Last());
			string program= inputCol[4].Split(": ").Last();
			Program = program.Split(",").Select(int.Parse).ToList();
			Operations.Add(Adv);
			Operations.Add(Bxl);
			Operations.Add(Bst);
			Operations.Add(Jnz);
			Operations.Add(Bxc);
			Operations.Add(Out);
			Operations.Add(Bdv);
			Operations.Add(Cdv);

		}
		public List<int> Run()
		{
			Pointer = 0;
			while (Pointer < Program.Count)
			{
				int op = Program[Pointer];
				int combo = Program[Pointer + 1];
				Operations[op](combo);
				Pointer += 2;
			}
			return OutpuList;
		}

		long GCD(long a,long b)
		{
			if(a==-1)
				return b;
			while (b != 0)
			{
				long temp = b;
				b = a % b;
				a = temp;
			}
			return a;

		}

		internal string FindCorrectADetermin()
		{
			List<long> backUpCombo = new List<long>(RegisterCombo);
			List<long> results=new List<long>() {  };
			List<long> difs=new List<long>() {  };
			long gcd=-1;
			int index = 1;
			long diff = 1;
			long newA = 19878166004L;
			while(true)
			{
				RegisterCombo = new List<long>(backUpCombo);
				RegisterCombo[A] = newA;
				OutpuList = new List<int>();
				Run();
				if (ListAreIdenticalDeter(Program, OutpuList,newA,ref diff,ref index,results, difs,ref gcd)) 
				{
					return newA.ToString();
				}
				newA+= diff;
				if(newA>long.MaxValue/2)
					throw new Exception("No solution found");
			}
		}

		private bool ListAreIdenticalDeter(List<int> program, List<int> outpuList, long newA, ref long diff, ref int index, List<long> results, List<long> difs, ref long gcd)
		{
			long lowerCount = Math.Min(program.Count, outpuList.Count);
			for (int i = 0; i < lowerCount; i++)
			{
				if (program[i] != outpuList[i])
				{
					return false;
				}
				if (i != index)
					continue;

				if (results.Count > 0)
				{
					difs.Add(newA - results.Last());
					gcd = GCD(gcd, newA - results.Last());
				}
				results.Add(newA);
				if(results.Count>30)
				{
					results.Clear();
					difs.Clear();
					diff = gcd;
					gcd = -1;
					index++;
				}
			}
			if (program.Count != outpuList.Count)
			{
				return false;
			}
			return true;
		}



		internal string FindCorrectA()
		{
			List<long> backUpCombo = new List<long>(RegisterCombo);
			List<List<long>> results = new List<List<long>>() { };
			List<List<long>> difs = new List<List<long>>() { };
			List<long> gcd = new List<long>() { };
			foreach (long i in Program)
			{
				results.Add(new List<long>());
				difs.Add(new List<long>());
				gcd.Add(-1);
			}
			long newA = 19878166004L;
			while (true)
			{
				RegisterCombo = new List<long>(backUpCombo);
				RegisterCombo[A] = newA;
				OutpuList = new List<int>();
				Run();
				if (ListAreIdentical(Program, OutpuList, newA, results, difs, gcd))
				{
					return newA.ToString();
				}
				newA += 536870912L;
			}
		}

		private bool ListAreIdentical(List<int> program, List<int> outpuList,long newA, List<List<long>> results,List<List<long>> difs, List<long> gcd)
		{
			long lowerCount = Math.Min(program.Count, outpuList.Count);
			for (int i = 0; i < lowerCount; i++)
			{
				if (program[i] != outpuList[i])
				{
					return false;
				}
				if (results[i].Count > 0)
				{
					difs[i].Add(newA - results[i].Last());
					gcd[i] = GCD(gcd[i], newA - results[i].Last());
				}
				results[i].Add(newA);
			}
			if (program.Count != outpuList.Count)
			{
				return false;
			}
			return true;
		}

		public void Adv(int combo)
		{
			int exp =(int) RegisterCombo[combo];
			long val=1L<<exp;
			RegisterCombo[A]= RegisterCombo[A] /val;
		}
		
		public void Bxl(int literal)
		{
			RegisterCombo[B]= RegisterCombo[B] ^ literal;
		}
		public void Bst(int combo)
		{
			RegisterCombo[B] = RegisterCombo[combo] %8;
		} 
		public void Jnz(int literal)
		{
			if (RegisterCombo[A] != 0)
			{
				Pointer = (int)(RegisterCombo[literal] -2);
			}
		} 
		public void Bxc(int literal)
		{
			RegisterCombo[B] = RegisterCombo[B] ^ RegisterCombo[C];
		} 
		public void Out(int combo)
		{
			OutpuList.Add((int)(RegisterCombo[combo] % 8));
		} 
		public void Bdv(int combo)
		{
			int exp = (int)RegisterCombo[combo];
			long val = 1 << exp;
			RegisterCombo[B] = RegisterCombo[A] / val;
		} 
		public void Cdv(int combo)
		{
			int exp = (int)RegisterCombo[combo];
			long val = 1 << exp;
			RegisterCombo[C] = RegisterCombo[A] / val;
		}

	}
}