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

using namespace std;

std::vector< std::vector<bool>> attended;

int nOfRows;
int nOfCols;

pair<int, int> FindS(std::vector<std::string>& vec)
{
	pair<int, int> res;
	for (int i = 0; i < nOfRows; i++)
	{
		attended.push_back({});
		for (int j = 0; j < nOfCols; j++)
		{
			attended.back().push_back(false);
			if (vec[i][j] == 'S')
				res = { j,i };
		}
	}
	return res;
}


pair<int, int> FindS2(std::vector<std::string>& vec)
{
	pair<int, int> res;
	for (int i = 0; i < nOfRows; i++)
	{
		attended.push_back({});
		for (int j = 0; j < nOfCols; j++)
		{
			attended.back().push_back(false);
			if (i > nOfRows / 3 && i< nOfRows * 2 / 3 && j>nOfCols / 3 && j < nOfCols * 2 / 3)
				if (vec[i][j] == 'S')
					res = { j,i };
		}
	}
	return res;
}

void OutPutLayout(std::vector<std::string>& vec)
{
	for (string& s : vec)
	{
		for (char c : s)
			cout << c;
		cout << endl;
	}
	cout << endl;
	cout << endl;

}

void OutPutLayoutFile(std::vector<std::string>& vec)
{
	std::ofstream out("output.txt");
	for (string& s : vec)
	{
		for (char c : s)
			out << c;
		out << endl;
	}
	out << endl;
	out << endl;

}

int ManhatDist(int x0, int y0, int x1, int y1)
{
	return abs(x0 - x1) + abs(y0 - y1);
}

void BfsStep(deque<pair<int, int>>& from, deque<pair<int, int>>& into, std::vector<std::string>& vec, int ii, int xS, int yS, int dist)
{
	while (!from.empty())
	{
		const auto [x, y] = from.front();
		from.pop_front();

		attended[y][x] = true;

		vec[y][x] = (ii % 10) + '0';
		/*if (ii % 2 == 0)
			vec[y][x] = 'O';
		else
			vec[y][x] = 'X';
			*/
		if (x + 1 < nOfCols && !attended[y][x + 1] && vec[y][x + 1] != '#' && ManhatDist(x + 1, y, xS, yS) <= dist)
		{
			into.push_back({ x + 1,y });
			attended[y][x + 1] = true;
		}
		if (x - 1 >= 0 && !attended[y][x - 1] && vec[y][x - 1] != '#' && ManhatDist(x - 1, y, xS, yS) <= dist)
		{
			into.push_back({ x - 1,y });
			attended[y][x - 1] = true;
		}
		if (y + 1 < nOfRows && !attended[y + 1][x] && vec[y + 1][x] != '#' && ManhatDist(x, y + 1, xS, yS) <= dist)
		{
			into.push_back({ x ,y + 1 });
			attended[y + 1][x] = true;

		}
		if (y - 1 >= 0 && !attended[y - 1][x] && vec[y - 1][x] != '#' && ManhatDist(x, y - 1, xS, yS) <= dist)
		{
			into.push_back({ x ,y - 1 });
			attended[y - 1][x] = true;
		}
	}
	//if (ii % 2 == 0)
	//	OutPutLayout(vec);
}

std::string MySolution1(std::vector<std::string>& vec)
{
	long long sum = 0;


	nOfRows = vec.size();
	nOfCols = vec.front().size();
	auto const [x, y] = FindS(vec);

	deque<pair<int, int>> from;
	deque<pair<int, int>> into;
	from.push_back({ x,y });
	for (int i = 0; i <= 64; i++)
	{
		if (i % 2 == 0)
			sum += from.size();
		BfsStep(from, into, vec, i, x, y, 1000000);
		from = into;
		into.clear();
	}
	OutPutLayout(vec);

	return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
	long long sum = 1;
	int a = (((-2) % 5) + 5) % 5;



	int times = 23;

	std::vector<std::string> bigger;
	for (int i = 0; i < times; i++)
	{
		for (int j = 0; j < vec.size(); j++)
		{
			string line;
			for (int k = 0; k < times; k++)
			{
				line.append(vec[j]);
			}
			bigger.push_back(line);
		}

	}




	nOfRows = bigger.size();
	nOfCols = bigger.front().size();
	auto const [x, y] = FindS2(bigger);

	deque<pair<int, int>> from;
	deque<pair<int, int>> into;
	from.push_back({ x,y });
	int range = 65 + 131 * 6;
	for (int i = 1; i <= range*2; i++)
	{
		BfsStep(from, into, bigger, i, x, y, range);
		from = into;
		into.clear();

		if (i % 2 == 0)
			sum += from.size();
		if (((i - 65) % 131 )== 0)
			std::cout << sum << "\n";
	}
	OutPutLayoutFile(bigger);

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

	std::string result;
	auto start = std::chrono::high_resolution_clock::now();
	//result = MySolution1(vec);
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