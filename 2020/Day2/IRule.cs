using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    public interface IRule
    {
        int Char1 { get; set; }
        int Char2 { get; set; }
        char ImportantChar{ get; set; }

        public bool StringFulfilsRule(string inputString);
    }
}
