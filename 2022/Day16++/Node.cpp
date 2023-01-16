#pragma once
using namespace std;

#include"Node.h"
#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <iterator>

int Node::NumberOfNodes = 0;

Node::Node() {}

bool Node::operator==(const Node& lhs) {
	if (this->Name == lhs.Name)
		return true;
	else
		return false;
}
bool Node::operator!=(const Node& lhs) {
	if (this->Name == lhs.Name)
		return false;
	else
		return true;
}

Node::Node(string& wholeStr, vector<pair<string, string>>& pairs) {

	stringstream ss(wholeStr);
	string str1;
	char chr;
	int num;

	ss >> str1 >> str1;
	Name = str1;

	ss >> str1 >> str1 >> chr >> chr >> chr >> chr >> chr >> num;
	Flow = num;

	ss >> chr >> str1 >> str1 >> str1 >> str1;

	char chr1, chr2;
	while (ss >> chr1 >> chr2)
	{
		string nodeName = "";
		nodeName = nodeName + chr1 + chr2;
		pairs.push_back(pair<string, string>(Name, nodeName));
		ss >> chr;
	}

	Index = NumberOfNodes;
	NumberOfNodes++;
}


void Node::AddAdjenced(std::shared_ptr<Node> node) { Adjenced.push_back(node); }