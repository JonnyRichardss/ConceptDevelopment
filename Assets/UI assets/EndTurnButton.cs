using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.hasBuildings || !GameManager.instance.hasEvents)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
