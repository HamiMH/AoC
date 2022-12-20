using Day19;
using System;
using System.Collections.Generic;
using System.Linq;

internal class BluePrint
{


    private const int ORE = 0;
    private const int CLAY = 1;
    private const int OBSI = 2;
    private const int GEOD = 3;
    public static readonly int NOfGeodes = 4;

    //private int[] Robots = new int[NOfGeodes];
    //private int[] Material = new int[NOfGeodes];
    private int[,] Cost = new int[NOfGeodes, NOfGeodes];

    //List<SortedSet<StateOfSim>> sortedSets = new List<SortedSet<StateOfSim>>();
    List<StateOfSim> states = new List<StateOfSim>();

    private int[] GeodesInTime = new int[30];

    public BluePrint()
    {
        //for (int i = 0; i < 30; i++)
        //    sortedSets.Add(new SortedSet<StateOfSim>());


        int[] arr = { 0, 0, 0, 0 };
        for (int i = 0; i < 30; i++)
            states.Add(new StateOfSim(arr, arr));



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

    //public bool AddSoS(int index, StateOfSim sos)
    //{
    //    bool isMandatoryState = false;
    //    while (sortedSets[index].Contains(sos))
    //    {
    //        isMandatoryState = true;
    //        sortedSets[index].Remove(sos);
    //    }

    //    if (isMandatoryState == false)
    //    {
    //        isMandatoryState = true;
    //        foreach (StateOfSim inSS in sortedSets[index])
    //        {
    //            if (sos.IsLessThen(inSS))
    //            {
    //                isMandatoryState = false;
    //                break;
    //            }

    //        }
    //    }
    //    if (isMandatoryState)
    //        sortedSets[index].Add(sos);
    //    return isMandatoryState;
    //}

    private void Clear()
    {
        int i, j;
        for (i = 0; i < NOfGeodes; i++)
        {
            //Robots[i] = 0;
            //Material[i] = 0;
            for (j = 0; j < NOfGeodes; j++)
                Cost[i, j] = 0;
        }
        //Robots[0] = 1;

        int[] arr = { 0, 0, 0, 0 };
        for (i = 0; i < 30; i++)
            states[i] = (new StateOfSim(arr, arr));

        Array.Fill(GeodesInTime, 0);
    }

//    internal int GetBestOres(int remSteps, int couldBoughtOre,
//    int couldBoughtClay,
//    int couldBoughtObsi,
//    int couldBoughtGeod
//)
//    {
//        if (remSteps == 0)
//            return Material[GEOD];

//        if (GeodesInTime[remSteps] > Material[GEOD])
//            return -1000;
//        if (GeodesInTime[remSteps] < Material[GEOD])
//            GeodesInTime[remSteps] = Material[GEOD];

//        //if (AddSoS(remSteps, new StateOfSim(Material, Robots)) == false)
//        //    return -10000;

//        StateOfSim tmpState = new StateOfSim(Material, Robots);
//        if (tmpState.IsLessThen(states[remSteps]))
//            return -10000;
//        if (states[remSteps].IsLessThen(tmpState))
//            states[remSteps] = tmpState;

//        int canBuyOreMine;
//        int canBuyClayMine;
//        int canBuyObsiMine;
//        int canBuyGeodMine;

//        int canBuyOreMineInTheEnd;
//        int canBuyClayMineInTheEnd;
//        int canBuyObsiMineInTheEnd;
//        int canBuyGeodMineInTheEnd;

//        int maxGain = 0;
//        int tmp;

//        //= CanBuySpec(ORE, couldBoughtOre);
//        Tuple<int, int, int, int> tupple;
//        tupple = GetMaterialGain();
//        int i, j, k, l;
//        //for (i = 0; i <= canBuyOreMine; i++)

//        canBuyGeodMine = CanBuySpec(GEOD, couldBoughtGeod);
//        //for (l = 0; l <= canBuyGeodMine; l++)
//        //{
//        l = canBuyGeodMine;
//        Buy(GEOD, l);

//        canBuyObsiMine = CanBuySpec(OBSI, couldBoughtObsi);
//        //for (k = 0; k <= canBuyObsiMine; k++)
//        for (k = canBuyObsiMine; k >= 0; k--)
//        {
//            Buy(OBSI, k);
//            canBuyClayMine = CanBuySpec(CLAY, couldBoughtClay);
//            //for (j = 0; j <= canBuyClayMine; j++)
//            for (j = canBuyClayMine; j >= 0; j--)
//            {
//                Buy(CLAY, j);
//                canBuyOreMine = CanBuySpec(ORE, couldBoughtOre);
//                for (i = canBuyOreMine; i >= 0; i--)
//                {
//                    Buy(ORE, i);

//                    canBuyOreMineInTheEnd = CanBuy(ORE);
//                    canBuyClayMineInTheEnd = CanBuy(CLAY);
//                    canBuyObsiMineInTheEnd = CanBuy(OBSI);
//                    canBuyGeodMineInTheEnd = CanBuy(GEOD);


//                    AddMaterials(tupple);

//                    tmp = GetBestOres(remSteps - 1, canBuyOreMineInTheEnd, canBuyClayMineInTheEnd, canBuyObsiMineInTheEnd, canBuyGeodMineInTheEnd);
//                    maxGain = Math.Max(maxGain, tmp);

//                    RemoveMaterials(tupple);

//                    //}
//                    RevertBuy(ORE, i);
//                }
//                RevertBuy(CLAY, j);
//            }
//            RevertBuy(OBSI, k);
//        }

//        RevertBuy(GEOD, l);


//        return maxGain;
//    }


    internal int GetBestOresBFS(int nOfSteps)
    {
        HashSet<StateBFS> old = new HashSet<StateBFS>();
        old.Add(new StateBFS(new int[] { 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 } ));
        int i;
        int maxHeur;
        for (i = 0; i < nOfSteps; i++)
        {
            HashSet<StateBFS> newtmpL = new HashSet<StateBFS>();
            foreach (StateBFS oldState in old)
            {
                SimOneStep(oldState, newtmpL);
            }
            //maxHeur = 0;
            //foreach (StateBFS stateState in newtmpL)
            //{
            //    maxHeur = Math.Max(maxHeur, stateState.GetHeur());
            //}
            //old = new List<StateBFS>();

            //foreach(StateBFS stateState in newtmpL)
            //{
            //    if(stateState.GetHeur()>maxHeur-60)
            //        old.Add(stateState);
            //}
            if (newtmpL.Count > 2000)
            {
                List<StateBFS> tmpList = newtmpL.ToList();
                tmpList.Sort((x, y) => y.GetHeur() - x.GetHeur());
                old = tmpList.Take(2000).ToHashSet();
            }
            else
            {
                old = newtmpL;

            }
        }
        int maxret = 0;
        StateBFS bestState = new StateBFS(new int[] { 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 });
        foreach (StateBFS oldState in old)
        {
            if (oldState.Material[3] > maxret)
            {
                maxret = oldState.Material[3];
                bestState=oldState;
            }
        }
        int ind = 0;
        //bestState.PrintHistory(ref ind) ;
        //Console.WriteLine("") ;
        return maxret;
    }


    private void SimOneStepOld(StateBFS oldState, HashSet<StateBFS> newL)
    {
        int canBuyOreMine;
        int canBuyClayMine;
        int canBuyObsiMine;
        int canBuyGeodMine;

        Tuple<int, int, int, int> tupple;
        tupple = GetMaterialGain(oldState);
        int i, j, k, l;
        //for (i = 0; i <= canBuyOreMine; i++)

        canBuyGeodMine = CanBuy(GEOD, oldState);
        //for (l = 0; l <= canBuyGeodMine; l++)
        //{
        l = canBuyGeodMine;
        Buy(GEOD, l, oldState);

        canBuyObsiMine = CanBuy(OBSI, oldState);
        //for (k = 0; k <= canBuyObsiMine; k++)
        for (k = canBuyObsiMine; k >= 0; k--)
        {
            Buy(OBSI, k, oldState);
            canBuyClayMine = CanBuy(CLAY, oldState);
            //for (j = 0; j <= canBuyClayMine; j++)
            for (j = canBuyClayMine; j >= 0; j--)
            {
                Buy(CLAY, j, oldState);
                canBuyOreMine = CanBuy(ORE, oldState);
                for (i = canBuyOreMine; i >= 0; i--)
                {
                    Buy(ORE, i, oldState);



                    AddMaterials(tupple, oldState);


                    newL.Add(new StateBFS(oldState));

                    RemoveMaterials(tupple, oldState);

                    //}
                    RevertBuy(ORE, i, oldState);
                }
                RevertBuy(CLAY, j, oldState);
            }
            RevertBuy(OBSI, k, oldState);
        }

        RevertBuy(GEOD, l, oldState);
    }


    private void SimOneStep(StateBFS oldState, HashSet<StateBFS> newL)
    {
        int canBuyOreMine= CanBuy(ORE, oldState);
        int canBuyClayMine = CanBuy(CLAY, oldState);
        int canBuyObsiMine = CanBuy(OBSI, oldState);
        int canBuyGeodMine = CanBuy(GEOD, oldState);

        Tuple<int, int, int, int> tupple;
        tupple = GetMaterialGain(oldState);
        int i, j, k, l;
        //for (i = 0; i <= canBuyOreMine; i++)

        //for (l = 0; l <= canBuyGeodMine; l++)
        //{

        if (canBuyGeodMine > 0)
        {
            Buy(GEOD, 1, oldState);
            AddMaterials(tupple, oldState);
            newL.Add(new StateBFS(oldState));
            RemoveMaterials(tupple, oldState);
            RevertBuy(GEOD, 1, oldState);
            return;
        }

        if (canBuyObsiMine > 0)
        {
            Buy(OBSI, 1, oldState);
            AddMaterials(tupple, oldState);
            newL.Add(new StateBFS(oldState));
            RemoveMaterials(tupple, oldState);
            RevertBuy(OBSI, 1, oldState);
            //return;
        }

        if (canBuyOreMine > 0)
        {
            Buy(ORE, 1, oldState);
            AddMaterials(tupple, oldState);
            newL.Add(new StateBFS(oldState));
            RemoveMaterials(tupple, oldState);
            RevertBuy(ORE, 1, oldState);
        }
        if (canBuyClayMine > 0)
        {
            Buy(CLAY, 1, oldState);
            AddMaterials(tupple, oldState);
            newL.Add(new StateBFS(oldState));
            RemoveMaterials(tupple, oldState);
            RevertBuy(CLAY, 1, oldState);
        }
        AddMaterials(tupple, oldState);
        newL.Add(new StateBFS(oldState));
        RemoveMaterials(tupple, oldState);
    }

    //private void RemoveMaterials(Tuple<int, int, int, int> tupple)
    //{
    //    Material[0] -= tupple.Item1;
    //    Material[1] -= tupple.Item2;
    //    Material[2] -= tupple.Item3;
    //    Material[3] -= tupple.Item4;
    //}

    private void RemoveMaterials(Tuple<int, int, int, int> tupple, StateBFS stateBFS)
    {
        stateBFS.Material[0] -= tupple.Item1;
        stateBFS.Material[1] -= tupple.Item2;
        stateBFS.Material[2] -= tupple.Item3;
        stateBFS.Material[3] -= tupple.Item4;
    }

    //private void AddMaterials(Tuple<int, int, int, int> tupple)
    //{
    //    Material[0] += tupple.Item1;
    //    Material[1] += tupple.Item2;
    //    Material[2] += tupple.Item3;
    //    Material[3] += tupple.Item4;
    //}

    private void AddMaterials(Tuple<int, int, int, int> tupple, StateBFS stateBFS)
    {
        stateBFS.Material[0] += tupple.Item1;
        stateBFS.Material[1] += tupple.Item2;
        stateBFS.Material[2] += tupple.Item3;
        stateBFS.Material[3] += tupple.Item4;
    }

    private int CanBuySpec(int type, int previous, StateBFS stateBFS)
    {
        if (previous > 0)
            return 0;

        return CanBuy(type, stateBFS);
    }
    //private int CanBuySpec(int type, int previous)
    //{
    //    if (previous > 0)
    //        return 0;

    //    return CanBuy(type);
    //}

    //private int CanBuy(int type)
    //{
    //    int max = int.MaxValue;
    //    int tmp;
    //    for (int i = 0; i < NOfGeodes; i++)
    //    {
    //        if (Cost[type, i] == 0) continue;
    //        tmp = Material[i] / Cost[type, i];
    //        if (tmp < max)
    //            max = tmp;
    //    }

    //    return max;
    //}
    private int CanBuy(int type, StateBFS stateBFS)
    {
        int max = int.MaxValue;
        int tmp;
        for (int i = 0; i < NOfGeodes; i++)
        {
            if (Cost[type, i] == 0) continue;
            tmp = stateBFS.Material[i] / Cost[type, i];
            if (tmp < max)
                max = tmp;
        }

        return max;
    }
    //private void Buy(int type, int amoutToBuy)
    //{

    //    for (int i = 0; i < NOfGeodes; i++)
    //        Material[i] -= (Cost[type, i] * amoutToBuy);

    //    Robots[type] += amoutToBuy;
    //}

    private void Buy(int type, int amoutToBuy, StateBFS stateBFS)
    {

        for (int i = 0; i < NOfGeodes; i++)
            stateBFS.Material[i] -= (Cost[type, i] * amoutToBuy);

        stateBFS.Robots[type] += amoutToBuy;
    }
    //private void RevertBuy(int type, int amoutToRevert)
    //{

    //    for (int i = 0; i < NOfGeodes; i++)
    //        Material[i] += (Cost[type, i] * amoutToRevert);

    //    Robots[type] -= amoutToRevert;
    //}

    private void RevertBuy(int type, int amoutToRevert, StateBFS stateBFS)
    {

        for (int i = 0; i < NOfGeodes; i++)
            stateBFS.Material[i] += (Cost[type, i] * amoutToRevert);

        stateBFS.Robots[type] -= amoutToRevert;
    }

    //private void AddMaterials()
    //{
    //    for (int i = 0; i < NOfGeodes; i++)
    //    {
    //        Material[i] += Robots[i];
    //    }
    //}

    //private Tuple<int, int, int, int> GetMaterialGain()
    //{
    //    return new Tuple<int, int, int, int>(Robots[0], Robots[1], Robots[2], Robots[3]);
    //}

    private Tuple<int, int, int, int> GetMaterialGain(StateBFS stateBFS)
    {
        return new Tuple<int, int, int, int>(stateBFS.Robots[0], stateBFS.Robots[1], stateBFS.Robots[2], stateBFS.Robots[3]);
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