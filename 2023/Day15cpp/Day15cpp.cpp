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
#include "Lens.h"

using namespace std;




std::string MySolution1(std::vector<std::string>& vec)
{
    long long sum = 0;
    vector<string> cases;
    StringWithSymbolIntoStrings(vec.front(), cases, ",");

    for (string& s : cases)
    {
        sum += HashOfStr(s);
    }

    return std::to_string(sum);

}

long long CalculateBoxes(vector<vector<Lens>>boxes)
{
    long long sum = 0;
    long long boxIndex = 0;
    long long slot = 0;
    long long focLen = 0;
    for (vector<Lens>& box : boxes)
    {
        slot = 0;
        for (Lens& len : box)
        {
            sum += (boxIndex + 1) * (slot + 1) * len.Value;
            slot++;
        }
        boxIndex++;
    }
    return sum;
}

std::string MySolution2(std::vector<std::string>& vec)
{
    long long sum = 0;
    vector<string> cases;
    vector<Lens> lenses;
    StringWithSymbolIntoStrings(vec.front(), cases, ",");

    for (string& s : cases)
    {
        lenses.push_back(Lens(s));
    }
    vector<vector<Lens>>boxes;
    for (int i = 0; i < 256; i++)
        boxes.push_back({});

    long long index;
    for (Lens& lens : lenses)
    {
        index = lens.LabelValue;
        int i;
        for (i = 0; i < boxes[index].size(); i++)
        {
            if (boxes[index][i].Label == lens.Label)
            {
                if (lens.Symbol == '-')
                {
                    boxes[index].erase(std::next(boxes[index].begin(), i));
                    break;
                }
                else
                {
                    boxes[index][i] = lens;
                    break;
                }
            }
        }
        if (i == boxes[index].size() && lens.Symbol == '=')
            boxes[index].push_back(lens);
    }
    return std::to_string(CalculateBoxes(boxes));
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