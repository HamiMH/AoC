using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    public class DataLine
    {

        public IRule Rule { get; set; }
        public string Value { get; set; }

        public DataLine(IRule rule, string value)
        {
            Rule = rule;
            Value = value;
        }

        public bool LineIsValid()
        {
            return Rule.StringFulfilsRule(Value);
        }
    }
}
