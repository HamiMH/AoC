namespace Day13cs
{
	internal struct ButtonCalculator
	{
		public ButtonCalculator((long, long) value1, (long, long) value2, (long, long) value3)
		{
			Button1 = value1;
			Button2 = value2;
			FinalDest = value3;
		}

		public (long, long) Button1 { get; }
		public (long, long) Button2 { get; }
		public (long, long) FinalDest { get; }

		private (long,long) TurnBy90((long,long) input)=> (input.Item2, -input.Item1);
		private long InnerProduct((long, long) input1, (long, long) input2) => input1.Item1 * input2.Item1 + input1.Item2 * input2.Item2;
		public long GetPrice()
		{
			(long, long) turnedButton1 = TurnBy90(Button1);
			(long, long) turnedButton2 = TurnBy90(Button2);

			long final_turnButt1 = InnerProduct(FinalDest, turnedButton1);
			long final_turnButt2 = InnerProduct(FinalDest, turnedButton2);
			long butt1_TurnButt2 = InnerProduct(Button1, turnedButton2);
			long butt2_TurnButt1 = InnerProduct(Button2, turnedButton1);

			if(final_turnButt1%butt2_TurnButt1==0 && final_turnButt2 % butt1_TurnButt2 == 0)
			{
				return 1 * final_turnButt1 / butt2_TurnButt1 + 3 * final_turnButt2 / butt1_TurnButt2;
			}
			return 0;
		}
	}
}