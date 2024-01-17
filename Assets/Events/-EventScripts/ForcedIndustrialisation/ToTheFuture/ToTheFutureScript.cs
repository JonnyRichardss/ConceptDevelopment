using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventScripts/ToTheFuture")]
public class ToTheFutureScript : AbstractEventScript
{
    override public void TriggerEventScript()
    {
        GameManager.instance.isIndustrialisation = true;
        GameManager.instance.state = GameState.DrawBuildings;
        GameManager.instance.SwitchScene();
    }
}
