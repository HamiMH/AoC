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


//vector<vector<vector<map<pair<int, int>, int>>>> MEMO;

vector<vector<int>>lavaMap;

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

const int WRONG = 1000000000;




priority_queue<pair<int, tuple<int,int, int>>, vector<pair<int, tuple<int,int, int>>>, greater<pair<int, tuple<int,int, int>>>>pq;
vector<vector<vector<int>>>distMap;

void CalculateMaze()
{
	distMap[RIGHT][nOfRows - 1][nOfCols - 1] = 0;
	distMap[DOWN][nOfRows - 1][nOfCols - 1] = 0;
	distMap[LEFT][nOfRows - 1][nOfCols - 1] = 0;
	distMap[UP][nOfRows - 1][nOfCols - 1] = 0;
	pq.push({ 0,{LEFT,nOfCols - 1,nOfRows - 1} });
	pq.push({ 0,{UP,nOfCols - 1,nOfRows - 1} });

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
			for (int lenOfSkip = 1; lenOfSkip <= 3; lenOfSkip++) 
			{
				x += directs[dirAfterTurn].first;
				y += directs[dirAfterTurn].second;
				if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
					continue;
				efford += lavaMap[y][x];

				if (distMap[dirCoord][yCoord][xCoord] + efford < distMap[dirCoord][y][x])
				{
					distMap[dirCoord][y][x] = distMap[dirCoord][yCoord][xCoord] + efford;
					pq.push({ distMap[dirCoord][y][x] ,{dirCoord,x,y} });
				}
			}
		}
		/*for (int curDir = 0; curDir < NDIRS; curDir++)
		{
			x = xCoord + directs[curDir].first;
			y = yCoord + directs[curDir].second;
			if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
				continue;

			if (distMap[coord.second][coord.first] + lavaMap[y][x] < distMap[y][x])
			{
				distMap[y][x] = distMap[coord.second][coord.first] + lavaMap[y][x];
				pq.push({ distMap[y][x] ,{x,y} });
			}
		}
		*/
	}

}



std::string MySolution1(std::vector<std::string>& vec)
{
	int min = 0;

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
		for (int i = 0; i < NDIRS; i++)
		{
			
			//MEMO[i].push_back(tmp2);
			distMap.push_back({});
			distMap[i].push_back(tmp3);
		}
	}
	nOfRows = lavaMap.size();
	nOfCols = lavaMap.front().size();

	CalculateMaze();

	return std::to_string(minResults - lavaMap[0][0]);

}

std::string MySolution2(std::vector<std::string>& vec)
{
	int sum = 0;

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


/*
map<tuple<int, int, int, int>, int> MEMO;
vector<vector<map<pair<int, int>, int>>> MEMO2;

vector<vector<int>>lavaMap;
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

const int WRONG = 1000000000;


int NavigateMaze(int x, int y, int prDir, int prSteps)
{
	if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
		return WRONG;

	if (x == nOfCols - 1 && y == nOfRows - 1)
		return lavaMap[y][x];

	if (prSteps > 3)
		return WRONG;
	if (MEMO.contains({ x, y, prDir, prSteps }))
		return MEMO[{x, y, prDir, prSteps}];

	int steps;
	int min = WRONG;
	int tmp;
	for (int curDir = 0; curDir < NDIRS; curDir++)
	{
		if (curDir == prDir)
			steps = prSteps + 1;
		else
			steps = 1;

		tmp = lavaMap[y][x] + NavigateMaze(x + directs[curDir].first, y + directs[curDir].second, curDir, steps);
		if (tmp < min)
			min = tmp;
	}
	MEMO[{x, y, prDir, prSteps}] = min;
	return min;
}


void NavigateMaze2(int x, int y, int prDir, int prSteps, int value)
{
	if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
		return;
	if (prSteps > 3)
		return;

	value += lavaMap[y][x];

	for (int i = 1; i <= prSteps; i++)
	{
		if (MEMO.contains({ x, y, prDir, i }))
			if (MEMO[{x, y, prDir, i}] <= value)
				return;
	}

	MEMO[{x, y, prDir, prSteps}] = value;

	if (x == nOfCols - 1 && y == nOfRows - 1)
	{
		if (minResults > value)
			minResults = value;
	}


	int steps;
	int tmp;
	for (int curDir = 0; curDir < NDIRS; curDir++)
	{
		if ((curDir - prDir) == 2 || (curDir - prDir) == -2)
			continue;
		if (curDir == prDir)
			steps = prSteps + 1;
		else
			steps = 1;

		NavigateMaze2(x + directs[curDir].first, y + directs[curDir].second, curDir, steps, value);
	}
}


void NavigateMaze3(int x, int y, int prDir, int prSteps, int value)
{
	if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
		return;
	if (prSteps > 3)
		return;

	value += lavaMap[y][x];

	if (x == nOfCols - 1 && y == nOfRows - 1)
	{
		if (minResults > value)
			minResults = value;
		return;
	}


	int steps;
	int tmp;
	bool toSkip;

	for (int curDir = 0; curDir < NDIRS; curDir++)
	{
		if ((curDir - prDir) == 2 || (curDir - prDir) == -2)
			continue;
		if (curDir == prDir)
			steps = prSteps + 1;
		else
			steps = 1;

		toSkip = false;
		for (pair<const pair<int, int>,int>& pai : MEMO2[y][x])
		{
			const pair<int, int>key = pai.first;
			int valPair= pai.second;

			if (key.first != curDir ||steps<= key.second)
			{
				if (valPair < value)
				{
					toSkip = true;
					break;
				}
			}
		}
		if (toSkip)
			continue;

		//MEMO[{x, y, prDir, prSteps}] = value;
		MEMO2[y][x][{prDir, prSteps}] = value;

		NavigateMaze3(x + directs[curDir].first, y + directs[curDir].second, curDir, steps, value);
	}
}


priority_queue<pair<int, pair<int, int>>, vector<pair<int, pair<int, int>>>, greater<pair<int, pair<int, int>>>>pq;
vector<vector<int>>distMap;

void PrecalculateMaze()
{
	distMap[nOfRows - 1][nOfCols - 1] = 0;
	pq.push({ 0,{nOfCols - 1,nOfRows - 1} });

	while (!pq.empty())
	{
		const pair<int, pair<int, int>> top = pq.top();
		pq.pop();
		int dis = top.first;
		pair<int, int>coord = top.second;
		if (dis > distMap[coord.second][coord.first])
			continue;

		int x, y;
		for (int curDir = 0; curDir < NDIRS; curDir++)
		{
			x = coord.first + directs[curDir].first;
			y = coord.second + directs[curDir].second;
			if (x < 0 || y < 0 || x >= nOfCols || y >= nOfRows)
				continue;

			if (distMap[coord.second][coord.first] + lavaMap[y][x] < distMap[y][x])
			{
				distMap[y][x] = distMap[coord.second][coord.first] + lavaMap[y][x];
				pq.push({ distMap[y][x] ,{x,y} });
			}
		}
	}

}



std::string MySolution1(std::vector<std::string>& vec)
{
	int min = 0;

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
		MEMO2.push_back(tmp2);
		distMap.push_back(tmp3);
	}
	nOfRows = lavaMap.size();
	nOfCols = lavaMap.front().size();

	NavigateMaze2(0, 0, START, 0,0);
	//NavigateMaze3(0, 0, START, 0, 0);
	PrecalculateMaze();

	return std::to_string(minResults - lavaMap[0][0]);

}

std::string MySolution2(std::vector<std::string>& vec)
{
	int sum = 0;

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
*/