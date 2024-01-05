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
#include "Part.h"
#include "PartValidator.h"

using namespace std;

unordered_map<string, deque<Part>>ques;
unordered_map<string, PartValidator>validators;

void Process()
{
    bool someNonempty = true;
    while (someNonempty)
    {
        someNonempty = false;
        for (pair<const string, deque<Part>>& que : ques)
        {
            if (que.first == "A" || que.first == "R")continue;
            if (!que.second.empty())
                someNonempty = true;
            while (!que.second.empty())
            {
                Part par = que.second.front();
                que.second.pop_front();
                string tmp = validators[que.first].GetNextValidator(par);
                ques[tmp].push_back(par);
            }
        }
    }

}

std::string MySolution1(std::vector<std::string>& vec)
{
    long long sum = 0;
    bool wasEmpty = false;
    for (string& line : vec)
    {
        if (line == "")
        {
            wasEmpty = true;
            continue;
        }
        if (wasEmpty)
        {
            ques["in"].push_back(Part(line));
        }
        else
        {
            PartValidator partVal(line);
            validators[partVal.Name] = partVal;
            ques[partVal.Name] = {};
        }
    }
    ques["A"] = {};
    ques["R"] = {};
    Process();
    return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    long long sum = 0;

    return std::to_string(sum);
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
    std::cout << endl << "Result1: " << result << "\n";
    if (microseconds > 1000000000)
        std::cout << microseconds / 1000000 << "s\n";
    else if (microseconds > 1000000)
        std::cout << microseconds / 1000 << "ms\n";
    else
        std::cout << microseconds << "us\n";

    start = std::chrono::high_resolution_clock::now();
    result = MySolution2(vec);
    finish = std::chrono::high_resolution_clock::now();

    microseconds = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    std::cout << endl << "Result2: " << result << "\n";
    if (microseconds > 1000000000)
        std::cout << microseconds / 1000000 << "s\n";
    else if (microseconds > 1000000)
        std::cout << microseconds / 1000 << "ms\n";
    else
        std::cout << microseconds << "us\n";
}