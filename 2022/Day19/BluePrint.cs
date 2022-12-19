﻿using Day19;
using System;
using System.Collections.Generic;
using System.Linq;

internal class BluePrint
{


    private const int ORE = 0;
    private const int CLAY = 1;
    private const int OBSI = 2;
    private const int GEOD = 3;
    private const int NOfGeodes = 4;

    private int[] Robots = new int[NOfGeodes];
    private int[] Material = new int[NOfGeodes];
    private int[,] Cost = new int[NOfGeodes, NOfGeodes];

    List<SortedSet<StateOfSim>> sortedSets = new List<SortedSet<StateOfSim>>();


    public BluePrint()
    {
        for (int i = 0; i < 30; i++)
            sortedSets.Add(new SortedSet<StateOfSim>());


        
        //AddSoS(0, new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }));
        //AddSoS(0, new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 1 }));
        //AddSoS(0, new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 1, 0 }));
        //AddSoS(0, new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 2, 2 }));
        //AddSoS(0, new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 2, 0 }));
        //AddSoS(0, new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 2, 1 }));


        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }));
        //bool a = sortedSets[0].Contains(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0 }));
        //if (a)
        //    sortedSets[0].Remove(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0 }));
        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0 }));

        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 1, 1 }));
        //bool b = sortedSets[0].Contains(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0 }));
        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 1 }));
        //bool c = sortedSets[0].Contains(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 1, 0 }));

        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 1, 0 }));

        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 1, 1 }));
        //sortedSets[0].Add(new StateOfSim(new int[] { 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0 }));

    }

    public bool AddSoS(int index, StateOfSim sos)
    {
        bool isMandatoryState = false;
        while (sortedSets[index].Contains(sos))
        {
            isMandatoryState = true;
            sortedSets[index].Remove(sos);
        }

        if (isMandatoryState == false)
        {
            isMandatoryState = true;
            foreach (StateOfSim inSS in sortedSets[index])
            {
                if (sos.IsLessThen(inSS))
                {
                    isMandatoryState = false;
                    break;
                }

            }
        }
        if (isMandatoryState)
            sortedSets[index].Add(sos);
        return isMandatoryState;
    }

    private void Clear()
    {
        int i, j;
        for (i = 0; i < NOfGeodes; i++)
        {
            Robots[i] = 0;
            Material[i] = 0;
            for (j = 0; j < NOfGeodes; j++)
                Cost[i, j] = 0;
        }
        Robots[0] = 1;
    }

    internal int GetBestOres(int remSteps)
    {
        if (remSteps == 0)
            return Material[GEOD];

        if (AddSoS(remSteps, new StateOfSim(Material,Robots)) == false)
            return -10000;
        int canBuyOreMine = CanBuy(ORE);
        int canBuyClayMine;
        int canBuyObsiMine;
        int canBuyGeodMine;

        int maxGain = 0;
        int tmp;

        Tuple<int, int, int, int> tupple;
        tupple = GetMaterialGain();
        int i, j, k, l;
        for (i = 0; i <= canBuyOreMine; i++)
        {
            Buy(ORE, i);
            canBuyClayMine = CanBuy(CLAY);
            for (j = 0; j <= canBuyClayMine; j++)
            {
                Buy(CLAY, j);
                canBuyObsiMine = CanBuy(OBSI);
                for (k = 0; k <= canBuyObsiMine; k++)
                {
                    Buy(OBSI, k);
                    canBuyGeodMine = CanBuy(GEOD);
                    for (l = 0; l <= canBuyGeodMine; l++)
                    {
                        Buy(GEOD, l);

                        AddMaterials(tupple);
                        tmp = GetBestOres(remSteps - 1);
                        if (maxGain < tmp)
                            maxGain = tmp;

                        RemoveMaterials(tupple);

                        RevertBuy(GEOD, l);
                    }
                    RevertBuy(OBSI, k);
                }
                RevertBuy(CLAY, j);
            }
            RevertBuy(ORE, i);
        }


        return maxGain;
    }

    private void RemoveMaterials(Tuple<int, int, int, int> tupple)
    {
        Material[0] -= tupple.Item1;
        Material[1] -= tupple.Item2;
        Material[2] -= tupple.Item3;
        Material[3] -= tupple.Item4;
    }

    private void AddMaterials(Tuple<int, int, int, int> tupple)
    {
        Material[0] += tupple.Item1;
        Material[1] += tupple.Item2;
        Material[2] += tupple.Item3;
        Material[3] += tupple.Item4;
    }

    private int CanBuy(int type)
    {
        int max = int.MaxValue;
        int tmp;
        for (int i = 0; i < NOfGeodes; i++)
        {
            if (Cost[type, i] == 0) continue;
            tmp = Material[i] / Cost[type, i];
            if (tmp < max)
                max = tmp;
        }

        return max;
    }
    private void Buy(int type, int amoutToBuy)
    {

        for (int i = 0; i < NOfGeodes; i++)
            Material[i] -= (Cost[type, i] * amoutToBuy);

        Robots[type] += amoutToBuy;
    }
    private void RevertBuy(int type, int amoutToRevert)
    {

        for (int i = 0; i < NOfGeodes; i++)
            Material[i] += (Cost[type, i] * amoutToRevert);

        Robots[type] -= amoutToRevert;
    }

    private void AddMaterials()
    {
        for (int i = 0; i < NOfGeodes; i++)
        {
            Material[i] += Robots[i];
        }
    }

    private Tuple<int, int, int, int> GetMaterialGain()
    {
        return new Tuple<int, int, int, int>(Robots[0], Robots[1], Robots[2], Robots[3]);
    }

    internal void SetBP(string inputStr)
    {
        Clear();
        string[] parts = inputStr.Split(new char[] { ' ' });
        Cost[0, 0] = int.Parse(parts[6]);
        Cost[1, 0] = int.Parse(parts[12]);
        Cost[2, 0] = int.Parse(parts[18]);
        Cost[2, 1] = int.Parse(parts[21]);
        Cost[3, 0] = int.Parse(parts[27]);
        Cost[3, 2] = int.Parse(parts[30]);
    }
}