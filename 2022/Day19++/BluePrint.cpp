#pragma once

#include "State.h"
#include "BluePrint.h"

#include <algorithm>
#include <stdexcept>

#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <iterator>
using namespace std;

 int BluePrint::Cost[BluePrint::NOfGeodes][BluePrint::NOfGeodes];
 int BluePrint::CostOreHiearch[BluePrint::NOfGeodes];

 BluePrint::BluePrint() {
	 unordered_map<long long, int> MEMO2;
	 int MaxUsefulRobots[BluePrint::NOfGeodes];
 }

void BluePrint::Clear(int time)
{
	int i, j, k, l, m;
	for (i = 0; i < BluePrint::NOfGeodes; i++)
		for (j = 0; j < BluePrint::NOfGeodes; j++)
			Cost[i][j] = 0;

	MEMO2.clear();

	//for (i = 0; i < time + 1; i++) {
	//	for (j = 0; j < MaxUsefulRobots[0] + 1; j++) {
	//		for (k = 0; k < MaxUsefulRobots[1] + 1; k++) {
	//			for (l = 0; l < MaxUsefulRobots[2] + 1; l++) {
	//				/*for (m = 0; m < MaxUsefulRobots[3] + 1; m++) {
	//					delete MEMO[i][j][k][l][m];
	//				}*/
	//				delete[] MEMO[i][j][k][l];
	//			}
	//			delete MEMO[i][j][k];
	//		}
	//		delete MEMO[i][j];
	//	}
	//	delete MEMO[i];
	//}
	//delete MEMO;
}
int BluePrint::RunDFS(int nOfSteps, State& state)
{
	if (nOfSteps < 0)
		return -10000;
	if (nOfSteps == 0)
		return state.Material[3];
	NeededMaterial(nOfSteps, state);

	//if (MEMO[nOfSteps][state.Robots[0]][state.Robots[1]][state.Robots[2]][state.Robots[3]].find(state.MaterialHash()) != MEMO[nOfSteps][state.Robots[0]][state.Robots[1]][state.Robots[2]][state.Robots[3]].end())
	//	return MEMO[nOfSteps][state.Robots[0]][state.Robots[1]][state.Robots[2]][state.Robots[3]][state.MaterialHash()];
	if (MEMO2.find(state.MaterialHash2()) != MEMO2.end())
		return MEMO2[state.MaterialHash2()];

	int nOfMoves;
	int maxValue = state.Material[3] + nOfSteps * state.Robots[3];
	for (int type = 0; type < 4; type++)
	{
		if (state.Robots[type] >= MaxUsefulRobots[type])
			continue;
		if (CanBuild(type, state.Robots) == false)
			continue;
		nOfMoves = MovesToCanBuild(type, state.Robots, state.Material);

		State newState = state.Build(type, nOfMoves + 1,BluePrint::Cost);
		maxValue = max(maxValue, BluePrint::RunDFS(nOfSteps - (nOfMoves + 1), newState));
	}

	//MEMO.Add("T: " + nOfSteps + ", " + state.ToString(), maxValue);
	//MEMO[nOfSteps][state.Robots[0]][state.Robots[1]][state.Robots[2]][state.Robots[3]][state.MaterialHash()] = maxValue;
	MEMO2[state.MaterialHash2()] = maxValue;
	return maxValue;
}

bool BluePrint::CanBuild(int type, int robots[])
{
	switch (type)
	{
	case 0:
		return true;
	case 1:
		return true;
	case 2:
		if (robots[1] > 0)
			return true;
		else
			return false;
	case 3:
		if (robots[2] > 0)
			return true;
		else
			return false;
	default:
		throw std::invalid_argument("BluePrint::CanBuild");

	}
}
int BluePrint::MovesToCanBuild(int type, int robots[], int material[]) {
	
	int val0;
	int val1;
	switch (type)
	{
	case 0:
		if (Cost[0][ 0] - material[0] <= 0)
			return 0;
		return IntDiv((Cost[0][0] - material[0]), robots[0]);
	case 1:
		if (Cost[1][ 0] - material[0] <= 0)
			return 0;
		return IntDiv((Cost[1][0] - material[0]), robots[0]);
	case 2:
		val0 = max(0, Cost[2][0] - material[0]);
		val1 = max(0, Cost[2][1] - material[1]);
		return max(IntDiv(val0, robots[0]), IntDiv(val1, robots[1]));
	case 3:
		val0 = max(0, Cost[3][0] - material[0]);
		val1 = max(0, Cost[3][2] - material[2]);
		return max(IntDiv(val0, robots[0]), IntDiv(val1, robots[2]));
	default:
		throw std::invalid_argument("BluePrint::MovesToCanBuild");
	}
}
int BluePrint::IntDiv(int a, int b)
{
	int res = a / b;
	if (a % b > 0)
		res++;
	return res;
}

