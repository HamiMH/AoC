#pragma once

#include "State.h"
#include "BluePrint.h"



State::State(int ores[], int robots[])
{
	Material[0] = ores[0];
	Material[1] = ores[1];
	Material[2] = ores[2];
	Material[3] = ores[3];
	Robots[0] = robots[0];
	Robots[1] = robots[1];
	Robots[2] = robots[2];
	Robots[3] = robots[3];
}
State::State(const State& oldState)
{
	Material[0] = oldState.Material[0];
	Material[1] = oldState.Material[1];
	Material[2] = oldState.Material[2];
	Material[3] = oldState.Material[3];
	Robots[0] = oldState.Robots[0];
	Robots[1] = oldState.Robots[1];
	Robots[2] = oldState.Robots[2];
	Robots[3] = oldState.Robots[3];
}


int State::MaterialHash()
{
	return  Material[0] | ((Material[1]) << 8) | ((Material[2]) << 16) | ((Material[3]) << 24);
	//return  Material[0] + 100* Material[1] + 10000 *Material[2] + 1000000* Material[3] ;
}

long long State::MaterialHash2()
{
	/*long l = 0;
	long long  r0 = ((long long)(Robots[0]));
	long long r1 = (long long)(Robots[1]);
	long long r2 = (long long)(Robots[2]);
	long long r3 = (long long)(Robots[3]);
	long long m0 = (long long)(Material[0]);
	long long m1 = (long long)(Material[1]);
	long long m2 = (long long)(Material[2]);
	long long m3 = (long long)(Material[3]);*/
	return  (((long long)(Robots[0])) << 0) | (((long long)(Robots[1])) << 8) | (((long long)(Robots[2])) << 16) | (((long long)(Robots[3])) << 24)
		|
		(((long long)(Material[0])) << 32) | (((long long)(Material[1])) << 40) | (((long long)(Material[2])) <<48) | (((long long)(Material[3])) << 56);
	/*return  (r0 << 0) | (r1 << 8) | (r2 << 16) | (r3 << 24)
		|
		(m0 << 32) | (m1<< 40) | (m2<<48) | (m3 << 56);*/
	//return  Material[0] + 100* Material[1] + 10000 *Material[2] + 1000000* Material[3] ;
}

State State::Build(int type, int nofMoves,int cost[4][4])
{
	//State thisStae = *this;
	State newState(*this);
	for (int i = 0; i < 4; i++)
		newState.Material[i] = newState.Material[i] + newState.Robots[i] * nofMoves - cost[type][i];

	newState.Robots[type]++;

	return newState;

}