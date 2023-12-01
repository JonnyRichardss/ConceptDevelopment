using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EventTestScript : MonoBehaviour
{
    public Canvas UI;
    public EventScriptable TestEvent;
    // Start is called before the first frame update
    void Start()
    {
        SetEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEvent()
    {
        UI.GetComponent<TestUIController>().SetEventUI(TestEvent);
    }
}
