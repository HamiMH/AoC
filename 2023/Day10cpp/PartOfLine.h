#pragma once
#include <vector>

class PartOfLine
{
public:

	int X;
	int Y;
	char Ch;

	PartOfLine(int x, int y, char c)
	{
		X = x;
		Y = y;
		Ch = c;
	}


	bool operator< (PartOfLine const& pol) const
	{
		if (this->Y < pol.Y)
			return true;
		if (this->Y > pol.Y)
			return false;

		if (this->X < pol.X)
			return true;
		if (this->X > pol.X)
			return false;

		return false;
	}
};