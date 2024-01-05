#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
using namespace std;

string _instructions;
unordered_map<string, pair<string, string>> map;

void LoadData(std::vector<std::string>& vec)
{
    _instructions = vec.front();
    for (size_t i = 2; i < vec.size(); i++)
    {
        map[vec[i].substr(0, 3)]= { vec[i].substr(7, 3), vec[i].substr(12, 3) };
    }
}


bool AllEndByZ(vector<string>& startedA)
{
    for (string s : startedA)
    {
        if (s[2] != 'Z')
            return false;
    }
    return true;
}

long long Simulation2()
{
    vector<string>startedA;
    for (pair<string, pair<string, string>> ps : map)
    {
        if (ps.first[2] == 'A')
            startedA.push_back(ps.first);
    }

    long long step = 0;
    string tmp = "AAA";
    long long instLen = _instructions.size();
    while (true)
    {
        if (AllEndByZ(startedA))
            break;
        if (_instructions[step % instLen] == 'L')
            for (int i = 0; i < startedA.size(); i++)
            {
                startedA[i] = map[startedA[i]].first;
            }
        else
            for (int i = 0; i < startedA.size(); i++)
            {
                startedA[i] = map[startedA[i]].second;
            }
        step++;
    }
    return step;
}
long long Simulation1()
{
    long long step = 0;
    string tmp = "AAA";
    long long instLen = _instructions.size();
    while (true)
    {
        if (tmp == "ZZZ")
            break;
        if (_instructions[step % instLen] == 'L')
            tmp = map[tmp].first;
        else
            tmp = map[tmp].second;
        step++;
    }
    return step;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    LoadData(vec);
    long long lenOfSIm = Simulation1();
    return std::to_string(lenOfSIm);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    LoadData(vec);
    long long lenOfSIm = Simulation2();
    return std::to_string(lenOfSIm);
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
