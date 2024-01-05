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

    std::tuple<long long, long long> GetIntersection(long long inpBegRange, long long inpSizeRange, long long inBegRange, long long inSizeRange)
    {
        std::tuple<long long, long long> tupOut = { 0,0 };

        long long inpTo = inpBegRange + inpSizeRange - 1;
        long long inTo = inBegRange + inSizeRange - 1;

        long long maxFrom = std::max(inpBegRange, inBegRange);
        long long minTo = std::min(inpTo, inTo);

        if (maxFrom <= minTo) 
        {
            tupOut = { maxFrom,minTo - maxFrom + 1 };
        }
        return tupOut;
    }


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
    std::vector<std::tuple<long long, long long>> ConvertRange(std::vector<std::tuple<long long, long long>>& inRanges)
    {
        std::vector<std::tuple<long long, long long>> outRanges;
        for (std::tuple<long long, long long>& range: inRanges)
        {
            const auto [inpBegRange, inpSizeRange] = range;

            for (std::tuple<long long, long long, long long>& tup : _listOfRules)
            {
                const auto [outBegRange, inBegRange, inSizeRange] = tup;

                const auto [begInter, sizeinter] = GetIntersection(inpBegRange, inpSizeRange, inBegRange, inSizeRange);
                if (sizeinter != 0)
                {
                    outRanges.push_back({ outBegRange ,sizeinter });
                }
            }
        }
        return outRanges;
    }


    static std::vector<std::tuple<long long, long long>> PairMerge(std::vector<std::tuple<long long, long long>>& inRanges)
    {
        std::sort(inRanges.begin(), inRanges.end());

        std::vector<std::tuple<long long, long long>> mergedRanges;
        int size = inRanges.size();
        std::tuple<long long, long long>tmpPair = inRanges[0];
        for (int i = 1; i < size; i++) 
        {
            const auto [tmpFrom, tmpRange] = tmpPair;
            const auto [iFrom, iRange] = inRanges[i];
            if (iFrom <= tmpFrom + tmpRange)
            {
                tmpPair = { tmpFrom ,(iRange + iFrom) - tmpFrom };
            }
            else
            {
                mergedRanges.push_back(tmpPair);
                tmpPair = inRanges[i];
            }
        }
        mergedRanges.push_back(tmpPair);
        return mergedRanges;
    }

};