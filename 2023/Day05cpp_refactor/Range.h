#pragma once
#include <vector>
#include <algorithm>

class Range
{


private:
    long long _begin;
    long long _end;
    long long _length;

public:
    long long Begin() { return _begin; }
    long long End() { return _end; }
    long long Length() { return _length; }

    Range(long long begin, long long len)
    {
        _begin = begin;
        _length = len;
        _end = begin + len - 1;
    }


    Range GetIntersection(Range range)
    {
        long long maxFrom = std::max(this->Begin(), range.Begin());
        long long minTo = std::min(this->End(), range.End());

        if (maxFrom <= minTo)
            return Range(maxFrom, minTo - maxFrom + 1);
        else
            return Range(0, 0);
    }

    bool operator< (Range const& y) const
    {
        if (this->_begin < y._begin)
            return true;
        if (this->_begin > y._begin)
            return false;

        if (this->_end <= y._end)
            return true;
        return false;
    }

    static std::vector<Range> PairsMerge(std::vector< Range>& inRanges)
    {
        std::sort(inRanges.begin(), inRanges.end());

        std::vector<Range> mergedRanges;
        int size = inRanges.size();
        if (size == 0)
            return inRanges;

        Range tmpRange = inRanges.front();
        for (int i = 1; i < size; i++)
        {
            //const auto& [tmpFrom, tmpRange] = tmpRange;
            //const auto& [iFrom, iRange] = inRanges[i];
            Range nowRange = inRanges[i];
            if (nowRange.Begin() <= tmpRange.End() + 1)
            {
                tmpRange = { tmpRange.Begin() ,(nowRange.End() + 1) - tmpRange.Begin() };
            }
            else
            {
                mergedRanges.push_back(tmpRange);
                tmpRange = inRanges[i];
            }
        }
        mergedRanges.push_back(tmpRange);
        return mergedRanges;
    }
};