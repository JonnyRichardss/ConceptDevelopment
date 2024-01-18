using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventObjects/choices")]
public class ChoiceScriptable : ScriptableObject
{
    public string m_choiceName;
    [SerializeField] public string m_choiceTitle;
    [SerializeField] public string m_choiceText;
    [SerializeField] public Effect m_choiceEffect;
    [SerializeField] public AbstractEventScript m_eventScript;
    public string tooltip;
}
