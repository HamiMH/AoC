#pragma once
#include "CardHandBase.h"
/*
CardHandBase::CardHandBase(std::string str, long long bet)
{
    _bet = bet;
    for (char& c : str)
    {
        _hand.push_back(RankOfCard(c));
    }
    _val = ValueOf(_hand);
}
*/
bool CardHandBase::operator< (CardHandBase const& y) const
{
    if (this->_val < y._val)
        return true;
    if (this->_val > y._val)
        return false;

    for (int i = 0; i < _hand.size(); i++)
    {
        if (this->_hand[i] < y._hand[i])
            return true;
        if (this->_hand[i] > y._hand[i])
            return false;
    }

    return false;
}