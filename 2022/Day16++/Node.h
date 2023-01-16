#pragma once
#include <string>
#include <vector>
#include <memory>
using namespace std;
class Node
{
public:
    static int NumberOfNodes;
    string Name;
     int Flow;
     int Index;
     bool IsOpened = false;
     vector<std::shared_ptr<Node>> Adjenced;

     int Distance = -1;
     bool Attended = false;

     Node();
     Node(string& name, int flow, int index);
     Node(string& wholeStr, vector<pair<string, string>>& pairs);
     void AddAdjenced(std::shared_ptr<Node> node);

     bool operator==(const Node& lhs);
     bool operator!=(const Node& lhs);
};

