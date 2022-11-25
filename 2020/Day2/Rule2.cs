namespace Day2
{
    public class Rule2:IRule
    {
        public int First { get; set; }
        public int Second { get; set; }
        public char MandatoryChar { get; set; }
        int IRule.Char1 { get => First; set => First = value; }
        int IRule.Char2 { get => Second; set => Second = value; }
        char IRule.ImportantChar { get => MandatoryChar; set => MandatoryChar = value; }

        public Rule2(int first,int second,char mandatoryChar)
        {
            First = first;
            Second = second;
            MandatoryChar = mandatoryChar;
        }


        public bool StringFulfilsRule(string inputString)
        {
            if (inputString[First - 1] == MandatoryChar && inputString[Second - 1] != MandatoryChar)
                return true;
            else if (inputString[First - 1] != MandatoryChar && inputString[Second - 1] == MandatoryChar)
                return true;
            else
                return false;
        }
    }
}