#pragma once
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <boost/algorithm/string.hpp>
#include <numeric>
class Convertor
{
private :
    std::vector<std::tuple<long long, long long, long long>>_listOfRules;
public:
    Convertor(std::vector<std::tuple<long long, long long, long long>>& inputForCreate) 
    {
        _listOfRules = inputForCreate;
    }


    long long Convert(long long inVal) 
    {
        long long dif;
        for (std::tuple<long long, long long, long long>& tup: _listOfRules)
        {
            const auto [outBegRange, inBegRange,  sizeRange] = tup;

            dif = inVal - inBegRange;
            if (dif >= 0 && dif < sizeRange)
                return outBegRange + dif;
        }
        return inVal;
    }



};