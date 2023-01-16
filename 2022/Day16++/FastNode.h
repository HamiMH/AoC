#pragma once
using namespace std;
#include <string>
#include <unordered_map>
#include <memory>

//class MyHashFunction {
//public:
//
//	size_t operator()(const FastNode& fn) const
//	{
//		return (hash<string>()(fn.Name));
//	}
//};

class FastNode
{
public:
	string Name;
	int Flow;
	int Index;
	unordered_map<string, int> Adjenced;

	FastNode();
	FastNode(string name, int flow, int index);
	void AddAdjenced(std::shared_ptr<FastNode> node, int wei);

	/*size_t operator()();
	size_t operator()(const FastNode& fn) const;
	FastNode& operator=(const FastNode& lhs);
	bool operator==(const FastNode& lhs);
	bool operator!=(const FastNode& lhs);*/

	//bool Equals(object ? obj);

	//bool IsOpened(unsigned long openedBitmap);

	int GetValueToEnd(int timeWhenIsOpened, int timeOfEnd);
};
