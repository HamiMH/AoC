#pragma once
#include <algorithm>
#include "CardHandBase.h"

class CardHand2 :public CardHandBase
{
public:
    CardHand2(std::string str, long long bet);
    //: CardHandBase(str, bet) {}
protected:
    virtual long long ValueOf(std::vector<long long>& hand) override;
    /*
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
*/
    virtual long long RankOfCard(char c) override;
    /*
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
    }
    */
};
