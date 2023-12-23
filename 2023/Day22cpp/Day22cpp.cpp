#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <set>
#include <map>
#include <unordered_map>
#include <unordered_set>
#include <boost/algorithm/string.hpp>
#include <numeric>
#include <algorithm>
#include <numeric>
#include <queue>
#include "Brick.h"

using namespace std;

vector<Brick>FinalPlacements;
vector<Brick>BrickIndexed;
unordered_set<int> Fundamentals;
vector<vector<vector<int>>>Space;

unordered_map<int, unordered_set<int>>standsOn;
unordered_map<int, unordered_set<int>>areAbove;
int NOfBricks;

void PlaceBrick(Brick& br)
{
	FinalPlacements.push_back(br);
	for (int z = br.Z.first; z <= br.Z.second; z++)
		for (int y = br.Y.first; y <= br.Y.second; y++)
			for (int x = br.X.first; x <= br.X.second; x++)
				Space[z][y][x] = br.Index;

}
void ResolveBrick(Brick& br)
{

	for (int z = br.Z.first - 1; z > 0; z--)
	{
		for (int y = br.Y.first; y <= br.Y.second; y++)
			for (int x = br.X.first; x <= br.X.second; x++)
			{
				if (Space[z][y][x] != -1)
				{
					PlaceBrick(br);
					return;

				}
			}
		br.Z.first--;
		br.Z.second--;
	}
	PlaceBrick(br);

}


void FindFundamentals()
{
	for (Brick& br : FinalPlacements)
	{
		unordered_set<int>pillars;
		for (int y = br.Y.first; y <= br.Y.second; y++)
			for (int x = br.X.first; x <= br.X.second; x++)
				if (Space[br.Z.first - 1][y][x] != -1)
					pillars.insert(Space[br.Z.first - 1][y][x]);

		if (pillars.size() == 1)
			for (int bi : pillars)
				Fundamentals.insert(bi);
	}

}
void FindPrevious()
{
	for (Brick& br : FinalPlacements)
	{
		for (int y = br.Y.first; y <= br.Y.second; y++)
			for (int x = br.X.first; x <= br.X.second; x++)
				if (Space[br.Z.first - 1][y][x] != -1)
				{
					int tmp = Space[br.Z.first - 1][y][x];
					standsOn[br.Index].insert(tmp);
					areAbove[tmp].insert(br.Index);
				}
	}
}

void ResolveInput(std::vector<std::string>& vec)
{
	int maxX = 0;
	int maxY = 0;
	int maxZ = 0;
	priority_queue<Brick> pq;
	int brickIndex = 0;
	for (string& s : vec)
	{
		pq.push(Brick(s, brickIndex, maxX, maxY, maxZ));
		standsOn[brickIndex] = {};
		areAbove[brickIndex] = {};
		brickIndex++;
	}
	NOfBricks = pq.size();

	vector<int>lineX;
	vector<vector<int>>lineY;
	for (int i = 0; i <= maxX; i++)
		lineX.push_back(-1);
	for (int i = 0; i <= maxY; i++)
		lineY.push_back(lineX);
	for (int i = 0; i <= maxZ; i++)
		Space.push_back(lineY);

	while (!pq.empty())
	{
		Brick br = pq.top(); pq.pop();
		ResolveBrick(br);
	}
}

/*
long long SimulateFalling(int i)
{
	unordered_set<int> removed = { i };
	unordered_set<int> processed = { i };

	deque<int>deq;
	for (int above : areAbove[i])
		deq.push_back(above);

	while (!deq.empty())
	{
		int process = deq.front(); deq.pop_front();
		//if (processed.contains(process))
		//	continue;
		processed.insert(process);
		unordered_set<int> belows = standsOn[process];

		bool allOk = true;
		for (int below : belows)
		{
			if (!removed.contains(below))
				allOk = false;
		}
		if (allOk)
			removed.insert(process);
		for (int above : areAbove[process])
			deq.push_back(above);
	}
	return (long long)(removed.size() - 1);
}
*/

long long SimulateFallingPq(int i)
{
	unordered_set<int> removed = { i };
	unordered_set<int> processed = { i };

	priority_queue<Brick>deq;
	for (int above : areAbove[i])
		deq.push(BrickIndexed[above]);

	while (!deq.empty())
	{
		int process = deq.top().Index; deq.pop();
		if (processed.contains(process))
			continue;
		processed.insert(process);
		unordered_set<int> belows = standsOn[process];

		bool allOk = true;
		for (int below : belows)
		{
			if (!removed.contains(below))
				allOk = false;
		}
		if (allOk)
			removed.insert(process);
		for (int above : areAbove[process])
			deq.push(BrickIndexed[ above]);
	}
	return (long long)(removed.size() - 1);
}

std::string MySolution1(std::vector<std::string>& vec)
{
	ResolveInput(vec);
	FindFundamentals();
	return std::to_string(NOfBricks - Fundamentals.size());

}

std::string MySolution2(std::vector<std::string>& vec)
{

	ResolveInput(vec);
	FindPrevious();
	long long sum = 0;

	for (int i = 0; i < NOfBricks; i++)
		BrickIndexed.push_back({});

	for (int i = 0; i < NOfBricks; i++)
		BrickIndexed[FinalPlacements[i].Index] = FinalPlacements[i];

	for (int i = 0; i < NOfBricks; i++)
	{
		//long long tmp = SimulateFalling(i);
		long long tmp = SimulateFallingPq(i);
		sum += tmp;
	}
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


	FinalPlacements.clear();
	
	BrickIndexed.clear();

	Fundamentals.clear();
	Space.clear();
	standsOn.clear();
	areAbove.clear();

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