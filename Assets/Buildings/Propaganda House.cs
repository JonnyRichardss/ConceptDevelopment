using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropagandaHouse : MonoBehaviour
{
    BuildingController building;
    public bool isPlaced;

    // Start is called before the first frame update
    void Start()
    {
        building = transform.parent.GetComponent<BuildingController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HousePlacementArea")
        {
            BuildingPlacementArea placementArea = other.GetComponent<BuildingPlacementArea>();
            if (placementArea != null && placementArea.CanPlaceBuilding())
            {
                PlacePropagandaHouse();
            }
            else
            {
                Debug.Log("Cannot place Propaganda House here.");
            }
        }
    }

    private void PlacePropagandaHouse()
    {
        if (!isPlaced)
        {
            building.isPlaced = true;
            isPlaced = true;

            
            Effect propagandaEffect = new Effect(0, 0, 0, 1, 0, 0);
            GameManager.instance.Effects.Add(propagandaEffect);
            GameManager.instance.ApplyChoiceChange(propagandaEffect);

            Debug.Log("Propaganda House placed successfully.");
        }
        else
        {
            Debug.Log("Propaganda House has already been placed.");
        }
    }   


}
