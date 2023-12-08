#pragma once
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <boost/algorithm/string.hpp>
#include <numeric>
#include "Range.h"
class Convertor
{
private:
    std::vector<std::tuple<long long, long long, long long>>_listOfRules;

    Range GetIntersection(long long inpBegRange, long long inpSizeRange, long long inBegRange, long long inSizeRange)
    {

        long long inpTo = inpBegRange + inpSizeRange - 1;
        long long inTo = inBegRange + inSizeRange - 1;

        long long maxFrom = std::max(inpBegRange, inBegRange);
        long long minTo = std::min(inpTo, inTo);

        if (maxFrom <= minTo)
        {
            return Range(maxFrom, minTo - maxFrom + 1);
        }
        else
        {
            return Range(0, 0);
        }
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
            const auto& [outBegRange, inBegRange, sizeRange] = tup;

            dif = inVal - inBegRange;
            if (dif >= 0 && dif < sizeRange)
                return outBegRange + dif;
        }
        return inVal;
    }

    std::vector<Range> ConvertRange(std::vector<Range>& inRanges)
    {
        inRanges = Range::PairsMerge(inRanges);

        std::vector<Range> outRanges;
        for (Range& range : inRanges)
        {
            std::vector<Range> inters;

            for (std::tuple<long long, long long, long long>& tup : _listOfRules)
            {
                const auto& [outBegRange, inBegRange, inSizeRange] = tup;

                Range intersection = GetIntersection(range.Begin(), range.Length(), inBegRange, inSizeRange);
                if (intersection.Length() != 0)
                {
                    inters.push_back(intersection);
                    outRanges.push_back({ Convert(intersection.Begin()) ,intersection.Length() });
                }
            }
            AddNotMappedParts(outRanges, range, inters);
        }
        return outRanges;
    }


    static void AddNotMappedParts(std::vector<Range>& outRanges, Range& range, std::vector<Range>& inters)
    {
        inters = Range::PairsMerge(inters);

        long long begRange = range.Begin();
        long long sizeRange = range.Length();

        for (Range& inter : inters)
        {
            if (inter.Begin() - begRange > 0)
                outRanges.push_back({ begRange, inter.Begin() - begRange });
            begRange = inter.Begin() + inter.Length();
        }
        if (range.Begin() + range.Length() - begRange > 0)
            outRanges.push_back({ begRange, range.Begin() + range.Length() - begRange});
    }



};