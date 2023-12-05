#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include <set>
#include <boost/algorithm/string.hpp>
#include <numeric>
using namespace std;

std::vector<std::string> vec;

vector < set<int>> win;
vector < set<int>> have;

void LoadInput()
{
    for (string& line : vec) 
    {
        vector<string> mainParts;
        boost::split(mainParts, line, boost::is_any_of(":|"));
        vector<string> winLine;
        vector<string> haveLine;
        boost::trim(mainParts[1]);
        boost::trim(mainParts[2]);
        boost::split(winLine, mainParts[1], boost::is_any_of(" "));
        boost::split(haveLine,mainParts[2], boost::is_any_of(" "));

        set<int> winLineSet;
        set<int> haveLineSet;
        for (string& s : winLine) 
        {
            boost::trim(s);
            if (s == "")
                continue;
            winLineSet.insert(stoi(s));
        }
        for (string& s : haveLine)
        {
            boost::trim(s);
            if (s == "")
                continue;
            haveLineSet.insert(stoi(s));
        }
        win.push_back(winLineSet);
        have.push_back(haveLineSet);
    }
}

std::string MySolution1()
{
    LoadInput();

    int sum = 0;
    for (int i = 0; i < win.size(); i++) 
    {
    std::vector<int> v_intersection;
    std::set_intersection(win[i].begin(), win[i].end(), have[i].begin(), have[i].end(),
        std::back_inserter(v_intersection));

    int num = 1;
    num <<= v_intersection.size();
    num >>= 1;
    sum += num;
    }

    return std::to_string(sum);
}

std::string MySolution2()
{
    LoadInput();
    int lenIn= vec.size();
    vector<long long> nOfCards;
    for (int i = 0; i < lenIn; i++)
        nOfCards.push_back(1);

    for (int i = 0; i < lenIn; i++)
    {
        std::vector<int> v_intersection;
        std::set_intersection(win[i].begin(), win[i].end(), have[i].begin(), have[i].end(),
            std::back_inserter(v_intersection));
        int num = v_intersection.size();

        for (int j = 1; j <= num; j++)
        {
            if (i + j >= lenIn)
                break;
            nOfCards[i + j] += nOfCards[i];
        }
    }

    return std::to_string(std::reduce(nOfCards.begin(), nOfCards.end()));

}

int main()
{
    std::string in;
    std::cin >> in;
    //std::ifstream file("input.txt");
    std::ifstream file(in + ".txt");
    std::string str;
    while (std::getline(file, str))
    {
        vec.push_back(str);
    }
    auto start = std::chrono::high_resolution_clock::now();
    std::string result = MySolution2();
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
