#pragma once
#include <string>
#include <unordered_map>
#include <memory>
#include <boost/algorithm/string.hpp>

using namespace std;

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

class Node
{
public:
    std::vector<std::string> InComingNodes;
    std::vector<std::string> OutGoingNodes;

    int Type;


    bool ffSwitch = false;
};

/*
class FlipFlop : public Node
{

};
class Conjunction : public Node
{

};
class Broadcaster : public Node
{

};
*/

class Graph
{
public:
    std::unordered_map<std::string, Node>Nodes;


    void AddEdge(int type, std::string from, std::string into)
    {
        if (!Nodes.contains(from))
            Nodes.insert({ from, {} });
        if (!Nodes.contains(into))
            Nodes.insert({ into, {} });
        Nodes[from].Type = type;
        Nodes[from].OutGoingNodes.push_back(into);
        Nodes[into].InComingNodes.push_back(from);
    }


    void AddNode(std::string str)
    {
        if (str.contains("broadcaster"))
        {
            std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, "->");
            std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), ",");
            for (std::string& target : strTargets)
            {
                AddEdge(BROADCAST, strSpl.front(), target);
            }

        }
        else if (str.contains("%"))
        {
            std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, "->");
            std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), ",");
            for (std::string& target : strTargets)
            {
                AddEdge(FLIPFLOP, strSpl.front().substr(1), target);
            }
        }
        else if (str.contains("&"))
        {
            std::vector<std::string> strSpl = StringWithSymbolIntoStrings(str, "->");
            std::vector<std::string> strTargets = StringWithSymbolIntoStrings(strSpl.back(), ",");
            for (std::string& target : strTargets)
            {
                AddEdge(CONJUCTION, strSpl.front().substr(1), target);
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

    }
};

/*

vector<int> ln = graph.Nodes["ln"]->HighPulseTimes;
vector<int> db = graph.Nodes["db"]->HighPulseTimes;
vector<int> rq = graph.Nodes["vq"]->HighPulseTimes;
vector<int> tf = graph.Nodes["tf"]->HighPulseTimes;
vector<int> Dln = Diff(ln);
vector<int> Ddb = Diff(db);
vector<int> Drq = Diff(rq);
vector<int> Dtf = Diff(tf);
vector<int> DDln = Diff(Dln);
vector<int> DDdb = Diff(Ddb);
vector<int> DDrq = Diff(Drq);
vector<int> DDtf = Diff(Dtf);

unsigned long long minDln = (unsigned long long) * std::min_element(Dln.begin(), Dln.end());
unsigned long long minDdb = (unsigned long long) * std::min_element(Ddb.begin(), Ddb.end());
unsigned long long minDrq = (unsigned long long) * std::min_element(Drq.begin(), Drq.end());
unsigned long long minDtf = (unsigned long long) * std::min_element(Dtf.begin(), Dtf.end());

unsigned long long maxDln = (unsigned long long) * std::max_element(Dln.begin(), Dln.end());
unsigned  long maxDdb = (unsigned long long) * std::max_element(Ddb.begin(), Ddb.end());
unsigned long long maxDrq = (unsigned long long) * std::max_element(Drq.begin(), Drq.end());
unsigned long long maxDtf = (unsigned long long) * std::max_element(Dtf.begin(), Dtf.end());

unsigned long long prodMin = lcm(lcm(lcm(minDln, minDdb), minDrq), minDtf);
unsigned long long prodMax = lcm(lcm(lcm(maxDln, maxDdb), maxDrq), maxDtf);

unsigned long long prodSub = prodMax - prodMin;

*/