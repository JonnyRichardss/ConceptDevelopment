using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Effect
{
    public int Duration;

    public float Food;

    public float Population;

    public float Suspicion;

    public int RepSoviet;

    public int RepPeople;

    public Effect(int duration = 0, float food=0, float population = 0, float suspicion = 0, int repSoviet=0, int repPeople = 0)
    {
        Duration = duration;
        Food = food;
        Population = population;
        Suspicion = suspicion;
        RepSoviet = repSoviet;
        RepPeople = repPeople;
    }
    public static Effect operator + (Effect a, Effect b)
    {
        Effect output = new Effect();
        output.Duration = a.Duration > b.Duration ? a.Duration : b.Duration; //pick the longer duration from A or B
        output.Food = a.Food + b.Food;
        output.Population = a.Population + b.Population;
        output.Suspicion = a.Suspicion + b.Suspicion;
        output.RepSoviet = a.RepSoviet + b.RepSoviet;
        output.RepPeople = a.RepPeople + b.RepPeople;
        return output;
    }
}
