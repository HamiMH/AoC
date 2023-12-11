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
vector<int> vecX;
vector<int> vecY;
/*
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
    dist += ((maxX - minX) - d) * distSpec;
    dist += maxX-minX; 
    d = std::distance(hasY.find(minY), hasY.find(maxY)) ;
    dist += ((maxY - minY) - d ) * distSpec;
    dist += maxY-minY;

    return dist;
}
*/

long long Distance3(pair<int, int> gal1, pair<int, int>gal2, long long distSpec)
{
    long long dist = 0;
    int minX = std::min(gal1.first, gal2.first);
    int maxX = std::max(gal1.first, gal2.first);
    int minY = std::min(gal1.second, gal2.second);
    int maxY = std::max(gal1.second, gal2.second);

    distSpec--;

    long long d = std::distance(std::lower_bound(vecX.begin(), vecX.end(), minX), std::lower_bound(vecX.begin(), vecX.end(), maxX));
    dist += ((maxX - minX) - d) * distSpec;
    dist += maxX-minX; 
    d = std::distance(std::lower_bound(vecY.begin(), vecY.end(), minY), std::lower_bound(vecY.begin(), vecY.end(), maxY));
    dist += ((maxY - minY) - d ) * distSpec;
    dist += maxY-minY;

    return dist;
}

void LoadInput(std::vector<std::string>& vec)
{
    int x, y;
    y = 0;
    unordered_set<int> usX;
    unordered_set<int> usY;
    for (string& str : vec)
    {
        x = 0;
        for (char c : str)
        {
            if (c == '#')
            {
                galaxies.push_back({ x,y });
                usX.insert(x);
                usY.insert(y);
            }
            x++;
        }
        y++;
    }
    vecX.insert(vecX.end(), usX.begin(), usX.end());
    vecY.insert(vecY.end(), usY.begin(), usY.end());
    std::sort(vecX.begin(), vecX.end());
    std::sort(vecY.begin(), vecY.end());
}

long long SumOfDistances(long long modif)
{
    long long sum = 0;
    int len = galaxies.size();
    for (int i = 0; i < len; i++)
        for (int j = i + 1; j < len; j++)
            sum += Distance3(galaxies[i], galaxies[j], modif);

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