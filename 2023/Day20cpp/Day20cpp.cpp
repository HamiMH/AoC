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
#include "Node.h"
#include <numeric>

using namespace std;

vector<int> Diff(vector<int> vi)
{
	vector<int>out = {};
	out.push_back(vi[0]);
	for (int i = 0; i < vi.size() - 1; i++)
		out.push_back(vi[i + 1] - vi[i]);
	return out;
}

vector<int> StatFrom(Graph graph, string name)
{
	vector<int >vll= graph.Nodes[name]->HighPulseTimes;
	vector<int>out = {};
	out.push_back(vll[0]);
	for (int i = 1; i < vll.size() - 2; i++)
		out.push_back(vll[i + 1] - vll[i]);
	return out;
}

std::string MySolution1(std::vector<std::string>& vec)
{
	long long sum = 0;

	Graph graph(vec);

	deque<tuple<string, string, int>> que = {};

	long long lowPulses = 0;
	long long highPulses = 0;

	for (int i = 0; i < 1000; i++)
	{
		SimulTime++;
		que.push_front({ "broadcaster","broadcaster",LOWPULSE });
		while (!que.empty())
		{
			tuple<string, string, int> tup = que.front();
			que.pop_front();
			auto const& [from, to, pulse] = tup;
			if (pulse == HIGHPULSE)highPulses++;
			if (pulse == LOWPULSE)lowPulses++;
			graph.Nodes[to]->ProcessSignal(tup, que);
		}
	}


	sum = lowPulses * highPulses;
	return std::to_string(sum);

}

std::string MySolution2(std::vector<std::string>& vec)
{

	Graph graph(vec);

	deque<tuple<string, string, int>> que = {};
	SimulTime = 0;


	for (int i = 0; i < 100000; i++)
	{
		SimulTime++; //now here
		que.push_front({ "broadcaster","broadcaster",LOWPULSE });
		while (!que.empty())
		{
			//SimulTime++;  previously here
			tuple<string, string, int> tup = que.front();
			que.pop_front();
			auto const& [from, to, pulse] = tup;
			graph.Nodes[to]->ProcessSignal(tup, que);
		}
	}

	/*vector<int> ln = graph.Nodes["ln"]->HighPulseTimes;
	vector<int> db = graph.Nodes["db"]->HighPulseTimes;
	vector<int> rq = graph.Nodes["vq"]->HighPulseTimes;
	vector<int> tf = graph.Nodes["tf"]->HighPulseTimes;
	vector<int> Dln = Diff(ln);
	vector<int> Ddb = Diff(db);
	vector<int> Drq = Diff(rq);
	vector<int> Dtf = Diff(tf);*/


	long long res = lcm((long long)graph.Nodes["ln"]->HighPulseTimes.front(), (long long)graph.Nodes["db"]->HighPulseTimes.front());
	res = lcm(res, (long long)graph.Nodes["vq"]->HighPulseTimes.front());
	res = lcm(res, (long long)graph.Nodes["tf"]->HighPulseTimes.front());
	long long testvbcv = (long long)graph.Nodes["ln"]->HighPulseTimes.front()* (long long)graph.Nodes["db"]->HighPulseTimes.front()* (long long)graph.Nodes["vq"]->HighPulseTimes.front()* (long long)graph.Nodes["tf"]->HighPulseTimes.front();

	return std::to_string(res);
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