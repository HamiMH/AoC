using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public class Validator1: IValidator
    {
        
        public bool IsValid(PassPort passPort)
        {
            if(
                passPort.Ecl == null||
            passPort.Pid == null ||
            passPort.Eyr == null ||
            passPort.Hcl == null ||
            passPort.Byr == null ||
            passPort.Iyr == null ||
            passPort.Hgt == null 
                )
                return false;
            else
                return true;
        }
    }
}
