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
#include <queue>
using namespace std;



int minResults = 1000000000;
int nOfCols;
int nOfRows;

const int START = -10;
const int RIGHT = 0;
const int DOWN = 1;
const int LEFT = 2;
const int UP = 3;
const int NDIRS = 4;

vector<pair<int, int>>directs =
{
	{1,0},
	{0,1},
	{-1,0},
	{0,-1},
};

vector<vector<int>>lavaMap;
priority_queue<pair<int, tuple<int,int, int>>, vector<pair<int, tuple<int,int, int>>>, greater<pair<int, tuple<int,int, int>>>>pq;
vector<vector<vector<int>>>distMap;

void CalculateMaze(int minDist,int maxDist)
{
	distMap[RIGHT][0][0] = 0;
	distMap[DOWN][0][0] = 0;
	distMap[LEFT][0][0] = 0;
	distMap[UP][0][0] = 0;
	pq.push({ 0,{LEFT,0,0} });
	pq.push({ 0,{UP,0,0} });

	while (!pq.empty())
	{
		const pair<int, tuple<int,int, int>> top = pq.top();
		pq.pop();
		int dis = top.first;
		auto const&[dirCoord,xCoord,yCoord] = top.second;
		if (dis > distMap[dirCoord][yCoord][xCoord])
			continue;

		vector<int>dirAfterTurnAllTurns = { (dirCoord + 1 + NDIRS) % NDIRS ,(dirCoord - 1 + NDIRS) % NDIRS };
		int x, y,efford;
		for (int dirAfterTurn : dirAfterTurnAllTurns)
		{
			x = xCoord;
			y = yCoord;
			efford = 0;
			for (int lenOfSkip = 1; lenOfSkip <= maxDist; lenOfSkip++) 
			{
				x += directs[dirAfterTurn].first;
				y += directs[dirAfterTurn].second;
				if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
					break;;
				efford += lavaMap[y][x];

				if (lenOfSkip < minDist)continue;
				if (distMap[dirCoord][yCoord][xCoord] + efford < distMap[dirAfterTurn][y][x])
				{
					distMap[dirAfterTurn][y][x] = distMap[dirCoord][yCoord][xCoord] + efford;
					pq.push({ distMap[dirAfterTurn][y][x] ,{dirAfterTurn,x,y} });
				}
			}
		}
	}

}



void OutPutLayout(vector<vector<int>>inputVec)
{
	for (vector<int>& vi : inputVec)
	{
		for (int i : vi)
		{
			cout << i;
			cout << ",";
		}
		cout << endl;
	}
	cout << endl;
	cout << endl;
	cout << endl;

}

void PreprateMap(std::vector<std::string>& vec)
{
	distMap.clear();
	lavaMap.clear();
	vector<vector<int>>preDistMap;
	for (string& s : vec)
	{
		vector<int>tmp;
		vector<map<pair<int, int>, int>> tmp2;
		vector<int>tmp3;
		for (char c : s)
		{
			tmp.push_back(c - '0');
			tmp2.push_back({});
			tmp3.push_back(1000000000);
		}
		lavaMap.push_back(tmp);
		preDistMap.push_back(tmp3);
	}

	for (int i = 0; i < NDIRS; i++)
	{
		distMap.push_back(preDistMap);
	}
	nOfRows = lavaMap.size();
	nOfCols = lavaMap.front().size();
}
int GetMinimalEndValue()
{
	int minV = 1000000000;
	for (int i = 0; i < NDIRS; i++)
	{
		minV = std::min(minV, distMap[i].back().back());
	}
	return minV;
}

std::string MySolution1(std::vector<std::string>& vec)
{
	PreprateMap(vec);
	CalculateMaze(1,3);
	return std::to_string(GetMinimalEndValue());

}

std::string MySolution2(std::vector<std::string>& vec)
{
	PreprateMap(vec);
	CalculateMaze(4, 10);
	return std::to_string(GetMinimalEndValue());
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