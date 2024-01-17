using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventScripts/StopThem")]
public class StopThemScript : AbstractEventScript
{
    override public void TriggerEventScript()
    {
        GameManager.instance.Effects.Add(new Effect(0, 0, Random.Range(-4, 5), 0, 0, 0));
    }
}
