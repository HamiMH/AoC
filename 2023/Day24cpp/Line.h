#pragma once

#include <quadmath.h>
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <set>
#include <map>
#include <unordered_map>
#include <boost/algorithm/string.hpp>
#include <numeric>
#include <algorithm>
#include <numeric>

using namespace std;

const long double EPS = .0000001L;

long double diffs = 0;
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

class Line
{
public:
	/* tuple<long long, long long, long long>X;
	 tuple<long long, long long, long long>Vec;*/
	tuple<long double, long double, long double>X;
	tuple<long double, long double, long double>Vec;

	Line() {}
	Line(string& str)
	{
		vector<string>sides = StringWithSymbolIntoStrings(str, "@");

		vector<string>beg = StringWithSymbolIntoStrings(sides[0], ",");
		vector<string>end = StringWithSymbolIntoStrings(sides[1], ",");

		//X = { stoll(beg[0]) ,stoll(beg[1]),stoll(beg[2]) };
	   // Vec = { stoll(end[0]) ,stoll(end[1]),stoll(end[2]) };
		X = { stold(beg[0]) ,stold(beg[1]),stold(beg[2]) };
		Vec = { stold(end[0]) ,stold(end[1]),stold(end[2]) };
	}

	long double MyAbs(long double in)
	{
		if (in < 0)
			return -in;
		else
			return in;
	}
	bool IntersectLine2D(Line inLine, long double MinP, long double MaxP)
	{
		const auto [x1, y1, z1] = X;
		const auto [u1, v1, w1] = Vec;
		const auto [x2, y2, z2] = inLine.X;
		const auto [u2, v2, w2] = inLine.Vec;

		long double t1;
		long double t2;
		//lines are paralel
		if (MyAbs(u1 * v2 - v1 * u2) < EPS)
		{
			//if (abs((x1 - x2) / u2 - (y1 - y2) / v2) < EPS)
			if (MyAbs((x1 - x2) * v2 - (y1 - y2) * u2) < EPS)
			{
				t1 = 0.0;
				t2 = (x1 - x2) / u2;
			}
			else
			{
				return false;
			}
		}
		else
		{
			t2 = (y1 - y2) / (v2 - v1 / u1 * u2 ) + (x2 - x1) * v1 / u1 / (v2 - v1 / u1 * u2 );
			t1 = (x2 - x1) / u1 + t2 / u1 * u2 ;
		}

		long double finX1 = x1 + u1 * t1;
		long double finY1 = y1 + v1 * t1;
		long double finX2 = x2 + u2 * t2;
		long double finY2 = y2 + v2 * t2;

		long double diffX = finX2 - finX1;
		long double diffY = finY2 - finY1;

		if (abs(diffX) > EPS)
			cout << "diffX" << diffX << endl;
		if (abs(diffY) > EPS)
			cout << "diffY" << diffY << endl;



		//if (t1 < -EPS || t2 < -EPS)
		if (t1 < 0 || t2 < 0)
			return false;

		//if (finX > (MinP - EPS) && finX < (MaxP + EPS) && finY > (MinP - EPS)  && finX < (MaxP + EPS))
		if (finX2 >= (MinP) && finX2 <= (MaxP) && finY2 >= (MinP) && finX2 <= (MaxP))
			return true;
		else
			return false;

	}


	bool IntersectLine2Dv2(Line inLine, long double MinP, long double MaxP)
	{
		const auto [px1, py1, pz1] = X;
		const auto [u1, v1, w1] = Vec;
		const auto [px2, py2, pz2] = inLine.X;
		const auto [u2, v2, w2] = inLine.Vec;

		long double x1 = px1 - MinP;
		long double x2 = px2 - MinP;
		long double y1 = py1 - MinP;
		long double y2 = py2 - MinP;

		long double t1;
		long double t2;
		//lines are paralel
		if (MyAbs(u1 * v2 - v1 * u2) < EPS)
		{
			//if (abs((x1 - x2) / u2 - (y1 - y2) / v2) < EPS)
			if (MyAbs((x1 - x2) * v2 - (y1 - y2) * u2) < EPS)
			{
				t1 = 0.0;
				t2 = (x1 - x2) / u2;
			}
			else
			{
				return false;
			}
		}
		else
		{
			t2 = ((y1 - y2) + (x2 - x1) * v1 / u1) / (v2 - v1 * u2 / u1);
			t1 = (x2 - x1 + t2 * u2) / u1;
		}

		long double finX1 = x1 + u1 * t1;
		long double finY1 = y1 + v1 * t1;
		long double finX2 = x2 + u2 * t2;
		long double finY2 = y2 + v2 * t2;

		long double diffX = finX2 - finX1;
		long double diffY = finY2 - finY1;
		diffs += abs(diffX);
		diffs += abs(diffY);
		/*if (abs(diffX) > .4)
			cout << "diffX" << diffX << endl;
		if (abs(diffY) > .4)
			cout << "diffY" << diffY << endl;*/



		//if (t1 < -EPS || t2 < -EPS)
		if (t1 < 0 || t2 < 0)
			return false;

		//if (finX > (MinP - EPS) && finX < (MaxP + EPS) && finY > (MinP - EPS)  && finX < (MaxP + EPS))
		if (finX2 >= (0) && finX2 <= (MaxP-MinP) && finY2 >= (0) && finX2 <= (MaxP - MinP))
			return true;
		else
			return false;

	}
};