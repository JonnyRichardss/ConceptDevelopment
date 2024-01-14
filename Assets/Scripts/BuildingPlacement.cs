using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacement : MonoBehaviour
{
    public List<int> NewDrawPile = new List<int>();
    List<int> DrawPile = new List<int>();
    List<int> DiscardPile = new List<int>();

    public static BuildingPlacement instance;
    public bool isPlacingBuilding = false;
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
                currentBuilding = AllBuildings[1].GetChild(GetNextBuilding(AllBuildings[1]));
                break;
            case 2:
                currentBuilding = AllBuildings[2].GetChild(GetNextBuilding(AllBuildings[2]));
                break;
            case 3:
                currentBuilding = AllBuildings[3].GetChild(GetNextBuilding(AllBuildings[3]));
                break;
        }
        currentBuilding.gameObject.SetActive(true);
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
                print(building.GetChild(i).gameObject.activeSelf);
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
                print("hoi");
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
        //StartCoroutine(DrawBuildingsRoutine());
        cards = GameObject.Find("Cards").transform;
        cards.gameObject.SetActive(true);
        if (DrawPile.Count <= 3)
        {
            Refresh();
        }
        for (int i = 0; i < 3; i++)
        {
            int deckChoice = DrawPile[UnityEngine.Random.Range(0, DrawPile.Count)];
            DrawPile.RemoveAt(deckChoice);
            Transform DrawnBuilding = GetDrawnBuilding(deckChoice);
            Transform currentCard = cards.GetChild(i);
            currentCard.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            currentCard.GetComponentInChildren<Button>().onClick.AddListener(() => SetCurrentBuilding(deckChoice));
            BuildingScriptable buildingEffect = DrawnBuilding.GetComponent<BuildingController>().buildingEffect;
            currentCard.GetChild(0).GetComponent<TextMeshProUGUI>().text = buildingEffect.name;
            currentCard.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Population: " + buildingEffect.m_buildingEffect.Population.ToString();
            currentCard.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Food: " + buildingEffect.m_buildingEffect.Food.ToString();
            currentCard.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Suspicion: " + buildingEffect.m_buildingEffect.Suspicion.ToString();
            currentCard.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Rep People: " + buildingEffect.m_buildingEffect.RepPeople.ToString();
            currentCard.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Rep Soviet: " + buildingEffect.m_buildingEffect.RepSoviet.ToString();
        }
    }

    /*IEnumerator DrawBuildingsRoutine()
    {

    }*/
}
