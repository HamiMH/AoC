#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <set>
#include <map>
#include <unordered_set>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
#include <numeric>
using namespace std;

vector<pair<int, int>> galaxies;
set<int> hasX;
set<int> hasY;

long long Distance(pair<int, int> gal1, pair<int, int>gal2, long long distSpec)
{
    long long dist = 0;
    int minX = std::min(gal1.first, gal2.first);
    int maxX = std::max(gal1.first, gal2.first);
    int minY = std::min(gal1.second, gal2.second);
    int maxY = std::max(gal1.second, gal2.second);

    for (; minX < maxX; minX++)
        if (hasX.contains(minX))
            dist++;
        else
            dist += distSpec;
    for (; minY < maxY; minY++)
        if (hasY.contains(minY))
            dist++;
        else
            dist += distSpec;

    return dist;
}
long long Distance2(pair<int, int> gal1, pair<int, int>gal2, long long distSpec)
{
    long long dist = 0;
    int minX = std::min(gal1.first, gal2.first);
    int maxX = std::max(gal1.first, gal2.first);
    int minY = std::min(gal1.second, gal2.second);
    int maxY = std::max(gal1.second, gal2.second);

    distSpec--;
    long long d = std::distance(hasX.find(minX), hasX.find(maxX));
    dist += (d - 1) * distSpec;
    dist += maxX-minX; 
    d = std::distance(hasY.find(minY), hasY.find(maxY));
    dist += (d - 1) * distSpec;
    dist += maxY-minY;

    return dist;
}

void LoadInput(std::vector<std::string>& vec)
{
    int x, y;
    y = 0;
    for (string& str : vec)
    {
        x = 0;
        for (char c : str)
        {
            if (c == '#')
            {
                galaxies.push_back({ x,y });
                hasX.insert(x);
                hasY.insert(y);
            }

            x++;
        }
        y++;
    }
}

long long SumOfDistances(long long modif)
{
    long long sum = 0;
    int len = galaxies.size();
    for (int i = 0; i < len; i++)
        for (int j = i + 1; j < len; j++)
            sum += Distance2(galaxies[i], galaxies[j], modif);

    return sum;
}

std::string MySolution1(std::vector<std::string>& vec)
{  
    LoadInput(vec);
    return std::to_string(SumOfDistances(2));

}

std::string MySolution2(std::vector<std::string>& vec)
{
    LoadInput(vec);
    return std::to_string(SumOfDistances(1000000));
}
int main()
{
    /*std::set<int>ts;
    ts.insert(7);
    ts.insert(1);
    ts.insert(23);
    ts.insert(15);

    auto d1 = ts.find(1);
    auto d2 = ts.find(23);
    std::cout<<std::distance(ts.find(1), ts.find(7))<<endl;
    std::cout<<std::distance(ts.find(1), ts.find(15))<<endl;
    std::cout<<std::distance(ts.find(1), ts.find(23))<<endl;
    std::cout<<std::distance(ts.find(7), ts.find(15))<<endl;
    std::cout<<std::distance(ts.find(7), ts.find(23))<<endl;
    std::cout<<std::distance(ts.find(15), ts.find(23))<<endl;
    */

    std::string in;
    std::cin >> in;
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