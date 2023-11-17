using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder
{
    public float Food
    {
        get;
        private set;
    }
    public float Population
    {
        get;
        private set;
    }
    public float Suspicion
    {
        get;
        private set;
    }
    public int RepSoviet
    {
        get;
        private set;
    }
    public int RepPeople
    {
        get;
        private set;
    }
    ResourceHolder()
    {
        Food = 0;
        Population = 0;
        Suspicion = 0;
        RepSoviet = 0;
        RepPeople = 0;
    }
    ResourceHolder(float food, float population, float suspicion, int repSoviet, int repPeople)
    {
        Food = food;
        Population = population;
        Suspicion = suspicion;
        RepSoviet = repSoviet;
        RepPeople = repPeople;
    }
}
