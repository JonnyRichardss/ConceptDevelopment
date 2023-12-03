using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Events")]
public class EventScriptable : ScriptableObject
{
    public string m_eventName;
    public string m_eventTitle;
    public string m_eventText;
    public ChoiceScriptable m_firstChoice;
    public ChoiceScriptable m_secondChoice;
}
