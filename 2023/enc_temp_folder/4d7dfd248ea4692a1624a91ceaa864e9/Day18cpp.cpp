#include <iostream>
#include <sstream>
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


unordered_map<string, char>corners =
{
    {"RD",'7'},
    {"RU",'J'},
    {"LD",'F'},
    {"LU",'L'},
    {"UL",'7'},
    {"UR",'F'},
    {"DL",'J'},
    {"DR",'L'},
};

unordered_map<char, pair<int, int>>directs =
{
    {'R',{1,0}},
    {'L',{-1,0}},
    {'U',{0,-1}},
    {'D',{0,1}},
};

vector< tuple<char, int>>input;

vector<vector<char>>layout;
vector<vector<pair<int, char>>>layout2;

int minX, maxX, minY, maxY;

std::vector<std::string>  StringWithSymbolIntoStrings(std::string str, std::string symbols)
{
    std::vector<std::string> outVec;
    boost::trim(str);
    std::vector<std::string>tmpVec;
    boost::split(tmpVec, str, boost::is_any_of(symbols));

    for (std::string& s : tmpVec)
    {
        boost::trim(s);
        if (s == "")
            continue;
        outVec.push_back((s));
    }
    return outVec;
}

void CreateLayoutDims()
{
    int dimX = maxX - minX;
    int dimY = maxY - minY;
    vector<char>tmp;
    for (int i = 0; i <= dimX; i++)
        tmp.push_back('.');
    for (int i = 0; i <= dimY; i++)
        layout.push_back(tmp);
}
void CreateLayoutDims2()
{
    int dimX = maxX - minX;
    int dimY = maxY - minY;
    for (int i = 0; i <= dimY; i++)
        layout2.push_back({});
}

void DrawLine(int& x, int& y, char corType, char dir, int len)
{
    const auto& [difX, difY] = directs[dir];
    layout[y][x] = corType;
    y += difY;
    x += difX;

    char sym;
    if (difX != 0)
        sym = '-';
    else
        sym = '|';
    for (int i = 1; i < len; i++)
    {
        layout[y][x] = sym;
        y += difY;
        x += difX;
    }
}

void DrawLine2(int& x, int& y, char corType, char dir, int len)
{
    const auto& [difX, difY] = directs[dir];
    layout2[y].push_back({ x,corType });
    if (difX != 0)
    {
        x += difX * len;
        return;
    }

    y += difY;

    char sym = '|';

    for (int i = 1; i < len; i++)
    {
        layout2[y].push_back({ x,sym });
        y += difY;
    }
}

void OutPutLayout()
{
    for (vector<char>& vc : layout)
    {
        for (char c : vc)
            cout << c;
        cout << endl;
    }
    cout << endl;
    cout << endl;

}

void DrawInpToLayout()
{
    int size = input.size();
    string corne;
    char corType;
    tuple<char, int, string> prev;
    tuple<char, int, string> cur;
    int x = max(-minX, 0);
    int y = max(-minY, 0);
    for (int i = 0; i < size; i++)
    {
        const auto [prevDir, prevLen] = input[(i + size - 1) % size];
        const auto [curDir, curLen] = input[i];
        corne = string(1, prevDir) + string(1, curDir);
        corType = corners[corne];
        DrawLine(x, y, corType, curDir, curLen);
    }
}
void DrawInpToLayout2()
{
    int size = input.size();
    string corne;
    char corType;
    tuple<char, int, string> prev;
    tuple<char, int, string> cur;
    int x = max(-minX, 0);
    int y = max(-minY, 0);
    for (int i = 0; i < size; i++)
    {
        const auto [prevDir, prevLen] = input[(i + size - 1) % size];
        const auto [curDir, curLen] = input[i];
        corne = string(1, prevDir) + string(1, curDir);
        corType = corners[corne];
        DrawLine2(x, y, corType, curDir, curLen);
    }
}

void FindOffSets()
{
    int x = 0;
    int y = 0;
    minX = 0;
    minY = 0;
    maxX = 0;
    maxY = 0;
    for (tuple<char, int>& tu : input)
    {
        const auto [dir, len] = tu;
        x += directs[dir].first * len;
        y += directs[dir].second * len;
        minX = min(minX, x);
        minY = min(minY, y);
        maxX = max(maxX, x);
        maxY = max(maxY, y);
    }
}

long long CountFilled()
{
    int parity;
    long long filled = 0;
    for (vector<char>& vc : layout)
    {
        parity = 0;
        for (char c : vc)
        {
            if (c == '|' || c == 'F' || c == '7')
                parity++;

            if (c != '.')
                filled++;
            else if (parity % 2 == 1)
                filled++;
        }

    }
    return filled;
}

long long CountFilled2()
{
    int parity;
    long long filled = 0;
    for (vector<pair<int, char>>& vp : layout2)
    {
        std::sort(vp.begin(), vp.end());
        parity = 0;
        long long lastX = -1;

        for (pair<int, char> pair : vp)
        {
            long long x = pair.first;
            char c = pair.second;

            if (c == '|' || c == 'F' || c == '7')
                parity++;

            if (parity % 2 == 1)
            {
                if (c == '|' || c == 'J' || c == '7')
                {
                    lastX = x;
                }
            }
            else
            {
                if (c == '|' || c == 'L' || c == 'F')
                {
                    filled +=(  x-lastX-1);
                }
            }
        }
    }
    return filled;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    long long sum = 0;

    for (string& str : vec)
    {
        vector<string> vstr = StringWithSymbolIntoStrings(str, " ");
        int val = stoi(vstr[1]);
        input.push_back({ vstr[0][0],val });
        sum += val;
    }

    FindOffSets();
    CreateLayoutDims();
    DrawInpToLayout();
    //OutPutLayout();
    sum = CountFilled();
    return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
    long long sum = 0;


    /*for (string& str : vec)
    {
        vector<string> vstr = StringWithSymbolIntoStrings(str, " ");

        char ch;
        switch (vstr[2][vstr[2].size() - 2])
        {
        case '0':
            ch = 'R';
            break;
        case '1':
            ch = 'D';
            break;
        case '2':
            ch = 'L';
            break;
        case '3':
            ch = 'U';
            break;
        }
        string hexValue = vstr[2].substr(2, 5);
        std::istringstream converter{ hexValue };
        long long int value = 0;
        converter >> std::hex >> value;
        input.push_back({ ch,value });
    }*/
    input.clear();
    for (string& str : vec)
    {
        vector<string> vstr = StringWithSymbolIntoStrings(str, " ");
        int val = stoi(vstr[1]);
        input.push_back({ vstr[0][0],val });
        sum += val;
    }

    FindOffSets();
    CreateLayoutDims2();
    DrawInpToLayout2();
    sum += CountFilled2();
    layout2;
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