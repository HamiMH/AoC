namespace Day2
{
    public class Rule1:IRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char LimitedChar { get; set; }
        int IRule.Char1 { get => Min; set => Min=value; }
        int IRule.Char2 { get => Max; set => Max = value; }
        char IRule.ImportantChar { get => LimitedChar; set => LimitedChar = value; }

        public Rule1(int min, int max, char limitOfChar)
        {
            this.Min = min;
            this.Max = max;
            LimitedChar = limitOfChar;
        }

        public bool StringFulfilsRule(string inputString)
        {
            int count = 0;
            foreach(char c in inputString)
            {
                if (c==LimitedChar)
                    count++;
            }
            if(count>=Min && count <=Max)
                return true;
            else
                return false;
        }
    }
}