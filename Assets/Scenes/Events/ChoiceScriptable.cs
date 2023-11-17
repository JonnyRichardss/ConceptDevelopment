using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/EventObjects/choices")]
public class ChoiceScriptable : ScriptableObject
{
    [SerializeField] string m_choiceName;
    [SerializeField] string m_choiceText;
    [SerializeField] int m_foodChange;
    [SerializeField] int m_populationChange;
    [SerializeField] int m_peopleRepChange;
    [SerializeField] int m_ussrRepChange;
}
