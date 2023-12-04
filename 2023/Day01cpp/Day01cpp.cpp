// Day01cpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//




#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <string_view>


void Update(int& first, int& last, int& tmp) 
{
    if (first == -1)
        first = tmp;
    last = tmp;
}

std::string MySolution(std::vector<std::string>& vec)
{
    int first, last=0, tmp=0;
    int sum = 0;
    for (std::string& str : vec)
    {
        first = -1;

        for (char c : str)
        {
            if (c >= '0' && c <= '9')
            {
                tmp = c - '0';
                Update(first, last, tmp);
            }
        }
        sum += (10 * first + last);
    }
    return std::to_string(sum);
}



std::vector<std::pair<std::string_view, int>> detect =
{
    {std::string_view("one"),1},
    {std::string_view("two"),2},
    {std::string_view("three"),3},
    {std::string_view("four"),4},
    {std::string_view("five"),5},
    {std::string_view("six"),6},
    {std::string_view("seven"),7},
    {std::string_view("eight"),8},
    {std::string_view("nine"),9},
};



std::string MySolution2(std::vector<std::string>& vec)
{
    int first, last, tmp,len;
    int sum = 0;
    char c;
    std::string_view sw;
    for (std::string& str : vec)
    {
        sw = str;
        first = -1;
        len = sw.length();
        for (int i=0;i< len;i++)
        {
            c = sw[i];
            if (c >= '0' && c <= '9')
            {
                tmp = c - '0';
                Update(first, last, tmp);
            }
            else
            {
                for (std::pair<std::string_view, int>& det : detect)
                {
                    std::string_view substr=sw.substr(i,det.first.length());
                    if(substr.compare(det.first)==0)
                        Update(first, last, det.second);
                }

            }
        }
        sum += (10 * first + last);

        std::cout << std::to_string(10 * first + last)<<std::endl;
    }

    return std::to_string(sum);
}

int main()
{
    std::string in;
    std::cin >> in;
    //std::ifstream file("input.txt");
    std::ifstream file(in+".txt");
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
    std::cout << "Result: "<< result << "\n";
    if(microseconds>1000000000)
        std::cout << microseconds/1000000 << "s\n";
    else if(microseconds>1000000)
        std::cout << microseconds/1000 << "ms\n";
    else
        std::cout << microseconds << "us\n";
        //std::cout << microseconds << "µs\n";
}
