#pragma once

#include "CardHand2.h"



CardHand2::CardHand2(std::string str, long long bet) //:CardHandBase(str, bet) 
{
    _bet = bet;
    for (char& c : str)
    {
        _hand.push_back(RankOfCard(c));
    }
    _val = ValueOf(_hand);
}

long long CardHand2::ValueOf(std::vector<long long>& hand)
{
    long long arr[15]{ 0 };
    long long nJacks = 0;
    for (long long l : hand)
    {
        if (l == 1)
            nJacks++;
        else
            arr[l]++;
    }

    std::sort(std::begin(arr), std::end(arr), std::greater<long long>());
    arr[0] += nJacks;
    if (arr[0] == 5)
        return 10;
    if (arr[0] == 4)
        return 9;
    if (arr[0] == 3)
        return 6 + arr[1];
    if (arr[0] == 2)
        return 4 + arr[1];
    return 4;
}
long long CardHand2::RankOfCard(char c)
{
    if (c >= '2' && c <= '9')
        return (c - '0');
    else if (c == 'T')
        return (10);
    else if (c == 'J')
        return (1);
    else if (c == 'Q')
        return (12);
    else if (c == 'K')
        return (13);
    else if (c == 'A')
        return (14);
    return 0;
}