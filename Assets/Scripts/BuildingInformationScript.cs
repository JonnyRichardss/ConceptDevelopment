using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingInformationScript : MonoBehaviour
{
    public Transform Tooltip;
    TextMeshProUGUI tipText;
    // Update is called once per frame
    private void Start()
    {
        tipText = Tooltip.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void FixedUpdate()
    {
        HoverOverBuilding();
    }

    void HoverOverBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!BuildingPlacement.instance.choosingCards && Physics.Raycast(ray, out RaycastHit hit, 1000, LayerMask.GetMask("Building")))
        {
            SetTooltip(hit.transform);
            Tooltip.position = Input.mousePosition + new Vector3(250, 0, 0);
        }
        else
        {
            Tooltip.gameObject.SetActive(false);
        }
    }

    void SetTooltip(Transform hitBuilding)
    {
        BuildingScriptable building = hitBuilding.GetComponent<BuildingController>().buildingEffect;
        Tooltip.gameObject.SetActive(true);
        switch (building.m_buildingName)
        {
            case "Collective Farm":
                tipText.text = "+" + building.m_buildingEffect.Food + " food per turn";
                break;
            case "Farm":
                tipText.text = "+" + building.m_buildingEffect.Food + " food per turn";
                break;
            case "Food Stash":
                
                tipText.text = "Left click to stror food - Right click to take food\n currently contains " + hitBuilding.GetComponent<FoodStashController>().foodAmount + " food.";
                break;
            case "House":
                tipText.text = "Increases population by " + building.m_buildingEffect.Population + " when placed";
                break;
            case "Orphanage":
                tipText.text = "Increases population by " + building.m_buildingEffect.Population + " when placed";
                break;
            case "Propaganda House":
                tipText.text = "Increases Soviet reputation by 1 when placed";
                break;
            case "Rebel Outpost":
                tipText.text = "Increases People reputation by 1 when placed";
                break;
            case "Secret Library":
                tipText.text = "Increases or decreases both Soviet and People reputation toward 0 by 1 when placed";
                break;
            case "Windmill":
                tipText.text = "Increases food by 5 when placed, per adjacent farm or collective farm, per turn";
                break;
            default:
                tipText.text = "This building is outside the buildings set inside buildingInformationScript.";
                break;
        }
    }
}
