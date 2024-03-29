using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public bool isPlaceable = true;
    bool overlappingBuilding = false;
    public bool isPlaced = false;
    public List<BuildingController> neighbours = new List<BuildingController>();

    public BuildingScriptable buildingEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckBoundaries();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (!isPlaced)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                overlappingBuilding = true;
                isPlaceable = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (!isPlaced)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                overlappingBuilding = true;
                isPlaceable = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (!isPlaced)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                overlappingBuilding = false;
                isPlaceable = true;
            }
        }
    }

    void CheckBoundaries()
    {
        if (!isPlaced && !overlappingBuilding)
        {
            if (RaycastBoundaries(1, 1) && RaycastBoundaries(1, -1) && RaycastBoundaries(-1, 1) && RaycastBoundaries(-1, -1))
            {
                isPlaceable = true;
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                isPlaceable = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    bool RaycastBoundaries(int x, int z)
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 RaycastOffset = transform.position + new Vector3((collider.size.x/2) * x, 1f, (collider.size.z/2) * z);
        Debug.DrawRay(RaycastOffset, Vector3.down);
        if (Physics.Raycast(RaycastOffset, Vector3.down, 1f))
        {
            return true;
        }
        return false;
    }
}
