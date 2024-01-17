using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacement : MonoBehaviour
{
    public List<int> NewDrawPile = new List<int>();
    public List<int> DrawPile = new List<int>();
    List<int> DiscardPile = new List<int>();

    public static BuildingPlacement instance;
    public bool isPlacingBuilding = false;
    public bool choosingCards = true;
    public List<Transform> AllBuildings = new List<Transform>();
    public Transform currentBuilding;
    public Transform cards;

    Action<bool> callback;

    private void Awake()
    {
        //makes sure gamemanager only exists once
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Refresh();
    }
    // Start is called before the first frame update
    void Start()
    {
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
            currentBuilding.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y) + 0.25f, Mathf.Round(hit.point.z));
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
                currentBuilding = AllBuildings[1].GetChild(GetNextBuilding(AllBuildings[1]));
                break;
            case 2:
                currentBuilding = AllBuildings[2].GetChild(GetNextBuilding(AllBuildings[2]));
                break;
            case 3:
                currentBuilding = AllBuildings[3].GetChild(GetNextBuilding(AllBuildings[3]));
                break;
            case 4:
                currentBuilding = AllBuildings[4].GetChild(GetNextBuilding(AllBuildings[4]));
                break;
            case 5:
                currentBuilding = AllBuildings[5].GetChild(GetNextBuilding(AllBuildings[5]));
                break;
            case 6:
                currentBuilding = AllBuildings[6].GetChild(GetNextBuilding(AllBuildings[6]));
                break;
            case 7:
                currentBuilding = AllBuildings[7].GetChild(GetNextBuilding(AllBuildings[7]));
                break;
            case 8:
                currentBuilding = AllBuildings[8].GetChild(GetNextBuilding(AllBuildings[8]));
                break;
            default:
                Debug.Log("no building");
                break;
        }
        currentBuilding.gameObject.SetActive(true);
        choosingCards = false;
        cards.gameObject.SetActive(false);
    }

    public Transform GetDrawnBuilding(int type)
    {
        switch (type)
        {
            case 0:
                return AllBuildings[0].GetChild(GetNextBuilding(AllBuildings[0]));
            case 1:
                return AllBuildings[1].GetChild(GetNextBuilding(AllBuildings[1]));
            case 2:
                return AllBuildings[2].GetChild(GetNextBuilding(AllBuildings[2]));
            case 3:
                return AllBuildings[3].GetChild(GetNextBuilding(AllBuildings[3]));
            case 4:
                return AllBuildings[4].GetChild(GetNextBuilding(AllBuildings[4]));
            case 5:
                return AllBuildings[5].GetChild(GetNextBuilding(AllBuildings[5]));
            case 6:
                return AllBuildings[6].GetChild(GetNextBuilding(AllBuildings[6]));
            case 7:
                return AllBuildings[7].GetChild(GetNextBuilding(AllBuildings[7]));
            case 8:
                return AllBuildings[8].GetChild(GetNextBuilding(AllBuildings[8]));
            default: return null;
        }
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
        if (currentBuilding != null)
        {
            BuildingController currentController = currentBuilding.GetComponent<BuildingController>();
            if (currentController.isPlaceable)
            {
                isPlacingBuilding = false;
                GameManager.instance.Effects.Add(currentBuilding.GetComponent<BuildingController>().buildingEffect.m_buildingEffect);
                GameManager.instance.ApplyChoiceChange(currentBuilding.GetComponent<BuildingController>().buildingEffect.m_buildingEffect);
                currentController.isPlaced = true;
                currentBuilding = null;
                callback(true);
            }
            else
            {
                Debug.Log("can't build there");
            }
        }
    }

    public void Refresh()
    {
        DiscardPile.Clear();
        DrawPile = new List<int>(NewDrawPile);
    }

    public void DrawBuildings(Action<bool> _callback)
    {
        callback = _callback;
        choosingCards = true;
        //StartCoroutine(DrawBuildingsRoutine());
        cards = GameObject.Find("Cards").transform;
        cards.gameObject.SetActive(true);
        if (DrawPile.Count <= 3)
        {
            Refresh();
        }
        for (int i = 0; i < 3; i++)
        {
            int position = UnityEngine.Random.Range(0, DrawPile.Count);
            int deckChoice = DrawPile[position];
            DrawPile.RemoveAt(position);
            Transform DrawnBuilding = GetDrawnBuilding(deckChoice);
            Transform currentCard = cards.GetChild(i);
            currentCard.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            currentCard.GetComponentInChildren<Button>().onClick.AddListener(() => SetCurrentBuilding(deckChoice));
            BuildingScriptable buildingEffect = DrawnBuilding.GetComponent<BuildingController>().buildingEffect;
            currentCard.GetChild(0).GetComponent<TextMeshProUGUI>().text = buildingEffect.name;
            switch (buildingEffect.m_buildingName)
            {
                case "Collective Farm":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + buildingEffect.m_buildingEffect.Food + " food per turn";
                    break;
                case "Farm":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + buildingEffect.m_buildingEffect.Food + " food per turn";
                    break;
                case "FoodStash":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Can store food here for later.\n High non-hidden food increases suspicion.";
                    break;
                case "House":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Increases population by " + buildingEffect.m_buildingEffect.Population + " when placed";
                    break;
                case "Orphanage":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Increases population by " + buildingEffect.m_buildingEffect.Population + " when placed";
                    break;
                case "Propaganda House":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Increases Soviet reputation by 1 when placed";
                    break;
                case "Rebel Outpost":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Increases People reputation by 1 when placed";
                    break;
                case "Secret Library":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Increases or decreases both Soviet and People reputation toward 0 by 1 when placed";
                    break;
                case "Windmill":
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Increases food by 5 when placed, per adjacent farm or collective farm, per turn";
                    break;
                default:
                    currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "This building is outside the buildings set inside buildingInformationScript.";
                    break;
            }
        }
    }

    /*IEnumerator DrawBuildingsRoutine()
    {

    }*/
}
