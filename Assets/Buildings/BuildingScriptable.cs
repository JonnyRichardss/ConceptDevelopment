using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Buildings")]
public class BuildingScriptable : ScriptableObject
{
    [SerializeField] string m_buildingName;
    [SerializeField] string m_buildingText;
    [SerializeField] public Effect m_buildingEffect;
    [SerializeField] Vector2 m_buildingCoordinates;
    public string ToLogString() 
    {
        return "";
    }
    public void ApplyToNeighbours()
    {
        foreach(BuildingScriptable neighbour in GetNeighbours())
        {
            neighbour.m_buildingEffect += m_buildingEffect;
        }
    }
    public List<BuildingScriptable> GetNeighbours()
    {
        return new List<BuildingScriptable>();
    }
}

