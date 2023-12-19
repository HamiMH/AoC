#pragma once
#include <string>
#include <vector>
#include <boost/algorithm/string.hpp>


std::vector<std::string>  StringWithSymbolIntoStrings(std::string str, std::string symbols)
{
    std::vector<std::string> outVec;
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
    return outVec;
}

class Part
{
public:
    int x;
    int m;
    int a;
    int s;

    Part()
    {
        x = 0;
    }

    Part(std::string st)
    {
        std::string str = st.substr(1, st.size() - 2);
        std::vector<std::string>parts = StringWithSymbolIntoStrings(str, ",");
        
        x=stoi( StringWithSymbolIntoStrings(parts[0], "=")[1]);
        m=stoi( StringWithSymbolIntoStrings(parts[1], "=")[1]);
        a=stoi( StringWithSymbolIntoStrings(parts[2], "=")[1]);
        s=stoi( StringWithSymbolIntoStrings(parts[3], "=")[1]);
    }

    long long TotalValue()
    {
        return x + m + a + s;
    }
};

class Cube
{
public:
    std::pair<int, int>x;
    std::pair<int, int>m;
    std::pair<int, int>a;
    std::pair<int, int>s;
    bool empty;
    Cube() {}

    Cube(std::pair<int, int>_x,
        std::pair<int, int>_m,
        std::pair<int, int>_a,
        std::pair<int, int>_s)
    {
        x = _x; m = _m, a = _a; s = _s;
        empty = false;
        if (x.first > x.second)empty = true;
        if (m.first > m.second)empty = true;
        if (a.first > a.second)empty = true;
        if (s.first > s.second)empty = true;
    }

    long long TotalValue()
    {
        if (empty)return 0;
        long long xx = x.second + 1 - x.first;
        long long mm = m.second + 1 - m.first;
        long long aa = a.second + 1 - a.first;
        long long ss = s.second + 1 - s.first;

        return xx * mm * aa * ss;
    }
};