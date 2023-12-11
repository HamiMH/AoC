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
#include "PartOfLine.h"
using namespace std;

const std::set<char> CharToAdd = { '|','F','J','L','7' };

unordered_map<char, map<pair<int, int>, pair<int, int>>>partDirs =
{
	{
		'7',
		{
			{{1,0},{0,1}},
			{{0,-1},{-1,0}}
		}
	},
	{
		'F',
		{
			{{-1,0},{0,1}},
			{{0,-1},{1,0}}
		}
	},
	{
		'L',
		{
			{{-1,0},{0,-1}},
			{{0,1},{1,0}}
		}
	},
	{
		'J',
		{
			{{1,0},{0,-1}},
			{{0,1},{-1,0}}
		}
	},
	{
		'|',
		{
			{{0,1},{0,1}},
			{{0,-1},{0,-1}},
		}
	},
	{
		'-',
		{
			{{1,0},{1,0}},
			{{-1,0},{-1,0}}
		}
	}

};

long long AreaOfPOL2(vector<PartOfLine>pols)
{
	std::sort(pols.begin(), pols.end());

	long long area = 0;

	long long x = 0;
	long long y = -1;
	char ch='0';
	bool inside = false;
	long long add = 0;
	for (PartOfLine& pol : pols)
	{
		if (pol.Y != y)
		{
			y = pol.Y;
			x = pol.X;
			ch = pol.Ch;
			inside = (pol.Ch == '|');
			continue;
		}

		if (pol.Ch == 'F' || pol.Ch == 'L')
		{
			if (inside)
			{
				add = (pol.X - x - 1);
				if(add>0)
					area += add;
			}
			ch = pol.Ch;
		}
		else if (pol.Ch == 'J')
		{
			x = pol.X;
			if (ch == 'F')
				inside = (!inside);
		}
		else if (pol.Ch == '7')
		{
			x = pol.X;
			if (ch == 'L')
				inside = (!inside);
		}
		else
		{

			if (inside)
			{
				add = (pol.X - x - 1);
				if (add > 0)
					area += add;
				inside = false;
			}
			else
			{
					x = pol.X;
					inside = true;;
			}
		}
	}
	return area;
}

long long RunTheMaze2(std::vector<std::string>& vec, pair<int, int> sBegin, pair<int, int> dirOrig)
{
	pair<int, int> dir = dirOrig;
	int posX = sBegin.second;
	int posY = sBegin.first;
	int lenOfPath = 0;
	char c;
	vector<PartOfLine>pols;
	while (vec[posY + dir.second][posX + dir.first] != 'S' || lenOfPath == 0)
	{
		posX += dir.first;
		posY += dir.second;
		c = vec[posY][posX];
		if (partDirs[c].contains(dir))
			dir = partDirs[c][dir];
		else
			return 0;
		if (CharToAdd.contains(c))
			pols.push_back(PartOfLine(posX, posY, c));

		lenOfPath++;
	}
	if (vec.size() > 30)
		pols.push_back(PartOfLine(sBegin.second, sBegin.first, 'J'));
	else
		pols.push_back(PartOfLine(sBegin.second, sBegin.first, 'F'));



	return AreaOfPOL2(pols);
}

int RunTheMaze(std::vector<std::string>& vec, pair<int, int> sBegin, pair<int, int> dir)
{
	int posX = sBegin.second;
	int posY = sBegin.first;
	int lenOfPath = 0;
	char c;
	while (vec[posY + dir.second][posX + dir.first] != 'S' || lenOfPath == 0)
	{
		posX += dir.first;
		posY += dir.second;
		c = vec[posY][posX];
		if (partDirs[c].contains(dir))
			dir = partDirs[c][dir];
		else
			return 0;
		lenOfPath++;
	}
	return lenOfPath;
}


pair<int, int>FindS(std::vector<std::string>& vec)
{
	for (int i = 0; i < vec.size(); i++)
		for (int j = 0; j < vec[0].size(); j++)
			if (vec[i][j] == 'S')
				return { i,j };
}
std::string MySolution1(std::vector<std::string>& vec)
{
	pair<int, int> sBegin = FindS(vec);
	int max = 0;
	int len = 0;
	pair<int, int> dir = { 1,0 };
	len = RunTheMaze(vec, sBegin, dir);
	if (len > max)
		max = len;
	dir = { -1,0 };
	len = RunTheMaze(vec, sBegin, dir);
	if (len > max)
		max = len;
	dir = { 0,1 };
	len = RunTheMaze(vec, sBegin, dir);
	if (len > max)
		max = len;
	dir = { 0,-1 };
	len = RunTheMaze(vec, sBegin, dir);
	if (len > max)
		max = len;
	return std::to_string((max + 1) / 2);

}

std::string MySolution2(std::vector<std::string>& vec)
{
	pair<int, int> sBegin = FindS(vec);
	long long max = 0;
	long long len = 0;
	pair<int, int> dir = { 1,0 };
	len = RunTheMaze2(vec, sBegin, dir);
	if (len > max)
		max = len;
	dir = { -1,0 };
	len = RunTheMaze2(vec, sBegin, dir);
	if (len > max)
		max = len;
	dir = { 0,1 };
	len = RunTheMaze2(vec, sBegin, dir);
	if (len > max)
		max = len;
	dir = { 0,-1 };
	len = RunTheMaze2(vec, sBegin, dir);
	if (len > max)
		max = len;
	return std::to_string(max );
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