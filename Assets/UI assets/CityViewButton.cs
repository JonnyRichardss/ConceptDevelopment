using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityViewButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.hasBuildings)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
