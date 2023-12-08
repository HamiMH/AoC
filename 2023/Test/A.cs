using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public abstract class A
    {
        public int X { get; set; }

        public abstract void SetX();
        public A()
        {
            SetX();
        }
    }
    public class B : A
    {
        public override void SetX()
        {
            X = 5;
        }
    }
}
