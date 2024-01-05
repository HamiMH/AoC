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
private:
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
        for (std::tuple<long long, long long, long long>& tup : _listOfRules)
        {
            const auto [outBegRange, inBegRange, sizeRange] = tup;

            dif = inVal - inBegRange;
            if (dif >= 0 && dif < sizeRange)
                return outBegRange + dif;
        }
        return inVal;
    }
    std::vector<std::tuple<long long, long long>> ConvertRange(std::vector<std::tuple<long long, long long>>& inRanges)
    {
        inRanges = PairMerge(inRanges);

        std::vector<std::tuple<long long, long long>> outRanges;
        for (std::tuple<long long, long long>& range : inRanges)
        {
            std::vector<std::tuple<long long, long long>> inters;
            const auto [inpBegRange, inpSizeRange] = range;

            for (std::tuple<long long, long long, long long>& tup : _listOfRules)
            {
                const auto [outBegRange, inBegRange, inSizeRange] = tup;

                const auto [begInter, sizeinter] = GetIntersection(inpBegRange, inpSizeRange, inBegRange, inSizeRange);
                if (sizeinter != 0)
                {
                    inters.push_back({ begInter ,sizeinter });
                    outRanges.push_back({ outBegRange ,sizeinter });
                }
            }
            AddCuts(outRanges, range, inters);
        }
        return outRanges;
    }


    static void AddCuts(std::vector<std::tuple<long long, long long>>& outRanges, std::tuple<long long, long long>& range, std::vector<std::tuple<long long, long long>>& inters)
    {
        inters = PairMerge(inters);
        const auto [begRangeIn, sizeRangeIn] = range;

        long long begRange = begRangeIn;
        long long sizeRange = sizeRangeIn;
        std::tuple<long long, long long> rangeToAdd = { 0,0 };

        for (std::tuple<long long, long long>& inter : inters)
        {
            const auto [begRangeInter, sizeRangeInter] = inter;
            if(begRangeInter - begRange>0)
                outRanges.push_back({ begRange, begRangeInter - begRange });
            begRange = begRangeInter+sizeRangeInter;
        }
        if (begRangeIn+ sizeRangeIn - begRange > 0)
            outRanges.push_back({ begRange, begRangeIn + sizeRangeIn - begRange });
    }

    static std::vector<std::tuple<long long, long long>> PairMerge(std::vector<std::tuple<long long, long long>>& inRanges)
    {
        std::sort(inRanges.begin(), inRanges.end());

        std::vector<std::tuple<long long, long long>> mergedRanges;
        int size = inRanges.size();
        if (size == 0)
            return inRanges;
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