#pragma once
#include <vector>

class PartOfLine
{
public:

	int X;
	int Y;

	bool ValidBeg;
	bool ValidEnd;

	PartOfLine(int x, int y, char c)
	{
		X = x;
		Y = y;
		switch (c)
		{
		case '|':
			ValidBeg = true;
			ValidEnd = true;
			break;
		case 'F':
			ValidBeg = false;
			ValidEnd = true;
			break;
		case 'J':
			ValidBeg = true;
			ValidEnd = false;
			break;
		case 'L':
			ValidBeg = false;
			ValidEnd = true;
			break;
		case '7':
			ValidBeg = true;
			ValidEnd = false;
			break;

		default:
			break;
		}
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