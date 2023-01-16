#pragma once
#include "FastLayout.h"
#include "CaveLayout.h"
#include "Node.h"
#include <queue>
#include <algorithm>


FastLayout::FastLayout() {
}

FastLayout::FastLayout(CaveLayout& caveLayout, int timeOfEnd)
{
	TestNode = std::shared_ptr<FastNode>(new FastNode("AA", 0, -1));
	//int MEMO[memoDim0][memoDim1][memoDim2];
	int index = 0;
	for (pair<string, std::shared_ptr<Node>>  const& pair : caveLayout.Nodes)
	{
		Node node = *(pair.second);
		if (node.Flow == 0)
			continue;

		std::shared_ptr<FastNode> fastNode= std::shared_ptr<FastNode>(new FastNode(node.Name, node.Flow, index));
		FastNodes[fastNode->Name] = fastNode;
		AllOpen |= (1 << index);
		index++;
	}

	for (pair<string, std::shared_ptr<Node>>  const& pair : caveLayout.Nodes)
	{
		std::shared_ptr<Node> node = (pair.second);
		if (node->Flow == 0)
			continue;

		caveLayout.ResetNodes();
		RunBfs(FastNodes[node->Name], node);
	}

	caveLayout.ResetNodes();
	RunBfs(TestNode, caveLayout.Nodes["AA"]);

	int i, j, k;
	for (i = 0; i < FastLayout::memoDim0; i++)
		for (j = 0; j < FastLayout::memoDim1; j++)
			for (k = 0; k < FastLayout::memoDim2; k++)
				MEMO[i][j][k] = -1;

}

void FastLayout::RunBfs(std::shared_ptr<FastNode> fastNode, std::shared_ptr<Node> node)
{

	queue<std::shared_ptr<Node>> queOfNodes;
	node->Distance = 0;
	node->Attended = true;
	queOfNodes.push(node);
	while (!queOfNodes.empty() )
	{
		std::shared_ptr<Node> tmp= queOfNodes.front();
		queOfNodes.pop();

		if (node != tmp)
		{
			if (tmp->Flow > 0)
				fastNode->AddAdjenced(FastNodes[tmp->Name], tmp->Distance);
		}
		for(std::shared_ptr<Node> const& adj : tmp->Adjenced)
		{
			if (adj->Attended)
				continue;
			adj->Distance = tmp->Distance + 1;
			adj->Attended = true;
			queOfNodes.push(adj);
		}
	}
}

int FastLayout::Simulate(int timeOfEnd)
{
	TimeOfEnd = timeOfEnd;
	int maxValue = 0;
	int lo = 0;

	for(pair<string, int> const&fn : TestNode->Adjenced)
	{
		lo = 0;
		if ((lo & (1 << FastNodes[fn.first]->Index)) == 0)
			maxValue = max(
				maxValue
				,
				GetMaxFlow(fn.first, fn.second + 1, lo | (1 << FastNodes[fn.first]->Index)) + FastNodes[fn.first]->GetValueToEnd(fn.second + 1, TimeOfEnd));
	}

	return maxValue;
}

int FastLayout::Simulate2(int timeOfEnd)
{
	TimeOfEnd = timeOfEnd;
	int maxValueOfFirst;
	int maxValueOfSecond;
	int maxValue = 0;
	int lo = 0;

	int first;
	int second;
	int nOfOnes;
	int fnCount = FastNodes.bucket_count();
	for (first = 0; first <= AllOpen / 2; first++)
	{
		nOfOnes = GetNofOnes(first);
		if (nOfOnes < 3 || nOfOnes >(fnCount - 3))
			continue;

		maxValueOfFirst = 0;
		maxValueOfSecond = 0;
		second = first ^ AllOpen;

		for(pair<string, int> const& fn : TestNode->Adjenced)
		{
			lo = first;
			if ((lo & (1 << FastNodes[fn.first]->Index)) == 0)
				maxValueOfFirst = max(
					maxValueOfFirst
					,
					GetMaxFlow(fn.first, fn.second + 1, lo | (1 << FastNodes[fn.first]->Index)) + FastNodes[fn.first]->GetValueToEnd(fn.second + 1, TimeOfEnd));
		}

		for (pair<string, int> const& fn : TestNode->Adjenced)
		{
			lo = second;
			if ((lo & (1 << FastNodes[fn.first]->Index)) == 0)
				maxValueOfSecond = max(
					maxValueOfSecond
					,
					GetMaxFlow(fn.first, fn.second + 1, lo | (1 << FastNodes[fn.first]->Index)) + FastNodes[fn.first]->GetValueToEnd(fn.second + 1, TimeOfEnd));
		}
		maxValue = max(
			maxValue
			,
			maxValueOfFirst + maxValueOfSecond
		);
	}


	return maxValue;
}
int FastLayout::GetNofOnes(int first)
{

	int nofOnes = 0;
	for (int i = 0; i < 30; i++)
		if ((first & (1 << i)) > 0)
			nofOnes++;
	return nofOnes;
}

int FastLayout::GetMaxFlow(string place, int time, int opened)
{
	if (time > TimeOfEnd)
		return -100000;

	if (time == TimeOfEnd)
		return 0;


	if (opened == AllOpen)
	{
		return 0;
	}

	std::shared_ptr < FastNode> node = FastNodes[place];
	if (MEMO[node->Index][ time][opened] != -1)
		return MEMO[node->Index][ time][ opened];
	//if (Memo.ContainsKey(place + " " + time + " " + opened))
	//    return Memo[place + " " + time + " " + opened];

	int maxValue = 0;

	for(pair<string, int> const& fn : node->Adjenced)
	{
		if ((opened & (1 << FastNodes[fn.first]->Index)) > 0)
			continue;
		maxValue = max(
			maxValue
			,
			GetMaxFlow(fn.first, time + fn.second + 1, opened | (1 << FastNodes[fn.first]->Index)) + FastNodes[fn.first]->GetValueToEnd(time + fn.second + 1, TimeOfEnd)
		);
	}

	MEMO[node->Index][ time][ opened] = maxValue;
	return maxValue;
}