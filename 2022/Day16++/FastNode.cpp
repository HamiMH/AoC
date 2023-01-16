#pragma once
#include "FastNode.h"
#include "Node.h"
#include "CaveLayout.h"
//
//size_t MyHashFunction::operator()(const FastNode& fn)
//{
//    return (hash<string>()(fn.Name));
//}
//
//
//size_t FastNode::operator()() 
//{
//    return (hash<string>()(Name));
//}
//size_t FastNode::operator()(const FastNode& fn) const 
//{
//    return (hash<string>()(fn.Name))^ (hash<string>()(this->Name));
//}
//
//
//bool FastNode::operator==(const FastNode& lhs) {
//    if (this->Name == lhs.Name)
//        return true;
//    else
//        return false;
//}
//bool FastNode::operator!=(const FastNode& lhs) {
//    if (this->Name == lhs.Name)
//        return false;
//    else
//        return true;
//}

    FastNode::FastNode()
{
        Name = "";
    Flow = 0;
    Index = 0;
}
    FastNode::FastNode(string name, int flow, int index)
{
    Name = name;
    Flow = flow;
    Index = index;
}

void FastNode::AddAdjenced(std::shared_ptr<FastNode> node, int wei) { Adjenced[node->Name]=(wei); }

int FastNode::GetValueToEnd(int timeWhenIsOpened, int timeOfEnd)
{
    int val = (timeOfEnd - timeWhenIsOpened) * Flow;
    return val;
}