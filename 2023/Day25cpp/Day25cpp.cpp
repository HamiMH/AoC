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
#include "Node.h"

using namespace std;

void ResetGraph(Graph& graph)
{
	for (pair<const string, Node>& pai0 : graph.Nodes)
		for (pair<const string, int>& pa : pai0.second.AdjencedNodes)
			pa.second = 0;
}

set<string> attended;

deque<string> DfsTravel(Graph& graph, string from, string into)
{
	if (from == into)
		return { from };
	for (pair<const string, int>& pa : graph.Nodes[from].AdjencedNodes)
	{
		if (pa.second != 0) continue;
		if (attended.contains(pa.first))continue;
		attended.insert(pa.first);
		deque<string> tmp = DfsTravel(graph, pa.first, into);
		if (!tmp.empty())
		{
			tmp.push_front(from);
			return tmp;
		}
	}
	return {};
}

int FindMaxFlow(Graph& graph, string from, string into)
{
	ResetGraph(graph);
	int iter = 0;
	while (true)
	{
		attended.clear();
		attended.insert(from);
		deque<string> deq = DfsTravel(graph, from, into);
		if (deq.empty())
			break;

		string poiFrom = deq.front(); deq.pop_front();
		string poiInto;

		while (!deq.empty())
		{
			poiInto = deq.front(); deq.pop_front();
			graph.Nodes[poiFrom].AdjencedNodes[poiInto] = 1;
			graph.Nodes[poiInto].AdjencedNodes[poiFrom] = 0;
			poiFrom = poiInto;
		}

		iter++;
	}


	return iter;
}

int SubGraphSize(Graph& graph, string from)
{
	set<string> locAttended;
	deque<string> deq;
	deq.push_front(from);
	locAttended.insert(from);

	string tmp;
	int size = 1;
	while (!deq.empty())
	{
		tmp = deq.back(); deq.pop_back();
		for (pair<const string, int>& pa : graph.Nodes[tmp].AdjencedNodes)
		{
			if (pa.second != 0) continue;
			if (locAttended.contains(pa.first))continue;
			locAttended.insert(pa.first);
			deq.push_front(pa.first);
			size++;
		}
	}
	return size;
}

std::string MySolution1(std::vector<std::string>& vec)
{
	long long sum = 0;

	Graph graph(vec);
	int graphSize = graph.Nodes.size();

	for (pair<const string, Node>& pai1 : graph.Nodes)
	{
		for (pair<const string, Node>& pai2 : graph.Nodes)
		{
			if (pai1.first == pai2.first)
				continue;
			
			int disconnect = FindMaxFlow(graph, pai1.first, pai2.first);
			if (disconnect != 3)continue;
			int sizeSub = SubGraphSize(graph, pai1.first);
			return std::to_string(sizeSub * (graphSize - sizeSub));

			//std::cout << pai1.first << " " << pai2.first <<" " << disconnect << "" << endl;
			//std::cout << sizeSub << " " << graphSize - sizeSub << endl;
			//std::cout << (sizeSub * (graphSize - sizeSub)) << endl;
		}
	}


	/*for (pair<const string, Node>& nod : graph.Nodes)
	{
		std::cout << nod.first << " " << nod.second.AdjencedNodes.size() << "" << endl;
	}*/

	return std::to_string(sum);

}


std::string MySolution2(std::vector<std::string>& vec)
{
	long long sum = 0;
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