using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    // not sure if the EventManager wants to be static or singleton

    //presumably have like lists of drawn and not-yet-drawn events

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //placeholder for now, this is just an example of what I *thought* the interface could look like
    
    public static EventScriptable DrawEvent(ResourceHolder weighting)
    {
        return new EventScriptable();
    }
}
