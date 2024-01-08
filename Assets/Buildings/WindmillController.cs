using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillController : MonoBehaviour
{
    BoxCollider farmCollider;
    BuildingController building;
    public List<BuildingController> newFarms = new List<BuildingController>();
    public int adjacencyBonus = 5;
    // Start is called before the first frame update
    void Start()
    {
        farmCollider = GetComponent<BoxCollider>();
        building = transform.parent.GetComponent<BuildingController>();
    }

    private void FixedUpdate()
    {
        CheckNewFarms();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Farm")
        {
            newFarms.Add(other.transform.GetComponent<BuildingController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Farm")
        {
            newFarms.Remove(other.transform.GetComponent<BuildingController>());
        }
    }

    void CheckNewFarms()
    {
        for (int i = 0; i < newFarms.Count; i++)
        {
            if (newFarms[i].isPlaced && building.isPlaced)
            {
                building.neighbours.Add(newFarms[i]);
                newFarms.RemoveAt(i);
                GameManager.instance.Effects.Add(new Effect(0, 5, 0, 0, 0, 0));
                print(building.buildingEffect.m_buildingEffect.Food);
            }
        }
    }

    
}
