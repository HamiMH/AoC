
namespace _001_MyDFT
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DiscreteFourier df = new DiscreteFourier();
			List<double> inVals = new() { 1,1,1,1,1,1 };
			List<double> inVals2 = new() { 0,1,0,1,0,1 };
			List<double> inVals3 = new() { -1,1,-1,1,-1,1 };
			
			PrintList(df.DFT(inVals));
			PrintList(df.DFT(inVals2));
			PrintList(df.DFT(inVals3));
		}

		private static void PrintList(List<double> outVals)
		{
			bool first = true;
			foreach (double val in outVals)
			{
				if (!first)
					Console.Write(",");
				else
					first = false;
				Console.Write(val);
			}
			Console.WriteLine();
		}
	}
}
