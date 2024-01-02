using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingPlacement : MonoBehaviour
{
    public bool isPlacingBuilding = true;
    public List<Transform> AllBuildings = new List<Transform>();
    public Transform currentBuilding;
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentBuilding(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlacingBuilding)
        {
            PlaceBuilding();
        }
    }

    void PlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, LayerMask.GetMask("Map")))
        {
            currentBuilding.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
        }
    }

    public void SetCurrentBuilding(int type)
    {
        isPlacingBuilding = true;
        switch (type)
        {
            case 0:
                currentBuilding = AllBuildings[0].GetChild(GetNextBuilding(AllBuildings[0]));
                break;
            case 1:
                currentBuilding = AllBuildings[1].GetChild(GetNextBuilding(AllBuildings[0]));
                break;
            case 2:
                currentBuilding = AllBuildings[2].GetChild(GetNextBuilding(AllBuildings[0]));
                break;
            case 3:
                currentBuilding = AllBuildings[3].GetChild(GetNextBuilding(AllBuildings[0]));
                break;
        }
        currentBuilding.gameObject.SetActive(true);
    }

    int GetNextBuilding(Transform building)
    {
        int position = 0;
        for (int i = 0; i < building.childCount; i++)
        {
            if (!building.GetChild(i).gameObject.activeSelf)
            {
                position = i;
                break;
            }
        }
        return position;
    }

    void OnPlaceBuilding()
    {
        isPlacingBuilding = false;
        currentBuilding = null;
    }
}
