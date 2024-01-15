using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretLibrary : MonoBehaviour
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
        if (other.gameObject.tag == "SecretLibraryPlacementArea") // Adjust the tag as needed
        {
            BuildingPlacementArea placementArea = other.GetComponent<BuildingPlacementArea>();
            if (placementArea != null && placementArea.CanPlaceBuilding())
            {
                PlaceSecretLibrary();
            }
            else
            {
                Debug.Log("Cannot place Secret Library here.");
            }
        }
    }

    private void PlaceSecretLibrary()
    {
        if (!isPlaced)
        {
            building.isPlaced = true;
            isPlaced = true;

            int repSovietChange = GameManager.instance.CurrentResources.RepSoviet > 0 ? -1 : 1;
            int repPeopleChange = GameManager.instance.CurrentResources.RepPeople < 0 ? 1 : 0;

            Effect secretLibraryEffect = new Effect(0, 0, 0, repSovietChange, repPeopleChange, 0);
            GameManager.instance.Effects.Add(secretLibraryEffect);
            GameManager.instance.ApplyChoiceChange(secretLibraryEffect);

            Debug.Log("Secret Library placed successfully.");
        }
        else
        {
            Debug.Log("Secret Library has already been placed.");
        }
    }
}
