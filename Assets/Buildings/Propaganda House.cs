using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropagandaHouse : MonoBehaviour
{
    BuildingController building;
    public bool isPlaced;
    private bool isCheckingPlacement = true; // Boolean to control the continuous checking

    // Start is called before the first frame update
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
            PlacePropagandaHouse();
            isCheckingPlacement = false; // Stop continuous checking once placed
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
