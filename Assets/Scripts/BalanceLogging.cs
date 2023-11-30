using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class BalanceLogging
{
    static public void Log(EventScriptable e, ChoiceScriptable c)
    {
        Debug.Log("Logged new choice");
        //make log of choice made
    }
    static public void Log(Effect e)
    {
        Debug.Log("Logged new effect");
        //make log of new resources
    }
    static public void Log(BuildingScriptable b)
    {
        Debug.Log("Logged new building");
        //make log of building placed
    }
}
