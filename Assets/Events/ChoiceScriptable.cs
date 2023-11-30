using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventObjects/choices")]
public class ChoiceScriptable : ScriptableObject
{
    public string m_choiceName;
    [SerializeField] string m_choiceTitle;
    [SerializeField] string m_choiceText;
    public Effect m_choiceEffect;
}
