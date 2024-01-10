using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.hasEvents)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
