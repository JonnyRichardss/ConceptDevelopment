using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Buildings")]
public class BuildingScriptable : ScriptableObject
{
    [SerializeField] string m_buildingName;
    [SerializeField] string m_buildingText;
    [SerializeField] Effect m_buildingEffect;
    [SerializeField] Vector2 m_buildingCoordinates;

}

