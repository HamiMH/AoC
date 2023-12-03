#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>

using namespace std;

int nOfRow ;
int nOfCol ;

vector<pair<int, int>> directions =
{
    {1,0},
    {1,1},
    {0,1},
    {-1,1},
    {-1,0},
    {-1,-1},
    {0,-1},
    {1,-1},
};

bool SymbolIsAdj(int i, int j, std::vector<std::string>& vec)
{
    int ii, jj;
    char c;
    for (pair<int, int>& pair : directions)
    {
        ii = pair.first + i;
        jj = pair.second + j;
        if (ii < 0 || jj < 0 || ii >= nOfCol || jj >= nOfCol)
            continue;
        c = vec[ii][jj];
        if ((c >= '0' && c <= '9') || c == '.') {

        }
        else
        {
            return true;
        }
    }
    return false;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    nOfRow = vec.size();
    nOfCol = vec[0].size();
    int i, j;
    int totSum = 0;
    int partSum=0;
    bool adjSym = false;
    char c;
    for (i = 0; i < nOfRow; i++) 
    {
        for (j = 0; j < nOfCol; j++)
        {
            c =vec[i][j];
            if (c >= '0' && c <= '9') 
            {
                partSum *= 10;
                partSum += (c-'0');
                adjSym |= SymbolIsAdj(i, j, vec);
            }
            else
            {
                if (adjSym)
                    totSum += partSum;
                adjSym = false;
                partSum = 0;
            }
        }
        if (adjSym)
            totSum += partSum;
        adjSym = false;
        partSum = 0;
    }

    return std::to_string(totSum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    return "";

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
    std::cout << "Result: " << result << "\n";
    if (microseconds > 1000000000)
        std::cout << microseconds / 1000000 << "s\n";
    else if (microseconds > 1000000)
        std::cout << microseconds / 1000 << "ms\n";
    else
        std::cout << microseconds << "us\n";
}
