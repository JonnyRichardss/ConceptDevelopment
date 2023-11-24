using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Effect
{
    public float Food;

    public float Population;

    public float Suspicion;

    public int RepSoviet;

    public int RepPeople;

    public Effect(float food=0, float population = 0, float suspicion = 0, int repSoviet=0, int repPeople = 0)
    {
        Food = food;
        Population = population;
        Suspicion = suspicion;
        RepSoviet = repSoviet;
        RepPeople = repPeople;
    }
}
