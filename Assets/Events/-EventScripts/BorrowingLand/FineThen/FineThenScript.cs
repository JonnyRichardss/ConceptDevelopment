using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventScripts/FineThen")]
public class FineThenScript : AbstractEventScript
{
    override public void TriggerEventScript()
    {
        if (GameObject.Find("Farms") != null)
        {
            Transform farms = GameObject.Find("Farms").transform;
            for (int i = 0; i < farms.childCount; i++)
            {
                if (farms.GetChild(i).gameObject.activeSelf) 
                {
                    farms.GetChild(i).GetComponent<BuildingController>().isPlaced = false;
                    farms.GetChild(i).gameObject.SetActive(false);
                    GameManager.instance.Effects.Add(new Effect(-1, -10, 0, 0, 0, 0));
                    break;
                }
            }
        }
    }
}
