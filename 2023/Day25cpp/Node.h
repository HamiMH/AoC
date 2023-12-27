#pragma once
#include <string>
#include <unordered_map>
#include <memory>
#include <boost/algorithm/string.hpp>

using namespace std;

long long SimulTime = 0;

std::vector<std::string>  StringWithSymbolIntoStrings(std::string str, std::string symbols)
{
	std::vector<std::string> outVec;
	boost::trim(str);
	std::vector<std::string>tmpVec;
	boost::split(tmpVec, str, boost::is_any_of(symbols));

	for (std::string& s : tmpVec)
	{
		boost::trim(s);
		if (s == "")
			continue;
		outVec.push_back((s));
	}
	return outVec;
}

class Node
{
public:
	std::map<std::string,int> AdjencedNodes;


	string Name;
	Node() {  }
	Node(string inName) { Name = inName; }
};



class Graph
{
public:
	std::unordered_map<std::string, Node>Nodes;

	void AddEdge(std::string from, std::string into)
	{

		if (!Nodes.contains(from))
			Nodes[from] = Node(from);
		if (!Nodes.contains(into))
			Nodes[into] = Node(into);

		//std::cout <<  from << " " <<  into << "" << endl;

		Nodes[from].AdjencedNodes[ into]= 0 ;
		Nodes[into].AdjencedNodes[ from]=0 ;
	}

	void ProcessNode(std::string& str, std::vector<pair<string, string>>& edges)
	{
		std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, ":");
		std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), " ");
		for (std::string& target : strTargets)
		{
			edges.push_back({ strSpl.front(), target });
			Nodes[strSpl.front()] = Node(strSpl.front());
		}

	}

	Graph(std::vector<std::string>& inVec)
	{
		std::vector<pair<string, string>>edges;

		for (string& inS : inVec)
		{
			ProcessNode(inS, edges);
		}
		for (pair<string, string>& ps : edges)
		{
			AddEdge(ps.first, ps.second);
		}
	}
};
