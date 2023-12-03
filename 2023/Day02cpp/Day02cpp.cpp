
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <chrono>
#include <unordered_map>
#include "../Library/StringAdv.cpp"

using namespace std;


std::unordered_map<string, int> maxCubes =
{
    {string("red"),12},
    {string("green"),13},
    {string("blue"),14},
};

bool SetOfGamesIsValid(string& line) 
{
    vector < string > games = CustomSplit(line, ';');
    for (std::string& gam : games)
    {
        string game = trim(gam);

        vector < string > vecOfDiceInfos = CustomSplit(game, ',');
        for (std::string& diceInfo : vecOfDiceInfos)
        {
            string diceInfoes = trim(diceInfo);

            vector < string > parts = CustomSplit(diceInfoes, ' ');

            int amount=std::stoi(parts.front());
            string type=parts.back();
            int maxOfType = maxCubes[type];

            if (!(maxOfType == 12 || maxOfType == 13 || maxOfType == 14)) {
                return false;
            }
            if (amount > maxOfType)
                return false;
        }
    }
    return true;
}

std::string MySolution1(std::vector<std::string>& vec)
{
    int iter = 1;
    int sum = 0;
    for (std::string& str : vec)
    {

        vector<string> line = CustomSplit(str, ':');
        iter=stoi(CustomSplit(line.front(), ' ').back());

        string lineOfGame = trim(line.back());
        if (SetOfGamesIsValid(lineOfGame))
        {
            sum += iter;
        }     
    }

    return std::to_string(sum);

}

long ValueOfSetOfGames(string& line)
{
    std::unordered_map<string, long> maxCubesInGame =
    {
        {string("red"),0},
        {string("green"),0},
        {string("blue"),0},
    };

    vector < string > games = CustomSplit(line, ';');
    for (std::string& gam : games)
    {
        string game = trim(gam);

        vector < string > vecOfDiceInfos = CustomSplit(game, ',');
        for (std::string& diceInfo : vecOfDiceInfos)
        {
            string diceInfoes = trim(diceInfo);

            vector < string > parts = CustomSplit(diceInfoes, ' ');

            long amount = std::stol(parts.front());
            string type = parts.back();


            if (amount > maxCubesInGame[type])
                maxCubesInGame[type] = amount;
        }
    }
    long product = 1;
    for (auto& maxxxx : maxCubesInGame)
    {
        product *= maxxxx.second;
    }
    return product;
}

std::string MySolution2(std::vector<std::string>& vec)
{
    long sum = 0;
    for (std::string& str : vec)
    {

        vector<string> line = CustomSplit(str, ':');
        string lineOfGame = trim(line.back());
        sum += ValueOfSetOfGames(lineOfGame);
    }

    return std::to_string(sum);
}

int main()
{
    std::string in;
    std::cin >> in;
    //std::ifstream file("input.txt");
    std::ifstream file(in + ".txt");
    std::string str;
    std::vector<std::string> vec;
    while (std::getline(file, str))
    {
        vec.push_back(str);
    }

    auto start = std::chrono::high_resolution_clock::now();
    std::string result = MySolution2(vec);
    auto finish = std::chrono::high_resolution_clock::now();

    auto microseconds = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    std::cout << "Result: " << result << "\n";
    if (microseconds > 1000000000)
        std::cout << microseconds / 1000000 << "s\n";
    else if (microseconds > 1000000)
        std::cout << microseconds / 1000 << "ms\n";
    else
        std::cout << microseconds << "us\n";
}
