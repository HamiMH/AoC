#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
#include "CardHand.h"
#include "CardHand2.h"
using namespace std;

std::vector<CardHand> hands;
std::vector<CardHand2> hands2;

std::string MySolution1(std::vector<std::string>& vec)
{
    for (std::string str : vec)
    {

        boost::trim(str);
        vector<string>tmpVec;
        boost::split(tmpVec, str, boost::is_any_of(" "));
        hands.push_back(CardHand(tmpVec[0], stoll(tmpVec[1])));
    }

    std::sort(hands.begin(), hands.end());
    long long sum=0;
    for (int i = 0; i < hands.size(); i++)
        sum += (i + 1) * hands[i].Bet();
    return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    for (std::string str : vec)
    {

        boost::trim(str);
        vector<string>tmpVec;
        boost::split(tmpVec, str, boost::is_any_of(" "));
        hands2.push_back(CardHand2(tmpVec[0], stoll(tmpVec[1])));
    }

    std::sort(hands2.begin(), hands2.end());
    long long sum = 0;
    for (int i = 0; i < hands2.size(); i++)
        sum += (i + 1) * hands2[i].Bet();
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
