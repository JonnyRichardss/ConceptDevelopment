using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebelOupostController : MonoBehaviour
{
    BuildingController building;
    public bool isPlaced;

    void Start()
    {
        building = transform.parent.GetComponent<BuildingController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RebelOutpostPlacementArea") 
        {
            BuildingPlacementArea placementArea = other.GetComponent<BuildingPlacementArea>();
            if (placementArea != null && placementArea.CanPlaceBuilding())
            {
                PlaceRebelOutpost();
            }
            else
            {
                Debug.Log("Cannot place Rebel Outpost here.");
            }
        }
    }

    private void PlaceRebelOutpost()
    {
        if (!isPlaced)
        {
            building.isPlaced = true;
            isPlaced = true;

            Effect rebelOutpostEffect = new Effect(0, 0, 0, 0, 1, 0); 
            GameManager.instance.Effects.Add(rebelOutpostEffect);
            GameManager.instance.ApplyChoiceChange(rebelOutpostEffect);

            Debug.Log("Rebel Outpost placed successfully.");
        }
        else
        {
            Debug.Log("Rebel Outpost has already been placed.");
        }
    }
}

