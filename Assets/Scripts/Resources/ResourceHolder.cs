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
    public void ApplyEffect(Effect e)
    {

        Food += e.Food;
        Population += e.Population;
        Suspicion += e.Suspicion;
        RepSoviet += e.RepSoviet;
        RepPeople += e.RepPeople;
    }
    public bool TryApplyEffect(Effect e)
    {
        if (e.Duration == -1)
        {
            ApplyEffect(e);
            return true;
        }
        else if (e.Duration > 0)
        {
            e.Duration -= 1;
            ApplyEffect(e);
            return true;
        }
        else if (e.Duration == 0)
        {
            ApplyEffect(e);
            return false;
        }
        else
        {
            return false;
        }
    }
}
