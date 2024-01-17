using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "My Assets/EventScripts/choices")]
public class AbstractEventScript : ScriptableObject
{
    virtual public void TriggerEventScript()
    {

    }
}
