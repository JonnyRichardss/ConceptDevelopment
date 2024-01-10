using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceHolder : ILoggable
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

    public ResourceHolder(float food=0, float population=0, float suspicion=0, int repSoviet=0, int repPeople=0)
    {
        Food = food;
        Population = population;
        Suspicion = suspicion;
        RepSoviet = repSoviet;
        RepPeople = repPeople;
    }
    private void ApplyEffect(Effect e)
    {

        Food += e.Food;
        Population += e.Population;
        Suspicion += e.Suspicion;
        RepSoviet += e.RepSoviet;
        RepPeople += e.RepPeople;
        BalanceLogging.Log(this);
    }

    public void RemoveEffect(Effect e)
    {
        Food -= e.Food;
        Population -= e.Population;
        Suspicion -= e.Suspicion;
        RepSoviet -= e.RepSoviet;
        RepPeople -= e.RepPeople;
        BalanceLogging.Log(this);
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
    public string ToLogString()
    {
        string output = String.Format(
            "Updated Resources:\n" +
            "Food: {0}\n" +
            "Population: {1}\n" +
            "Suspicion: {2}\n" +
            "Soviet Rep: {3}\n" +
            "People Rep: {4}",
            Food, Population, Suspicion, RepSoviet, RepPeople);
        


        return output;
    }
    public string LogDebugMessage()
    {
        return "Logged resources after new effect";
    }
}
