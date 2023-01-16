#pragma once
using namespace std;
#include <string>
#include <vector>
#include <unordered_map>
#include"Node.h"

#include <iostream>
#include <sstream>
#include <iterator>
class CaveLayout
{
public:
    unordered_map<string, std::shared_ptr<Node>> Nodes;

    CaveLayout(vector<string>& inputCol);

    void ResetNodes();
};

