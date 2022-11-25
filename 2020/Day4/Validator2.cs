
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public class Validator2 : IValidator
    {

        public bool IsValid(PassPort passPort)
        {
            bool valid = true;

            int byrInt;
            bool byrOk = int.TryParse(passPort.Byr, out byrInt);
            valid &= byrOk;
            valid &= (1920 <= byrInt && byrInt <= 2002);

            int iyrInt;
            bool iyrOk = int.TryParse(passPort.Iyr, out iyrInt);
            valid &= iyrOk;
            valid &= (2010 <= iyrInt && iyrInt <= 2020);

            int eyrInt;
            bool eyrOk = int.TryParse(passPort.Eyr, out eyrInt);
            valid &= eyrOk;
            valid &= (2020 <= eyrInt && eyrInt <= 2030);

            valid &= TestHgt(passPort.Hgt);
            valid &= TestHcl(passPort.Hcl);
            valid &= TestEcl(passPort.Ecl);
            valid &= TestPid(passPort.Pid);
          
            return valid;
        }

        private bool TestPid(string? pid)
        {

            if (pid == null || pid.Length != 9 )
                return false;
            for (int i = 0; i < 9; i++)
            {
                char c = pid[i];
                if ('0' <= c && c <= '9')
                    continue;
                return false;
            }
            return true;
        }

        private bool TestEcl(string? ecl)
        {

            if (ecl == null )
                return false;
            if (ecl == "amb" || ecl == "blu" || ecl == "brn" ||
                ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth")
                return true;
            else
                return false;                    
        }

        private bool TestHcl(string? hcl)
        {

            if (hcl == null || hcl.Length!=7 || hcl[0]!='#')
                return false;
            for(int i = 1; i < 7; i++)
            {
                char c = hcl[i];
                if ('0' <= c && c <= '9')
                    continue;
                if ('a' <= c && c <= 'f')
                    continue;
                return false;
            }
            return true;
        }

        private bool TestHgt(string? hgt)
        {
            if(hgt==null)
                return false;

            if (hgt.Contains("in"))
            {
                hgt = hgt.Replace("in", "");

                int hgtInt;
                bool hgtOk = int.TryParse(hgt, out hgtInt);
                if (!hgtOk)
                    return false;
                if ((59 <= hgtInt && hgtInt <= 76))
                    return true;
                else
                    return false;
            }
            else if (hgt.Contains("cm"))
            {
                hgt = hgt.Replace("cm", "");

                int hgtInt;
                bool hgtOk = int.TryParse(hgt, out hgtInt);
                if (!hgtOk)
                    return false;
                if ((150 <= hgtInt && hgtInt <= 193))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
