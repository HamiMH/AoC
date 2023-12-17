#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <set>
#include <map>
#include <unordered_set>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
#include <numeric>
#include <algorithm>

using namespace std;


map<char, map<pair<int, int>, vector<pair<int, int>>>>dirs =
{
	{
		'-',{ 
			{{1,0},{{1,0}}},
			{{-1,0},{{-1,0}}},
			{{0,1},{{-1,0},{1,0}}},
			{{0,-1},{{-1,0},{1,0}}},
			}
	},
	{
		'|',{ 
			{{0,1},{{0,1}}},
			{{0,-1},{{0,-1}}},
			{{1,0},{{0,-1},{0,1}}},
			{{-1,0},{{0,-1},{0,1}}},
			}
	},
	{
		'/',{ 
			{{0,1},{{-1,0}}},
			{{0,-1},{{1,0}}},
			{{1,0},{{0,-1}}},
			{{-1,0},{{0,1}}},
			}
	},
	{
		'\\',{ 
			{{0,1},{{1,0}}},
			{{0,-1},{{-1,0}}},
			{{1,0},{{0,1}}},
			{{-1,0},{{0,-1}}},
			}
	},
	{
		'.',{ 
			{{0,1},{{0,1}}},
			{{0,-1},{{0,-1}}},
			{{1,0},{{1,0}}},
			{{-1,0},{{-1,0}}},
			}
	}
};


long long NavigateMaze(std::vector<std::string>& vec, pair<pair<int, int>, pair<int, int>> firstStep)
{
	deque<pair<pair<int, int>, pair<int, int>>>deq;
	set<pair<pair<int, int>, pair<int, int>>>attended;
	set<pair<int, int>> attendedPos;
	deq.push_back(firstStep);

	while (true)
	{
		if (deq.empty())
			break;
		pair<pair<int, int>, pair<int, int>> frontElem = deq.front();
		deq.pop_front();
		if (attended.contains(frontElem))
			continue;

		pair<int, int> pos = frontElem.first;
		pair<int, int> dir = frontElem.second;

		if (pos.first < 0 || pos.second < 0 || pos.first >= vec.front().size() || pos.second >= vec.size())
			continue;

		attended.insert(frontElem);
		attendedPos.insert(pos);
		vector<pair<int, int>> direcs = dirs[vec[pos.second][pos.first]][dir];

		for (pair<int, int>& outDir : direcs)
		{
			deq.push_back({ {pos.first + outDir.first,pos.second + outDir.second},outDir });
		}
	}
	return attendedPos.size();
}

std::string MySolution1(std::vector<std::string>& vec)
{
	return std::to_string(NavigateMaze(vec, { {0,0},{1,0} }));
}

std::string MySolution2(std::vector<std::string>& vec)
{
	long long maxV = 0;

	int nOfCols = vec.front().size();
	int nOfRows = vec.size();
	int i;
	for ( i = 0; i < nOfRows; i++)
	{
		maxV=std::max(maxV, NavigateMaze(vec, { {0,i},{1,0} }));
		maxV=std::max(maxV, NavigateMaze(vec, { {nOfCols-1 ,i},{-1,0} }));
	}
	for ( i = 0; i < nOfCols; i++)
	{
		maxV=std::max(maxV, NavigateMaze(vec, { {i,0},{0,1} }));
		maxV=std::max(maxV, NavigateMaze(vec, { {i ,nOfRows-1},{0,-1} }));
	}


	return std::to_string(maxV);
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
	else if (microseconds > 100000)
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