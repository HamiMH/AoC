#pragma once
using namespace std;
#include"CaveLayout.h"
#include"Node.h"

CaveLayout::CaveLayout(vector<string>& inputCol) {


	vector<pair<string, string>> pairs;

	for (string str : inputCol) {
		
		std::shared_ptr<Node> nPtr = std::shared_ptr<Node>(new Node(str, pairs));
        Nodes[(nPtr->Name)] = nPtr;
	}

	for (pair<string, string>  const& par : pairs)
	{
		Nodes[par.first]->AddAdjenced(Nodes[par.second]);
	}
}

void CaveLayout::ResetNodes() {
	for(pair<string, std::shared_ptr<Node>>  const& pair : Nodes)
	{
		pair.second->Distance = -1;
		pair.second->Attended = false;
	}
}