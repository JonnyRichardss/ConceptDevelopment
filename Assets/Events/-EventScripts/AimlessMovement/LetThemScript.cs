using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventScripts/LetThem")]
public class LetThemScript : AbstractEventScript
{
    override public void TriggerEventScript()
    {
        GameManager.instance.Effects.Add(new Effect(0, 0, Random.Range(-10, 11), 0, 0, 0));
    }
}
