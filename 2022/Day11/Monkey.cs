using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{



    internal class Monkey
    {
        public static List<Monkey> MonkeyList = new List<Monkey>();

        public static long Biggest2()
        {
            long big1 = 0;
            long big2 = 0;
            foreach (Monkey monkey in MonkeyList)
            {
                if (big1 > big2)
                {
                    if (monkey.NumberOfMoves > big2)
                        big2 = monkey.NumberOfMoves;
                }
                else
                {
                    if (monkey.NumberOfMoves > big1)
                        big1 = monkey.NumberOfMoves;
                }
            }
            return big1 * big2;
        }


        public Queue<long> Items;

        public Func<long, long> Operation;

        public long TestValue;

        public int TrueTarget;
        public int FalseTarget;

        public long NumberOfMoves = 0;

        private long? _module=null;
        private long ModuleValue
        {
            get
            {
                if (_module == null)
                {
                    _module = 1;
                    foreach (Monkey monkey in MonkeyList)
                        _module *= monkey.TestValue;
                    return (long)_module;
                }
                else { return (long)_module; }
            }
        }

        public Monkey(string inputStr)
        {
            Items = new Queue<long>();
            string[] strArr = inputStr.Split(";");
            string[] strArr1 = strArr[1].Replace("Starting items:", "").Trim().Split(",");
            foreach (string str in strArr1)
                Items.Enqueue(long.Parse(str.Trim()));

            string strTmp = strArr[2].Replace("Operation: new = old", "").Trim();
            char oper = strTmp[0];
            if (strTmp.Substring(2) == "old")
            {
                if (oper == '+')
                {
                    Operation = (x => x + x);
                }
                else
                {
                    Operation = (x => x * x);
                }
            }
            else
            {
                long val = long.Parse(strTmp.Substring(2));
                if (oper == '+')
                {
                    Operation = (x => x + val);
                }
                else
                {
                    Operation = (x => x * val);
                }
            }

            strTmp = strArr[3].Replace("Test: divisible by", "").Trim();
            TestValue = long.Parse(strTmp);

            strTmp = strArr[4].Replace("If true: throw to monkey", "").Trim();
            TrueTarget = int.Parse(strTmp);

            strTmp = strArr[5].Replace("If false: throw to monkey", "").Trim();
            FalseTarget = int.Parse(strTmp);
        }


        public void PlayWithItems()
        {
            long item;
            while (Items.Any())
            {
                item = Items.Dequeue();
                item = Operation(item);
                item = Mod(item);
                if (item % TestValue == 0)
                    MonkeyList[TrueTarget].Items.Enqueue(item);
                else
                    MonkeyList[FalseTarget].Items.Enqueue(item);
                NumberOfMoves++;
            }

        }

        private long Div3(long item)
        {
            long val = item / 3;
            //if (item % 3 == 2)
            //    val++;
            return val;
        }

        private long Mod(long item)
        {
            return item% ModuleValue;
        }
    }
}
