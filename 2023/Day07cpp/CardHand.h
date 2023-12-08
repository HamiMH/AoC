#pragma once
#include <string>
#include <vector>
#include <algorithm>
#include "CardHandBase.h"

class CardHand :public CardHandBase
{
public:
    CardHand(std::string str, long long bet);
    //: CardHandBase(str, bet) {}

protected:
    virtual long long ValueOf(std::vector<long long>& hand) override;
    /*{
        long long arr[15]{ 0 };

        for (long long l : hand)
            arr[l]++;

        std::sort(std::begin(arr), std::end(arr), std::greater<long long>());

        if (arr[0] == 5)
            return 10;
        if (arr[0] == 4)
            return 9;
        if (arr[0] == 3)
            return 6 + arr[1];
        if (arr[0] == 2)
            return 4 + arr[1];
        return 4;
    }*/ 

    virtual long long RankOfCard(char c) override;
    /*
    {
        if (c >= '2' && c <= '9')
            return (c - '0');
        else if (c == 'T')
            return(10);
        else if (c == 'J')
            return (11);
        else if (c == 'Q')
            return (12);
        else if (c == 'K')
            return (13);
        else if (c == 'A')
            return (14);
    }
    */
};

