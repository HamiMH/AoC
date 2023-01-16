#pragma once
#include <unordered_map>
#include <string>
#include "State.h"

using namespace std;
class BluePrint
{
public:
	static const int NOfGeodes = 4;
private:
	const int ORE = 0;
	const int CLAY = 1;
	const int OBSI = 2;
	const int GEOD = 3;

	int MaxUsefulRobots[NOfGeodes];
public:
	static int Cost[NOfGeodes][NOfGeodes];
	static int CostOreHiearch[NOfGeodes];
	//unordered_map<int, int>***** MEMO;
	unordered_map<long long, int> MEMO2;
	BluePrint();

private:
	void Clear(int time);
	int RunDFS(int nOfSteps, State& state);
	bool CanBuild(int type, int robots[]);
	int MovesToCanBuild(int type, int robots[], int material[]);
	int IntDiv(int a, int b);
	void NeededMaterial(int timeToEnd, State& state);

public:
	int GetBestOresDFS(int nOfSteps);
	void SetBP(string& inputStr, int time);

};