using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Events")]
public class EventScriptable : ScriptableObject
{
    public string m_eventName;
    [SerializeField] string m_eventTitle;
    [SerializeField] string m_eventText;
    [SerializeField] ChoiceScriptable m_firstChoice;
    [SerializeField] ChoiceScriptable m_secondChoice;
}
