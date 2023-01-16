#pragma once
using namespace std;
#include <string>
#include <unordered_set>
#include "Node.h"
#include "FastNode.h"
#include "CaveLayout.h"
class FastLayout
{

public:
    static int const memoDim0 = 15;
    static int const memoDim1 = 31;
    static int const memoDim2 = (1 << 15) - 1;
    int TimeOfEnd = 30;
    unordered_map<string, std::shared_ptr<FastNode>> FastNodes;


    //public Dictionary<string, int> Memo = new Dictionary<string, int>();
    int MEMO[memoDim0][memoDim1][memoDim2];

    int AllOpen = 0;

    std::shared_ptr<FastNode> TestNode;

    FastLayout();
    FastLayout(CaveLayout& caveLayout, int timeOfEnd);



    int Simulate(int timeOfEnd);
    int Simulate2(int timeOfEnd);
    int GetMaxFlow(string place, int time, int opened);


private:
    void RunBfs(std::shared_ptr<FastNode>, std::shared_ptr<Node> node);
    int GetNofOnes(int first);

};

