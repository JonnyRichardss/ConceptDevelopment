using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    Transform touchingBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlaced();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            transform.GetComponent<MeshRenderer>().enabled = false;
            touchingBuilding = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            transform.GetComponent<MeshRenderer>().enabled = true;
            touchingBuilding = null;
        }
    }

    void CheckPlaced()
    {
        if (touchingBuilding != null)
        {
            if (touchingBuilding.GetComponent<BuildingController>().isPlaced)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }
}
