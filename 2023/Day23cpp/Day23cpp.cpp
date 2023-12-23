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
int nOfCols;
int nOfRows;

const int START = -10;
const int RIGHT = 0;
const int DOWN = 1;
const int LEFT = 2;
const int UP = 3;
const int NDIRS = 4;

std::vector<std::string> layout;

//set<pair<pair<int, int>, pair<int, int>>, int>edges;

map< pair<int, int>, map<pair<int, int>, int>>edges;

vector<pair<int, int>>directs =
{
	{1,0},
	{0,1},
	{-1,0},
	{0,-1},
};
vector<char>okSymbol =
{
	'>',
	'v',
	'<',
	'.',
};

set<pair<pair<int, int>, int>>WentFrom;


bool BegFile(pair<int, int> pos)
{
	return ((pos.first == 1 && pos.second == 0));
}
bool EndFile(pair<int, int> pos)
{
	return ((pos.first == nOfCols - 2 && pos.second == nOfRows - 1)) ;
}
bool IsCrossRoad(pair<int, int> pos)
{
	int num = 0;
	for (int i = 0; i < NDIRS; i++)
	{
		if (layout[pos.second + directs[i].second][pos.first + directs[i].first] != '#')
			num++;
	}
	if (num > 2)
		return true;
	else
		return false;
}

void GoFrom(pair<int, int> posIn, int dir,bool ignoreIce)
{
	pair<int, int> pos = posIn;
	int pathLen = 0;
	if (WentFrom.contains({ pos,dir }))
		return;
	WentFrom.insert({ pos,dir });

	pos.first += directs[dir].first;
	pos.second += directs[dir].second;
	pathLen++;
	while (!BegFile(pos) && !EndFile(pos) && !IsCrossRoad(pos))
	{
		/*if(pos.first==nOfCols-2 &&pos.second==nOfRows-1)
		break;*/
		for (int i = 0; i < NDIRS; i++)
		{
			if (i == ((dir + 2) % NDIRS))
				continue;
			if (layout[pos.second + directs[i].second][pos.first + directs[i].first] != '#')
			{
				if (layout[pos.second][pos.first] == '.' || layout[pos.second][pos.first] == okSymbol[i] || ignoreIce)
				{
					pos.first += directs[i].first;
					pos.second += directs[i].second;
					pathLen++;
					dir = i;
					break;
				}
				else
				{
					return;
				}
			}
		}
	}
	if (edges[posIn].contains(pos))
	{
		if (edges[posIn][pos] < pathLen)
			edges[posIn][pos] = pathLen;
	}
	else
	{
		edges[posIn][pos] = pathLen;
	}

	if (!BegFile(pos) && !EndFile(pos))
		for (int i = 0; i < NDIRS; i++)
		{
			if (i == ((dir + 2) % NDIRS))
				continue;
			if (layout[pos.second + directs[i].second][pos.first + directs[i].first] != '#')
			{
				GoFrom(pos, i,ignoreIce);
			}
		}
}

void GoMap(bool ignoreIce)
{
	pair<int, int>pos = { 1,0 };
	int dir = DOWN;
	WentFrom.insert({ pos ,UP});

	GoFrom(pos, dir, ignoreIce);
}



map<pair<pair<int, int>,int>, int>locMaxR;
set<pair<int, int>>attendedR;

map<pair<int, int>, int>pairToIndex;
map< int, pair<int, int>>indexToPair;

void FindMaxRecursive(pair<int, int>& now,int bmIn)
{
	for (pair<pair<int, int>, int>edge : edges[now])
	{
		pair<int, int>to = edge.first;
		int len = edge.second;
		int bmOut = bmIn | (1 << pairToIndex[to]);
		if (attendedR.contains(to))
			continue;

		if (locMaxR.contains({ to,bmOut }) && locMaxR[{to, bmOut }] >= locMaxR[{now, bmIn }] + len)
			continue;
		locMaxR[{to, bmOut }] = locMaxR[{now, bmIn }] + len;

		attendedR.insert(to);
		FindMaxRecursive(to,bmOut);
		attendedR.erase(to);
	}
}

long long CallFindMaxRecursive()
{
	int i = 0;
	for (pair< const pair<int, int>, map<pair<int, int>, int>> &ppp : edges)
	{
		indexToPair[i] = ppp.first;
		pairToIndex[ppp.first] = i;
		i++;
	}

	pair<int, int> first = { 1, 0 }; 
	int bm = 1 << pairToIndex[first];
	locMaxR[{first,bm}] = 0;
	attendedR.insert(first);
	FindMaxRecursive(first, bm);

	pair<int, int>endPair = { nOfCols - 2, nOfRows - 1 };
	int maxVal = 0;
	for (pair < const pair<pair<int, int> ,int>, int> pai : locMaxR)
	{
		if (pai.first.first == endPair)
		{
			if (pai.second > maxVal)
				maxVal = pai.second;
		}
	}
	return maxVal;
}

long long FindMax()
{
	map<pair<int, int>, int>locMax;
	locMax[{1, 0}] = 0;

	deque<pair<int, int>>deq = { {1,0} };
	while (!deq.empty())
	{
		pair<int, int> now = deq.front(); deq.pop_front();
		for (pair<pair<int, int>, int>edge : edges[now])
		{
			pair<int, int>to = edge.first;
			int len = edge.second;
			if (locMax.contains(to) && locMax[to] >= locMax[now] + len)
				continue;
			locMax[to] = locMax[now] + len;

			/*for (pair<pair<int, int>, int>edgeTo : edges[to])
				deq.push_back(edgeTo.first);*/
			deq.push_back(to);
		}
	}




	return locMax[{ nOfCols - 2, nOfRows - 1}];
}

std::string MySolution1(std::vector<std::string>& vec)
{
	long long sum = 0;
	layout = vec;
	nOfCols = layout.front().size();
	nOfRows = layout.size();
	GoMap(false);

	sum = FindMax();

	return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{
	long long sum = 0;
	layout = vec;
	nOfCols = layout.front().size();
	nOfRows = layout.size();
	WentFrom.clear();
	edges.clear();

	GoMap(true);

	sum = CallFindMaxRecursive();

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