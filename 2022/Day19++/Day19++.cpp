// Day19++.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#pragma once
#include <iostream>
#include <chrono>
#include "BluePrint.h"

int GetResult1(vector<string>& inputCol)
{
    BluePrint bp;
    int result = 0;
    int index = 1;
    for (string& str : inputCol)
    {
        
        bp.SetBP(str, 24);
        result += bp.GetBestOresDFS(24) * index;
        index++;
    }
    return result;
}


int GetResult2(vector<std::string>& inputCol)
{
    BluePrint bp;
    int result = 1;
    int index = 0;
    for (std::string& str : inputCol)
    {
        if (index == 3)
            break;
        bp.SetBP(str, 32);
        result *= bp.GetBestOresDFS(32);
        index++;
    }
    return result;
}

int main()
{
    std::string input;


    vector<string> list;
    while (getline(cin, input))
    {
        if (input == "" || input.empty())
            break;
        list.push_back(input);
    }
    //int result = GetResult1(list);
    std::chrono::steady_clock::time_point begin = std::chrono::steady_clock::now();
    int result = GetResult2(list);
    std::chrono::steady_clock::time_point end = std::chrono::steady_clock::now();

    std::cout << result << std::endl;;
    std::cout << "Time difference = " << std::chrono::duration_cast<std::chrono::microseconds>(end - begin).count() << "[µs]" << std::endl;
    std::cout << "Time difference = " << std::chrono::duration_cast<std::chrono::milliseconds>(end - begin).count() << "[ms]" << std::endl;
}
// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
