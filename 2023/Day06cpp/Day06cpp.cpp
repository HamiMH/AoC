#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
using namespace std;

vector<long long>times;
vector<long long>distances;

tuple<double, double>RootKvadratic(double a, double b, double c)
{
    double det = b * b - 4 * a * c;
    if (det < 0)
        return { 1,0 };

    double root1 = (-b - std::sqrt(det)) / (2 * a);
    double root2 = (-b + std::sqrt(det)) / (2 * a);
    if (root1 < root2)
        return { root1 ,root2 };
    else
        return { root2 ,root1 };
}

long long Solve1(size_t i)
{
    long long time = times[i];
    long long distance = distances[i];

    // s*(t-s)   compl distance
    // -s^2 +s t - d
    const auto [root1D, root2D ] = RootKvadratic(-1, time, -distance);

    long long  root1=std::ceil(root1D);
    long long  root2=std::floor(root2D);
    if (std::abs(((double)root1) - root1D) < 0.00000000001)
        root1++;
    if (std::abs(((double)root2) - root2D) < 0.00000000001)
        root2--;
    return root2 - root1 + 1;
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

void LoadInput(std::vector<std::string>& vec)
{
    vector<string>tmpVec;
    boost::split(tmpVec, vec.front(), boost::is_any_of(":"));
    StringWithSpacesIntoLongs(tmpVec.back(), times);
    tmpVec.clear();
    boost::split(tmpVec, vec.back(), boost::is_any_of(":"));
    StringWithSpacesIntoLongs(tmpVec.back(), distances);
}

void LoadInput2(std::vector<std::string>& vec)
{
    LoadInput(vec);
    string sl;
    for (long long l : times)
    {
        sl += std::to_string(l);
    }
    times.clear();
    times.push_back(stoll(sl));

    sl.clear();
    for (long long l : distances)
    {
        sl += std::to_string(l);
    }
    distances.clear();
    distances.push_back(stoll(sl));
}

std::string MySolution1(std::vector<std::string>& vec)
{
    LoadInput(vec);
    long long prod = 1;
    for (size_t i = 0; i < times.size(); i++)
        prod *= Solve1(i);

    return std::to_string(prod);
}

std::string MySolution2(std::vector<std::string>& vec)
{
    LoadInput2(vec);
    return std::to_string(Solve1(0));
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
