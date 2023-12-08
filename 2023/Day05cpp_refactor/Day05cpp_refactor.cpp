#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <boost/algorithm/string.hpp>
#include <numeric>
#include "Convertor.h"
using namespace std;

std::vector<Convertor> Converts;

long long ConvertByAll(long long in)
{
    for (Convertor& conv : Converts)
    {
        in = conv.Convert(in);
    }
    return in;
}
std::vector<Range> ConvertByAllRanges(std::vector<Range> in)
{
    for (Convertor& conv : Converts)
    {
        in = conv.ConvertRange(in);
    }
    return in;
}

void StringWithSpacesIntoLongs(string str, vector<long long>& outVec)
{
    boost::trim(str);
    vector<string>tmpVec;
    boost::split(tmpVec, str, boost::is_any_of(" "));

    for (string& s : tmpVec)
    {
        boost::trim(s);
        if (s == "")
            continue;
        outVec.push_back(stoll(s));
    }
}

vector<long long> LoadInputAndGenerateConvertors(std::vector<std::string>& vec)
{
    vector<long long> seeds;
    vector<string>tmpVec;
    boost::split(tmpVec, vec[0], boost::is_any_of(":"));
    StringWithSpacesIntoLongs(tmpVec[1], seeds);
    vector<tuple<long long, long long, long long>> inputForCreate;
    for (int i = 3; i < vec.size(); i++)
    {
        string str = vec[i];
        boost::trim(str);
        if (str == "")
        {
            Converts.push_back(Convertor(inputForCreate));
            inputForCreate.clear();
            i++;
            continue;
        }
        vector<long long> lineAsVeclong;
        StringWithSpacesIntoLongs(str, lineAsVeclong);
        inputForCreate.push_back({ lineAsVeclong[0], lineAsVeclong[1], lineAsVeclong[2] });
    }
    Converts.push_back(Convertor(inputForCreate));
    return seeds;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    vector<long long> seeds = LoadInputAndGenerateConvertors(vec);
    long long min = LONG_MAX;
    for (long long& seed : seeds)
    {
        long long convVal = ConvertByAll(seed);
        string sssss = std::to_string(convVal);
        min = std::min(min, convVal);
    }
    return std::to_string(min);

}


std::string MySolution2(std::vector<std::string>& vec)
{
    vector<long long> seeds = LoadInputAndGenerateConvertors(vec);
    std::vector<Range> tmp;
    for (int i = 0; i < seeds.size(); i += 2)
    {
        long long seedBase = seeds[i];
        long long seedRange = seeds[i + 1];

        tmp.push_back(Range( seeds[i] ,seeds[i + 1] ));
    }
    tmp = Range::PairsMerge(tmp);

    tmp = ConvertByAllRanges(tmp);
    tmp = Range::PairsMerge(tmp);
    return std::to_string(tmp.front().Begin());
}

int main()
{
    std::string in;
    std::cin >> in;
    //std::ifstream file("input.txt");
    std::ifstream file(in + ".txt");
    std::string str;
    std::vector<std::string> vec;
    while (std::getline(file, str))
    {
        vec.push_back(str);
    }

    auto start = std::chrono::high_resolution_clock::now();
    std::string result = MySolution2(vec);
    auto finish = std::chrono::high_resolution_clock::now();

    auto microseconds = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    std::cout << "Result: " << result << "\n";
    if (microseconds > 1000000000)
        std::cout << microseconds / 1000000 << "s\n";
    else if (microseconds > 1000000)
        std::cout << microseconds / 1000 << "ms\n";
    else
        std::cout << microseconds << "us\n";
}
