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
using namespace std;

bool RestOk(string_view& line, long long startInd)
{
    for (long long i = startInd; i < line.size(); i++)
        if (line[i ] == '#')
            return false;
    return true;
}
bool CanBeHash(string_view& line, long long startInd, long long len)
{
    if (startInd + len > line.size())
        return false;
    long long i;
    for ( i = 0; i < len; i++)
        if (line[i + startInd] == '.')
            return false;
    if (i + startInd < line.size() && line[i + startInd] == '#')
        return false;
    return true;
}

long long GetNumOfVars(string_view& line, vector<long long>& nums, long long indLine, long long indVec)
{
    if (indVec == nums.size())
    {
        if (RestOk(line, indLine))
            return 1;
        else
            return 0;
    }
    if (indLine >= line.size())
        return 0;
    for (long long i = indLine; i < line.size(); i++)
    {
        switch (line[i])
        {
        case '#':
            if (CanBeHash(line, i, nums[indVec]))
                return GetNumOfVars(line, nums, i + nums[indVec]+1, indVec + 1);
            break;
        case '?':
            if (CanBeHash(line, i, nums[indVec]))
            {
                long long sum = GetNumOfVars(line, nums, i + nums[indVec]+1, indVec + 1);
                sum += GetNumOfVars(line, nums, i + 1, indVec);
                return sum;
            }
            break;
        }
    }

    return 0;
}

void StringWithSpacesIntoLongs(string str, vector<long long>& outVec)
{
    boost::trim(str);
    vector<string>tmpVec;
    boost::split(tmpVec, str, boost::is_any_of(","));

    for (string& s : tmpVec)
    {
        boost::trim(s);
        if (s == "")
            continue;
        outVec.push_back(stoll(s));
    }
}


void ReadInput(string& in, string& out, vector<long long>& nums)
{
    vector<string>tmpVec;
    boost::split(tmpVec, in, boost::is_any_of(" "));
    out = tmpVec[0];
    StringWithSpacesIntoLongs(tmpVec[1], nums);
}
std::string MySolution1(std::vector<std::string>& vec)
{
    long long sum = 0;
    for (string& str : vec)
    {
        string line;
        vector<long long> nums;
        ReadInput(str, line, nums);
        string_view swLine = line;
        long long res = GetNumOfVars(swLine, nums, 0, 0);
        std::cout << "pr: " << res << "\n";
        sum += res;
    }
    return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    return std::to_string(0);
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
    std::string result = MySolution1(vec);
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