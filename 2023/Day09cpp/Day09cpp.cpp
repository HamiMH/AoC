#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
#include <numeric>
using namespace std;



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

long long FindLast(vector<long long>& inVec)
{
    vector<vector<long long>> matrix;
    matrix.push_back(inVec);
    bool allZeroes = false;
    long long i = 1;
    long long j= 1;
    long long num = 0;
    while (!allZeroes)
    {
        allZeroes = true;
        matrix.push_back({});
        for (j = 1; j < matrix[i-1].size(); j++)
        {
            num = matrix[i - 1][j] - matrix[i - 1][j - 1];
            matrix[i].push_back(num);
            if (num != 0)
                allZeroes = false;
        }
        i++;
    }
    for (i = matrix.size() - 2; i >= 0; i--)
    {
        matrix[i].push_back(matrix[i].back() + matrix[i + 1].back());
    }
    return matrix.front().back();
}

long long FindFirst(vector<long long>& inVec)
{
    vector<vector<long long>> matrix;
    matrix.push_back(inVec);
    bool allZeroes = false;
    long long i = 1;
    long long j= 1;
    long long num = 0;
    while (!allZeroes)
    {
        allZeroes = true;
        matrix.push_back({});
        for (j = 1; j < matrix[i-1].size(); j++)
        {
            num = matrix[i - 1][j] - matrix[i - 1][j - 1];
            matrix[i].push_back(num);
            if (num != 0)
                allZeroes = false;
        }
        i++;
    }
    for (i = matrix.size() - 2; i >= 0; i--)
    {
        matrix[i][0]=(matrix[i].front() - matrix[i + 1].front());
    }
    return matrix.front().front();
}

std::string MySolution1(std::vector<std::string>& vec)
{
    long long sum = 0;
    for (string& s : vec)
    {
        vector<long long> vecll;
        StringWithSpacesIntoLongs(s, vecll);
        sum += FindLast(vecll);
    }
    return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    long long sum = 0;
    for (string& s : vec)
    {
        vector<long long> vecll;
        StringWithSpacesIntoLongs(s, vecll);
        sum += FindFirst(vecll);
    }
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