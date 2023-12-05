#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include <functional>

using namespace std;

int nOfRow;
int nOfCol;
std::vector<std::string> vec;
vector<pair<int, int>> vecStars{};
vector<pair<pair<int, int>, pair<int, int>>> vecNumbers{};
vector<pair<int, int>> directions =
{
	{1,0},
	{1,1},
	{0,1},
	{-1,1},
	{-1,0},
	{-1,-1},
	{0,-1},
	{1,-1},
};

bool AnySymbol(char c)
{
	if (!((c >= '0' && c <= '9') || c == '.'))
		return true;
	return false;
}
bool StarSymbol(char c)
{
	if (c == '*')
		return true;
	return false;
}

bool SymbolIsAdj(int i, int j, std::function<bool(char)> f)
{
	int ii, jj;
	char c;
	for (pair<int, int>& pair : directions)
	{
		ii = pair.first + i;
		jj = pair.second + j;
		if (ii < 0 || jj < 0 || ii >= nOfCol || jj >= nOfCol)
			continue;
		if (f(vec[ii][jj]))
			return true;
	}
	return false;
}
long long long longVal(pair<pair<int, int>, pair<int, int>>& begEnd)
{
	long long sum = 0;
	int i = begEnd.first.first;
	for (int j = begEnd.first.second; j <= begEnd.second.second; j++)
	{
		sum *= 10;
		sum += (vec[i][j] - '0');
	}
	return sum;
}





long long EngAdjancedToStar(std::pair<int, int>& star)
{
	int ii, jj;
	long long product = 1;
	int appears = 0;
	for (std::pair<std::pair<int, int>, std::pair<int, int>>& begEnd : vecNumbers)
	{
		for (std::pair<int, int>& dirs : directions)
		{
			ii = dirs.first + star.first;
			jj = dirs.second + star.second;

			if (begEnd.first.first == ii && (begEnd.first.second <= jj && begEnd.second.second >= jj))
			{
				product *= long longVal(begEnd);
				appears++;
				break;
			}
		}
	}
	if (appears == 2)
		return product;

	return 0;
}


void TryAddNumber(bool& adjSym, int& i, int& firstDig, int& lastDig)
{
	if (adjSym)
	{
		vecNumbers.push_back({ {i,firstDig},{i,lastDig} });
	}
	adjSym = false;
	firstDig = -1;
}

void FindNumbers(std::function<bool(char)> f)
{
	int i, j;
	int firstDig = -1;
	int lastDig = -1;
	bool adjSym = false;
	char c;
	for (i = 0; i < nOfRow; i++)
	{
		for (j = 0; j < nOfCol; j++)
		{
			c = vec[i][j];

			if (c == '*')
				vecStars.push_back({ i,j });

			if (c >= '0' && c <= '9')
			{
				if (firstDig == -1)
					firstDig = j;
				lastDig = j;
				adjSym |= SymbolIsAdj(i, j,f);
			}
			else
			{
				TryAddNumber(adjSym, i, firstDig, lastDig);
			}

		}
		TryAddNumber(adjSym, i, firstDig, lastDig);
	}
}

std::string MySolution1()
{
	nOfRow = vec.size();
	nOfCol = vec[0].size();


	FindNumbers(AnySymbol);


	long long totSum = 0;

	for (pair<pair<int, int>, pair<int, int>> begEnd : vecNumbers)
	{
		totSum += long longVal(begEnd);
	}

	return std::to_string(totSum);

}

std::string MySolution2()
{

	nOfRow = vec.size();
	nOfCol = vec[0].size();
	FindNumbers(StarSymbol);

	long long totSum = 0;
	for (pair<int, int>& pair : vecStars)
	{
		totSum += EngAdjancedToStar(pair);
	}

	return std::to_string(totSum);

}
int main()
{
	std::string in;
	std::cin >> in;
	std::ifstream file(in + ".txt");
	std::string str;
	while (std::getline(file, str))
	{
		vec.push_back(str);
	}

	auto start = std::chrono::high_resolution_clock::now();
	std::string result = MySolution2();
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
