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

using namespace std;


bool CompareCols(std::vector<std::string>& vec, int first, int second)
{
    int nOfRows = vec.size();
    for (int i = 0; i < nOfRows; i++)
    {
        if (vec[i][first] != vec[i][second])
            return false;
    }
    return true;
}
bool CompareRows(std::vector<std::string>& vec, int first, int second)
{
    return (vec[first].compare(vec[second]) == 0);
}

vector<vector<string>>  GetListOfInputs(std::vector<std::string>& vec)
{
    vector<vector<string>> out;
    vector<string> tmp;
    for (string& str : vec)
    {
        if (str.empty())
        {
            out.push_back(tmp);
            tmp.clear();
        }
        else
        {
            tmp.push_back(str);
        }
    }
    if (!tmp.empty())
        out.push_back(tmp);
    return out;
}
long long CalculateReflection(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j;
    bool allOk;
    for (i = 0; i < nOfRows - 1; i++)
    {
        allOk = true;
        for (j = 0; i + j + 1 < nOfRows && i - j >= 0; j++)
        {
            if (!CompareRows(vec, i - j, i + j + 1))
            {
                allOk = false;
                break;
            }
        }
        if (allOk)
        {
            return 100 * (i + 1);
        }
    }
    for (i = 0; i < nOfCols - 1; i++)
    {
        allOk = true;
        for (j = 0; i + j + 1 < nOfCols && i - j >= 0; j++)
        {
            if (!CompareCols(vec, i - j, i + j + 1))
            {
                allOk = false;
                break;
            }
        }
        if (allOk)
        {
            return  (i + 1);
        }
    }
    //for (i = nOfRows%2; i < nOfRows-1; i++)
    //{
    //    /*allOk = true;
    //    for (j = i; j < (nOfRows-i)/2; j++)
    //    {
    //        if (!CompareRows(vec, j, nOfRows - 1 - (j - i)))
    //        {
    //            allOk = false;
    //            break;
    //        }
    //    }
    //    if (allOk)
    //    {

    //    }*/


    //}

    return 0;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    vector<vector<string>> vvs = GetListOfInputs(vec);
    long long sum = 0;
    for (vector<string>& vecStr : vvs)
    {
        sum += CalculateReflection(vecStr);
    }
    return std::to_string(sum);

}
void Swap(vector<string>& vecStr, int i, int j)
{
    if (vecStr[i][j] == '.')
        vecStr[i][j] = '#';
    else
        vecStr[i][j] = '.';
}

std::string MySolution2(std::vector<std::string>& vec)
{
    vector<vector<string>> vvs = GetListOfInputs(vec);
    long long sum = 0;
    int i, j;
    int nOfRows;
    int nOfCols;
    long long tmp;
    bool toBreak = false;
    for (vector<string>& vecStr : vvs)
    {
        toBreak = false;
        nOfRows = vecStr.size();
        nOfCols = vecStr.front().size();
        for (i = 0; i < nOfRows; i++)
        {

            for (j = 0; j < nOfCols; j++)
            {
                Swap(vecStr, i, j);
                tmp = CalculateReflection(vecStr);
                Swap(vecStr, i, j);
                if (tmp != 0)
                {
                    sum += tmp;
                    toBreak = true;
                    break;
                }
            }
            if (toBreak)
                break;
        }
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