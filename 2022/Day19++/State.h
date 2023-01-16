#pragma once

class State
{

public:
	static const int NOfStates = 8;
	int Robots[4]; //= new int[BluePrint.NOfGeodes];
	int Material[4];// = new int[BluePrint.NOfGeodes];



	State(const State& oldState);

	State(int ores[], int robots[]);

	int MaterialHash();
	long long MaterialHash2();
	
	State Build(int type, int nofMoves, int cost[4][4]);
};