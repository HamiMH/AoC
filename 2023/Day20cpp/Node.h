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
const int BROADCAST = 0;
const int FLIPFLOP = 1;
const int CONJUCTION = 2;

const int LOWPULSE=0;
const int HIGHPULSE=1;

class Node
{
public:
	std::vector<std::string> InComingNodes;
	std::vector<std::string> OutGoingNodes;

	std::vector<int> HighPulseTimes;

	string Symbol;
	string Name;
	virtual void Init() {};
	virtual void ProcessSignal(tuple<string, string, int> pulseInfo, deque<tuple<string, string, int>>& que) {};

	Node(string inName) { Name = inName; }
};


class FlipFlop : public Node
{
public:
	FlipFlop(string str) :Node(str) {
		Symbol = "F_";
	}

	bool Switch = false;
	virtual void Init() override
	{
		Switch = false;
	}
	virtual void ProcessSignal(tuple<string, string, int> pulseInfo, deque<tuple<string, string, int>>& que) override
	{
		const auto& [from, into, pulse] = pulseInfo;
		if (pulse == HIGHPULSE)return;

		Switch ^= true;
		int pulseOut;
		if (Switch)
			pulseOut = HIGHPULSE;
		else
			pulseOut = LOWPULSE;
		for (string& outNode : OutGoingNodes)
			que.push_back({ Name,outNode,pulseOut });
	};
};
class Conjunction : public Node
{
private:
	unordered_map<string, int> _recentPulses;
public:
	Conjunction(string str) :Node(str) {
		Symbol = "C_";
	}

	virtual void Init() override
	{
		for (string& str : InComingNodes)
			_recentPulses[str] = LOWPULSE;

	}
	virtual void ProcessSignal(tuple<string, string, int> pulseInfo, deque<tuple<string, string, int>>& que) override
	{
		const auto& [from, into, pulse] = pulseInfo;

		_recentPulses[from] = pulse;

		int pulseOut = LOWPULSE;
		for (pair<const string, int>& psi : _recentPulses)
			if (psi.second == LOWPULSE)
			{
				pulseOut = HIGHPULSE;
				break;
			}
		if (pulseOut == HIGHPULSE)
			HighPulseTimes.push_back(SimulTime);

		for (string& outNode : OutGoingNodes)
			que.push_back({ Name,outNode,pulseOut });
	};

};
class Broadcaster : public Node
{
public:
	Broadcaster(string str) :Node(str) {
		Symbol = "B_";
	}

	virtual void ProcessSignal(tuple<string, string, int> pulseInfo, deque<tuple<string, string, int>>& que) override
	{
		const auto& [from, into, pulse] = pulseInfo;

		for (string& outNode : OutGoingNodes)
			que.push_back({ Name,outNode,pulse });
	};
};
class Special : public Node
{
public:
	Special(string str) :Node(str) {
		{
			Symbol = "S_";
		}
	}

	virtual void ProcessSignal(tuple<string, string, int> pulseInfo, deque<tuple<string, string, int>>& que) override
	{
		const auto& [from, into, pulse] = pulseInfo;
		if(pulse==LOWPULSE)
			throw std::invalid_argument(", Does not contains important chars.");
		//else
		//	throw std::invalid_argument(", Does not contains important chars.");
	};
};


class Graph
{
public:
	std::unordered_map<std::string, shared_ptr<Node>>Nodes;


	void AddEdge(std::string from, std::string into)
	{

		if (!Nodes.contains(from))
			Nodes[from] = make_shared<Special>(from);
		if (!Nodes.contains(into))
			Nodes[into] = make_shared<Special>(into);

		//std::cout << Nodes[from]->Symbol << from << "-> " << Nodes[into]->Symbol << into << ";" << endl;
		//std::cout << Nodes[from]->Symbol << from << ">" << Nodes[into]->Symbol << into << "" << endl;

		Nodes[from]->OutGoingNodes.push_back(into);
		Nodes[into]->InComingNodes.push_back(from);
	}


	void ProcessNode(std::string& str, std::vector<pair<string, string>>& edges)
	{
		if (str.contains("broadcaster"))
		{
			std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, "->");
			std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), ",");
			for (std::string& target : strTargets)
			{
				edges.push_back({ strSpl.front(), target });
				Nodes[strSpl.front()] = make_shared<Broadcaster>(strSpl.front());
			}

		}
		else if (str.contains("%"))
		{
			std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, "->");
			std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), ",");
			for (std::string& target : strTargets)
			{
				edges.push_back({ strSpl.front().substr(1), target });
				Nodes[strSpl.front().substr(1)] = make_shared<FlipFlop>(strSpl.front().substr(1));
			}
		}
		else if (str.contains("&"))
		{
			std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, "->");
			std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), ",");
			for (std::string& target : strTargets)
			{
				edges.push_back({ strSpl.front().substr(1), target });
				Nodes[strSpl.front().substr(1)] = make_shared<Conjunction>(strSpl.front().substr(1));
			}
		}
		else
		{
			throw std::invalid_argument(str + ", Does not contains important chars.");
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
		for (pair<const std::string, shared_ptr<Node>>& pairs : Nodes)
		{
			pairs.second->Init();
		}
	}
};
