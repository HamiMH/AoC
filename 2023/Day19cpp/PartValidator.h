#pragma once
#include <string>
#include <vector>
#include <boost/algorithm/string.hpp>
#include "Part.h"


class PartValidator
{
public:
    std::vector<std::function<std::string(Part)>> Validators;

    std::string GetNextValidator(Part part)
    {
        std::string outStr;
        for (std::function<std::string(Part)>& validator : Validators)
        {
            outStr = validator(part);
            if (outStr != "")
                return outStr;
        }
    }
    std::string Name;

    PartValidator()
    {
        Name = "";
    }
    PartValidator(std::string st)
    {
        std::vector<std::string>spl = StringWithSymbolIntoStrings(st, "{");
        Name = spl[0];
        std::string str = spl[1].substr(0, spl[1].size() - 1);
        std::vector<std::string>parts = StringWithSymbolIntoStrings(str, ",");

        for (std::string& part : parts)
        {
            std::function<std::string(Part)> fce;
            if (part.contains(':'))
            {
                std::vector<std::string>innerPart = StringWithSymbolIntoStrings(part, ":");
                char vr = innerPart[0][0];
                char rel = innerPart[0][1];
                int val = stoi(innerPart[0].substr(2, innerPart[0].size() - 2));
                std::string tmp = innerPart[1];
                std::string empty = "";
                if (rel == '<')
                    switch (vr)
                    {
                    case 'x':
                        fce = [val, tmp, empty](Part p) {if (p.x < val)return tmp; else return empty; };
                        break;
                    case 'm':
                        fce = [val, tmp, empty](Part p) {if (p.m < val)return tmp; else return empty; };
                        break;
                    case 'a':
                        fce = [val, tmp, empty](Part p) {if (p.a < val)return tmp; else return empty; };
                        break;
                    case 's':
                        fce = [val, tmp, empty](Part p) {if (p.s < val)return tmp; else return empty; };
                        break;
                    }
                else
                    switch (vr)
                    {
                    case 'x':
                        fce = [val, tmp, empty](Part p) {if (p.x > val)return tmp; else return empty; };
                        break;
                    case 'm':
                        fce = [val, tmp, empty](Part p) {if (p.m > val)return tmp; else return empty; };
                        break;
                    case 'a':
                        fce = [val, tmp, empty](Part p) {if (p.a > val)return tmp; else return empty; };
                        break;
                    case 's':
                        fce = [val, tmp, empty](Part p) {if (p.s > val)return tmp; else return empty; };
                        break;
                    }
            }
            else
            {
                fce = [part](Part p) {return part; };
            }
            Validators.push_back(fce);
        }

    }
};

class CubeValidatorItem
{
public:
    char charac;
    int value;
    char symbol;
    std::string outVal;
    CubeValidatorItem(char inCharac, int inValue, char inSymbol, std::string inOutVal)
    {
        charac = inCharac;
        value = inValue;
        symbol = inSymbol;
        outVal = inOutVal;
    }
};

class CubeValidator
{
public:
    std::vector<CubeValidatorItem> Validators;

    void ResolveValidation(Cube cube, std::unordered_map<std::string, std::deque<Cube>>& que)
    {
        for (CubeValidatorItem& validator : Validators)
        {
            if (cube.empty)break;
            Cube accPart;
            Cube rejPart;
            //outStr = validator(part);if(rel=='<')
            if (validator.symbol == '<')
                switch (validator.charac)
                {
                case 'x':
                    accPart = Cube({ cube.x.first,validator.value - 1 }, cube.m, cube.a, cube.s);
                    rejPart = Cube({ validator.value, cube.x.second }, cube.m, cube.a, cube.s);
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                case 'm':
                    accPart = Cube(cube.x, { cube.m.first,validator.value - 1 }, cube.a, cube.s);
                    rejPart = Cube(cube.x, { validator.value, cube.m.second }, cube.a, cube.s);
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                case 'a':
                    accPart = Cube(cube.x, cube.m, { cube.a.first,validator.value - 1 }, cube.s);
                    rejPart = Cube(cube.x, cube.m, { validator.value, cube.a.second }, cube.s);
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                case 's':
                    accPart = Cube(cube.x, cube.m, cube.a, { cube.s.first,validator.value - 1 });
                    rejPart = Cube(cube.x, cube.m, cube.a, { validator.value, cube.s.second });
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                }
            else if (validator.symbol == '>')
                switch (validator.charac)
                {
                case 'x':
                    rejPart = Cube({ cube.x.first,validator.value }, cube.m, cube.a, cube.s);
                    accPart = Cube({ validator.value + 1, cube.x.second }, cube.m, cube.a, cube.s);
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                case 'm':
                    rejPart = Cube(cube.x, { cube.m.first,validator.value }, cube.a, cube.s);
                    accPart = Cube(cube.x, { validator.value + 1, cube.m.second }, cube.a, cube.s);
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                case 'a':
                    rejPart = Cube(cube.x, cube.m, { cube.a.first,validator.value }, cube.s);
                    accPart = Cube(cube.x, cube.m, { validator.value + 1, cube.a.second }, cube.s);
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                case 's':
                    rejPart = Cube(cube.x, cube.m, cube.a, { cube.s.first,validator.value });
                    accPart = Cube(cube.x, cube.m, cube.a, { validator.value + 1, cube.s.second });
                    que[validator.outVal].push_back(accPart);
                    cube = rejPart;
                    break;
                }
            else
                que[validator.outVal].push_back(cube);

        }
    }



    std::string Name;

    CubeValidator()
    {
        Name = "";
    }
    CubeValidator(std::string st)
    {
        std::vector<std::string>spl = StringWithSymbolIntoStrings(st, "{");
        Name = spl[0];
        std::string str = spl[1].substr(0, spl[1].size() - 1);
        std::vector<std::string>parts = StringWithSymbolIntoStrings(str, ",");
        for (std::string& part : parts)
        {

            if (part.contains(':'))
            {
                std::vector<std::string>innerPart = StringWithSymbolIntoStrings(part, ":");
                char vr = innerPart[0][0];
                char rel = innerPart[0][1];
                int val = stoi(innerPart[0].substr(2, innerPart[0].size() - 2));
                std::string tmp = innerPart[1];
                std::string empty = "";

                Validators.push_back(CubeValidatorItem(vr, val, rel, tmp));
            }
            else
            {
                Validators.push_back(CubeValidatorItem('.', 0, '.', part));
            }
        }

    }
};