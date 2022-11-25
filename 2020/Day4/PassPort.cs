using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public class PassPort
    {
        public string? Ecl { get; set; } = null;
        public string? Pid { get; set; } = null;
        public string? Eyr { get; set; } = null;
        public string? Hcl { get; set; } = null;
        public string? Byr { get; set; } = null;
        public string? Iyr { get; set; } = null;
        public string? Cid { get; set; } = null;
        public string? Hgt { get; set; } = null;

        public PassPort(string? ecl, string? pid, string? eyr, string? hcl, string? byr, string? iyr, string? cid, string? hgt, IValidator validator)
        {
            Ecl = ecl;
            Pid = pid;
            Eyr = eyr;
            Hcl = hcl;
            Byr = byr;
            Iyr = iyr;
            Cid = cid;
            Hgt = hgt;
            Validator = validator;
        }

        public bool PassPortIsValid()
        {
            return Validator.IsValid(this);
        }
        public PassPort( IValidator validator)
        {
            this.Validator = validator;
        }

        public IValidator Validator { get; set; }

        private void SetPar(string param, string value)
        {
            switch (param.ToLower())
            {
                case "ecl":
                    Ecl = value;
                    break;
                case "pid":
                    Pid = value;
                    break;
                case "eyr":
                    Eyr = value;
                    break;
                case "hcl":
                    Hcl = value;
                    break;
                case "byr":
                    Byr = value;
                    break;
                case "iyr":
                    Iyr = value;
                    break;
                case "cid":
                    Cid = value;
                    break;
                case "hgt":
                    Hgt = value;
                    break;
                default:
                    throw new Exception("Fatal Error");
            }
        }


        private string? GetPar(string param)
        {
            switch (param.ToLower())
            {
                case "ecl":
                    return Ecl;
                case "pid":
                    return Pid;
                case "eyr":
                    return Eyr;
                case "hcl":
                    return Hcl;
                case "byr":
                    return Byr;
                case "iyr":
                    return Iyr;
                case "cid":
                    return Cid;
                case "hgt":
                    return Hgt;
                default:
                    throw new Exception("Fatal Error");
            }
        }

        public void AddData(string inputStr)
        {
            string[] inputArr = inputStr.Split(" ");
            string[] partArr;
            foreach(string part in inputArr)
            {
                partArr = part.Split(":");
                SetPar(partArr[0], partArr[1]); 
            }
        }
    }
}
