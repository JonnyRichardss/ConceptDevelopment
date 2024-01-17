using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebelOupostController : MonoBehaviour
{
    BuildingController building;
    public bool isPlaced;
    private bool isCheckingPlacement = true; // Boolean to control the continuous checking

    void Start()
    {
        building = transform.parent.GetComponent<BuildingController>();
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        // Check if the building is not placed and we are still allowed to check placement
        if (!isPlaced && isCheckingPlacement)
        {
            CheckBuildingPlacement();
        }
    }

    private void CheckBuildingPlacement()
    {
        // Check if the associated BuildingController is placed
        if (building.isPlaced)
        {
            PlaceRebelOutpost();
            isCheckingPlacement = false; // Stop continuous checking once placed
        }
    }

    private void PlaceRebelOutpost()
    {
        if (!isPlaced)
        {
            building.isPlaced = true;
            isPlaced = true;

            Effect rebelOutpostEffect = new Effect(0, 0, 0, 0, 0, 1);
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

