using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretLibrary : MonoBehaviour
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
            PlaceSecretLibrary();
            isCheckingPlacement = false; // Stop continuous checking once placed
        }
    }

    private void PlaceSecretLibrary()
    {
        if (!isPlaced)
        {
            building.isPlaced = true;
            isPlaced = true;
            
            int sovietRep = GameManager.instance.Resources.RepSoviet;
            int peopleRep = GameManager.instance.Resources.RepPeople;

            
            int resourceChange = 0;
            int duration = 1;

            
            if (sovietRep > 0)
            {
                
                resourceChange = -1;
                duration = 1;

                Debug.Log("Secret Library placed successfully. Decreased Soviet reputation.");
            }
            
            if (peopleRep < 0)
            {
                
                resourceChange = +1;
                duration = 1;

                Debug.Log("Secret Library placed successfully. Increased People's reputation.");
            }

            
            if (resourceChange != 0)
            {
                Effect secretLibraryEffect = new Effect(resourceChange, duration);
                GameManager.instance.Effects.Add(secretLibraryEffect);
                GameManager.instance.ApplyChoiceChange(secretLibraryEffect);
            }
            else
            {
                Debug.Log("Secret Library placed successfully. No reputation changes needed.");
            }
        }
        else
        {
            Debug.Log("Secret Library has already been placed.");
        }
    }
}
        
    
        

