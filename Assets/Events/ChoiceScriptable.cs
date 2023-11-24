using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventObjects/choices")]
public class ChoiceScriptable : ScriptableObject
{
    [SerializeField] string m_choiceName;
    [SerializeField] string m_choiceText;
    [SerializeField] Effect m_effect;
}