void BluePrint::NeededMaterial(int timeToEnd, State& state)
{
	int timeToEndTmp = timeToEnd;
	int mater0 = 0;
	int timeToRem;
	for (int i = 0; i < 4; i++)
	{
		timeToRem = min(MaxUsefulRobots[CostOreHiearch[i]] - state.Robots[CostOreHiearch[i]], timeToEndTmp);
		mater0 += timeToRem * Cost[CostOreHiearch[i]][0];
		timeToEndTmp -= timeToRem;
	}
	state.Material[0] = min(state.Material[0], mater0);

	state.Material[1] = min(state.Material[1],
		min(MaxUsefulRobots[2] - state.Robots[2], timeToEnd) * Cost[2][1]);

	state.Material[2] = min(state.Material[2],
		min(MaxUsefulRobots[3] - state.Robots[3], timeToEnd) * Cost[3][2]);


}


int BluePrint::GetBestOresDFS(int nOfSteps)
{
	int arr0[] = { 0, 0, 0, 0 };
	int arr1[] = { 1, 0, 0, 0 };
	State state(arr0, arr1);

	int result = BluePrint::RunDFS(nOfSteps, state);
	Clear(nOfSteps);
	return result;

}
void BluePrint::SetBP(std::string& inputStr, int time)
{
	//Clear();
	std::istringstream iss(inputStr);
	vector<std::string> parts((istream_iterator<string>(iss)),
		std::istream_iterator<std::string>());


	Cost[0][0] = stoi(parts[6]);
	Cost[1][0] = stoi(parts[12]);
	Cost[2][0] = stoi(parts[18]);
	Cost[2][1] = stoi(parts[21]);
	Cost[3][0] = stoi(parts[27]);
	Cost[3][2] = stoi(parts[30]);
	MaxUsefulRobots[0] = max(max(max(Cost[0][0] - 1, Cost[1][0]), Cost[2][0]), Cost[3][0]);
	MaxUsefulRobots[1] = Cost[2][1];
	MaxUsefulRobots[2] = Cost[3][2];
	MaxUsefulRobots[3] = time;



	int i, j, k, l, m;
	//MEMO = new unordered_map<int, int>****[time + 1];
	//unordered_map<int, int> uom; 
	//for (i = 0; i < time + 1; i++) {
	//	MEMO[i] = new unordered_map<int, int>***[MaxUsefulRobots[0] + 1];
	//	for (j = 0; j < MaxUsefulRobots[0] + 1; j++) {
	//		MEMO[i][j] = new unordered_map<int, int>**[MaxUsefulRobots[1] + 1];
	//		for (k = 0; k < MaxUsefulRobots[1] + 1; k++) {
	//			MEMO[i][j][k] = new unordered_map<int, int>*[MaxUsefulRobots[2] + 1];
	//			for (l = 0; l < MaxUsefulRobots[2] + 1; l++) {
	//				MEMO[i][j][k][l] = new unordered_map<int, int>[MaxUsefulRobots[3] + 1];
	//				/*for (m = 0; m < MaxUsefulRobots[3] + 1; m++) {
	//					MEMO[i][j][k][l][m] = uom;
	//				}*/
	//			}
	//		}
	//	}
	//}
	CostOreHiearch[0] = -1;
	CostOreHiearch[1] = -1;
	CostOreHiearch[2] = -1;
	CostOreHiearch[3] = -1;


	int maxIndex;
	int max;
	for (i = 0; i < 4; i++)
	{
		maxIndex = 0;
		max = 0;
		for (j = 0; j < 4; j++)
		{
			for (k = 0; k < i; k++)
				if (CostOreHiearch[k] == j)
					continue;
			if (Cost[j][0] >= max)
			{
				max = Cost[j][0];
				maxIndex = j;
			}
		}
		CostOreHiearch[i] = maxIndex;
	}
}

