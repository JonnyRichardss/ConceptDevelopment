using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder
{
    public float Food
    {
        get;
        set;
    }
    public float Population
    {
        get;
        set;
    }
    public float Suspicion
    {
        get;
        set;
    }
    public int RepSoviet
    {
        get;
        set;
    }
    public int RepPeople
    {
        get;
        set;
    }
    public ResourceHolder()
    {
        Food = 0;
        Population = 0;
        Suspicion = 0;
        RepSoviet = 0;
        RepPeople = 0;
    }
    public ResourceHolder(float food, float population, float suspicion, int repSoviet, int repPeople)
    {
        Food = food;
        Population = population;
        Suspicion = suspicion;
        RepSoviet = repSoviet;
        RepPeople = repPeople;
    }
}
