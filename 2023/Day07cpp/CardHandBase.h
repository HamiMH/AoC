#pragma once
#include <vector>
#include <string>


class CardHandBase
{

protected:
    long long _bet;
    std::vector<long long> _hand;
    long long _val;

public:
    long long Bet()
    {
        return _bet;
    }

    //CardHandBase(std::string str, long long bet);

    bool operator< (CardHandBase const& y) const;
protected:
    virtual long long RankOfCard(char c) = 0;
    virtual long long ValueOf(std::vector<long long>& hand) = 0;


};