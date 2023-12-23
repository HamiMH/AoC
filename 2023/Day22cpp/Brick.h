#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <set>
#include <map>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
#include <numeric>
#include <algorithm>
#include <numeric>


using namespace std;


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

class Brick
{
public:
	pair<int, int>X;
	pair<int, int>Y;
	pair<int, int>Z;
    int Index;

    Brick() {}
	Brick(string& str,int index,int &maxX,int &maxY,int &maxZ)
	{
        Index = index;
        vector<string>sides = StringWithSymbolIntoStrings(str, "~");

        vector<string>beg = StringWithSymbolIntoStrings(sides[0], ",");
        vector<string>end = StringWithSymbolIntoStrings(sides[1], ",");
        X = { stoi(beg[0]) ,stoi(end[0]) };
        Y = { stoi(beg[1]) ,stoi(end[1]) };
        Z = { stoi(beg[2]) ,stoi(end[2]) };

        if (X.first > X.second)
        {
            int tmp = X.first;
            X.first = X.second;
            X.second = tmp;
        }
        if (Y.first > Y.second)
        {
            int tmp = Y.first;
            Y.first = Y.second;
            Y.second = tmp;
        }
        if (Z.first > Z.second)
        {
            int tmp = Z.first;
            Z.first = Z.second;
            Z.second = tmp;
        }

        maxX = max(X.first, maxX);
        maxX = max(X.second, maxX);
        maxY= max(Y.first, maxY);
        maxY = max(Y.second, maxY);
        maxZ = max(Z.first, maxZ);
        maxZ = max(Z.second, maxZ);
	}


    bool operator< (Brick const& y) const
    {
        if (this->Z.first > y.Z.first)
            return true;
        if (this->Z.first < y.Z.first)
            return false;

        if (this->Z.second > y.Z.second)
            return true;
        if (this->Z.second < y.Z.second)
            return false;

        return false;
    }

};