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

unordered_map<size_t, size_t> hashMap;

size_t Hasher(std::vector<string> const& vec)
{
    std::size_t ret = 0;
    for (auto& i : vec) {
        ret ^= std::hash<string>()(i);
    }
    return ret;
}

void PrintVecStr(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j;
    for (i = 0; i < nOfRows; i++)
    {
        for (j = 0; j < nOfCols; j++)
        {
            cout << vec[i][j];
        }
        cout << endl;
    }
    cout << endl;
    cout << endl;
}


void GoWest(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j;
    int lastRock;
    long long sum = 0;
    for (i = 0; i < nOfRows; i++)
    {
        lastRock = -1;
        for (j = 0; j < nOfCols; j++)
        {
            switch (vec[i][j])
            {
            case '#':
                lastRock = j;
                break;
            case 'O':
                vec[i][j] = '.';
                vec[i][lastRock + 1] = 'O';
                lastRock++;
            }
        }
    }
}

void GoEast(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j;
    int lastRock;
    long long sum = 0;
    for (i = 0; i < nOfRows; i++)
    {
        lastRock = nOfCols;
        for (j = nOfCols - 1; j >= 0; j--)
        {
            switch (vec[i][j])
            {
            case '#':
                lastRock = j;
                break;
            case 'O':
                vec[i][j] = '.';
                vec[i][lastRock - 1] = 'O';
                lastRock--;
            }
        }

    }
}


void GoNorth(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j;
    int lastRock;
    long long sum = 0;
    for (i = 0; i < nOfCols; i++)
    {
        lastRock = -1;
        for (j = 0; j < nOfRows; j++)
        {
            switch (vec[j][i])
            {
            case '#':
                lastRock = j;
                break;
            case 'O':
                vec[j][i] = '.';
                vec[lastRock + 1][i] = 'O';
                lastRock++;
            }
        }

    }
}
void GoSouth(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j;
    int lastRock;
    long long sum = 0;
    for (i = 0; i < nOfCols; i++)
    {
        lastRock = nOfRows;
        for (j = nOfRows - 1; j >= 0; j--)
        {
            switch (vec[j][i])
            {
            case '#':
                lastRock = j;
                break;
            case 'O':
                vec[j][i] = '.';
                vec[lastRock - 1][i] = 'O';
                lastRock--;
            }
        }

    }
}



void Simulate(std::vector<std::string>& vec, long long steps)
{
    size_t hashOfVec;
    int i = 0;
    int previous;
    int cycle;
    int remainStep;
    int remainCycle;
    for (i = 0; i < steps; i++)
    {

        hashOfVec = Hasher(vec);
        if (hashMap.contains(hashOfVec))
        {
            previous = hashMap[hashOfVec];
            cycle = i - previous;
            remainStep = steps - i;
            remainCycle = remainStep / cycle;
            i += remainCycle * cycle;
        }
        else
            hashMap[hashOfVec] = i;
        /*
        if (hashMap.contains(hashOfVec))
        {
            previous = hashMap[hashOfVec];
            cycle = i - previous;
            remainStep = steps - i;
            remainCycle = remainStep / cycle;
            i += remainCycle * cycle;
        }
        else
            hashMap[hashOfVec] = i;
        */
        GoNorth(vec);
        GoWest(vec);
        GoSouth(vec);
        GoEast(vec);
    }
}

long long CalculateSolution(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j, y;
    int lastRock;
    long long sum = 0;
    for (i = 0; i < nOfCols; i++)
    {
        lastRock = nOfRows + 1;
        for (j = 0; j < nOfRows; j++)
        {
            y = nOfRows - j;
            switch (vec[j][i])
            {
            case '#':
                lastRock = y;
                break;
            case '0':
            case 'O':
                sum += (lastRock - 1);
                lastRock--;
            }
        }

    }
    return sum;
}
long long CalculateValue(std::vector<std::string>& vec)
{
    int nOfRows = vec.size();
    int nOfCols = vec.front().size();
    int i, j, y;
    long long sum = 0;
    for (i = 0; i < nOfCols; i++)
    {
        for (j = 0; j < nOfRows; j++)
        {
            y = nOfRows - j;
            switch (vec[j][i])
            {
            case '0':
            case 'O':
                sum += (y);
            }
        }

    }
    return sum;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    long long sum = 0;
    sum += CalculateSolution(vec);
    return std::to_string(sum);

}
std::string MySolution2(std::vector<std::string>& vec)
{
    long long sum = 0;
    Simulate(vec, 1000000000);
    sum += CalculateValue(vec);
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