#pragma once

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
#include <numeric>
#include "Line.h"

using namespace std;
vector<Line>Lines;

//long long MinP = 7;
//long long MaxP = 27;
long double MinP = 200000000000000.0L;
long double MaxP = 400000000000000.0L;

long long FindIntersect2D()
{
	long long sum = 0;
	int size = Lines.size();
	for (int i = 0; i < size; i++)
		for (int j = i + 1; j < size; j++)
			if (Lines[i].IntersectLine2Dv2(Lines[j], MinP, MaxP))
				sum++;
	cout << "diffs:" << diffs << endl;
	return sum;
}

void ResolveInput(std::vector<string>& vec)
{
	for (string& s : vec)
	{
		Lines.push_back(Line(s));
	}
}
std::string MySolution1(std::vector<std::string>& vec)
{
	long long sum = 0;

	ResolveInput(vec);
	sum = FindIntersect2D();


	return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
	long long sum = 0;
	return std::to_string(sum);
}

int main()
{
	cout << "sizeof( double):" << sizeof(double) << endl;
	cout << "sizeof(long double):" << sizeof(long double) << endl;
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