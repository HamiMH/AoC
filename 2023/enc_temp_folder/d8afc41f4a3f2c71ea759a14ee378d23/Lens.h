#pragma once
#include <string>
void StringWithSymbolIntoStrings(std::string str, std::vector<std::string>& outVec, std::string symbols)
{
    boost::trim(str);
    std::vector<std::string>tmpVec;
    boost::split(tmpVec, str, boost::is_any_of(symbols));

    for (std::string& s : tmpVec)
    {
        boost::trim(s);
        if (s == "")
            continue;
        outVec.push_back((s));
    }
}
long long HashOfStr(std::string s)
{
    long long hash = 0;

    for (char c : s)
    {
        hash += c;
        hash *= 17;
        hash %= 256;
    }
    return hash;
}

class Lens
{
public :
    std::string Label;
    char Symbol;
    long long Value;
    long long LabelValue;

    Lens(std::string& str)
    {
        if (str.contains('-'))
        {
            Label = str.substr(str.size() - 1);
            Symbol = '-';
            Value = -1;
            LabelValue = HashOfStr(Label);
        }
        else
        {
            std::vector<std::string>tmp;
            StringWithSymbolIntoStrings(str, tmp, "=");

            Label = tmp.front();
            Symbol = '=';
            Value = stoll(tmp.back());
            LabelValue = HashOfStr(Label);
        }
    }
};