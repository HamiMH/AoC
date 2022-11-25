using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    public abstract class CommBase
    {
        public int Value { get; }
        public bool Attended { get; set; }
        public CommBase(int value)
        {
            Value = value;
            Attended = false;
        }
        public abstract bool ExecComm(SimState simState);
        public abstract CommBase Swap();
        public abstract bool FastSwap(ref CommBase commBase);
        public abstract bool Swappable();

        public static CommBase Create(string inpStr)
        {
            string[] inpArr = inpStr.Split(" ");
            int val = Convert.ToInt32(inpArr[1]);
            switch (inpArr[0].Trim())
            {
                case "nop":
                    return new CommNop(val);
                    break;
                case "acc":
                    return new CommAcc(val);
                    break;
                case "jmp":
                    return new CommJmp(val);
                    break;
                default: throw new Exception("Fatal error.");
            }
        }
    }

    internal class CommJmp : CommBase
    {
        public CommJmp(int value) : base(value)
        {
        }

        public override bool ExecComm(SimState simState)
        {
            if (Attended)
                return false;
            simState.Position += Value;
            Attended = true;
            return true;
        }

        public override bool FastSwap(ref CommBase commBase)
        {
            commBase = new CommNop(commBase.Value);
            return true;
        }

        public override CommBase Swap()
        {
            return new CommNop(Value);
        }

        public override bool Swappable()
        {
            return true;
        }
    }

    internal class CommAcc : CommBase
    {
        public CommAcc(int value) : base(value)
        {
        }
        public override bool ExecComm(SimState simState)
        {
            if (Attended)
                return false;
            simState.Position++;
            simState.Accum += Value;
            Attended = true;
            return true;
        }

        public override bool FastSwap(ref CommBase commBase)
        {
            return false;
        }
        public override CommBase Swap()
        {
            throw new Exception("Error swap");
        }

        public override bool Swappable()
        {
            return false;
        }
    }

    internal class CommNop : CommBase
    {
        public CommNop(int value) : base(value)
        {
        }
        public override bool ExecComm(SimState simState)
        {
            if (Attended)
                return false;
            simState.Position++;
            Attended = true;
            return true;
        }
        public override bool FastSwap(ref CommBase commBase)
        {
            commBase = new CommJmp(commBase.Value);
            return true;
        }
        public override CommBase Swap()
        {
            return new CommJmp(Value);
        }

        public override bool Swappable()
        {
            return true;
        }
    }
}
