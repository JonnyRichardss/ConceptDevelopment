using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentSceneIndex;
    public GameState state;
    public int TurnNumber;
    ResourceHolder Resources;
    public List<Effect> Effects;
    public List<BuildingScriptable> Buildings;
    public bool hasEvents = false;
    public bool hasBuildings = false;
    public enum EndReason { allGone, allStarved, suspicion, noPeopleRep, noSovietRep }
    public EndReason endReason;

    Effect StartValues;
    Effect TurnSummary;

    //awake is caleld even if the object is disabled apaprently
    private void Awake()
    {
        //makes sure gamemanager only exists once
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        currentSceneIndex = 4;
        instance = this;
        DontDestroyOnLoad(gameObject);
        Resources = new ResourceHolder();
        Effects = new List<Effect>();
        state = GameState.Menu;
    }
    #region StartEnd
    public void NewGame()
    {
        Resources = new ResourceHolder(100f, 100f, 0f, 0, 0);
        Effects = new List<Effect>();
        Buildings = new List<BuildingScriptable>();
        StartValues = new Effect(0, 90, 0, 0, 0, 0);
        TurnSummary = StartValues;
        state = GameState.ShowResources;
        if (BuildingPlacement.instance != null)
        {
            BuildingPlacement.instance.Refresh();
        }
        NewTurn();
    }
    public void ExitToMenu()
    {
        state = GameState.Menu;
        SwitchView();
    }
    #endregion
    
    #region GameLoop
    public void NewTurn()
    {
        if (TurnNumber != 0)
        {
            //AddBuildings(); //add building effects to effects list
            ApplyAllEffects(); //apply
            //display summary here
        }
        if (CheckEndGame())
        {
            return;
        }
        hasEvents = false;
        hasBuildings = false;
        TurnNumber++;
        Debug.Log("Resource Showing not Implemented yet");

        state = GameState.ShowResources;
        StartCoroutine(SwitchView());
        //TryNextEvent();
        //turn logging goes in here
    }

    bool CheckEndGame()
    {
        bool end = false;
        if (Resources.Population <= 0) 
        {
            endReason = EndReason.allGone;
            end = true;
        }
        else if (Resources.Food < -50)
        {
            endReason = EndReason.allStarved;
            end = true;
        }
        else if (Resources.Suspicion == 10)
        {
            endReason = EndReason.suspicion;
            end = true;
        }
        else if (Resources.RepSoviet >= 10 || Resources.RepPeople <= -10)
        {
            endReason = EndReason.noPeopleRep;
            end = true;
        }
        else if (Resources.RepSoviet <= -10 || Resources.RepPeople >= 10)
        {
            endReason = EndReason.noSovietRep;
            end = true;
        }
        if (end)
        {
            print("die");
            state = GameState.EndGame;
            StartCoroutine(SwitchView());
        }
        return end;
    }

    public IEnumerator DrawEvents()
    {
        bool isDrawn = false;
        TestUIController.instance.CallDrawEvents(4, (bool getDrawn) =>
        {
            isDrawn = getDrawn;
        });
        while (!isDrawn)
        {
            yield return new WaitForFixedUpdate();
        }
        //AdvanceState();
        hasEvents |= true;
        state = GameState.ShowResources;
        StartCoroutine(SwitchView());
        Debug.Log("Event Drawing is implemented");
    }
    public IEnumerator DrawBuildings()
    {
        bool isPlaced = false;
        BuildingPlacement.instance.DrawBuildings((bool getPlaced) =>
        {
            isPlaced = getPlaced;
        });
        while (!isPlaced)
        {
            yield return new WaitForFixedUpdate();
        }
        print("building placed!");
        hasBuildings = true;
        //yield return new WaitForSeconds(1f);
        //display all
        //place one
        Debug.Log("Building placing is implemented");
        //AdvanceState();
        //state = GameState.ShowResources;
        //StartCoroutine(SwitchView());
    }
    #endregion
    #region ApplyEffects
    private void ApplyAllEffects()
    {
        List<Effect> effectsTemp = new List<Effect>();
        Resources.TryApplyEffect(new Effect(0, StartValues.Food - Resources.Population, 0, 0, 0, 0));
        //Resources.TryApplyEffect(TurnSummary);
        foreach (Effect effect in Effects)
        {
            if (Resources.TryApplyEffect(effect))
            {
                effectsTemp.Add(effect);
            }
            else
            {
                TurnSummary -= effect;
            }
            //TurnSummary += effect;
        }
        Effects = effectsTemp;
    }
    public void AddBuildings()
    {
        foreach (BuildingScriptable b in Buildings)
        {
            ApplyChoiceChange(b.m_buildingEffect);
            Effects.Add(b.m_buildingEffect);
        }
    }
    public void ApplyChoice(ChoiceScriptable choice)
    {
        Resources.TryApplyEffect(choice.m_choiceEffect);
        //TurnSummary += choice.m_choiceEffect;
    }

    public void ApplyChoiceChange(Effect choice)
    {
        TurnSummary += choice;
    }

    public void ShowEffects()
    {

        TextMeshProUGUI resources = GameObject.Find("Resources").GetComponent<TextMeshProUGUI>();
        resources.text = "Population: " + Resources.Population + " + " + TurnSummary.Population + "\n"
            + "Food: " + Resources.Food + " + " + TurnSummary.Food + " - " + Resources.Population + "\n"
            + "Suspicion: " + Resources.Suspicion + " + " + TurnSummary.Suspicion + "\n"
            + "People Rep: " + Resources.RepPeople + " + " + TurnSummary.RepPeople + "\n"
            + "Soviet Rep: " + Resources.RepSoviet + " + " + TurnSummary.RepSoviet;
    }
    #endregion
    #region GameState
    public void LoadScene(int sceneIndex)
    {
        currentSceneIndex = sceneIndex;
        //StartCoroutine(SwitchView());
        SceneManager.LoadScene(sceneIndex);
    }

    public void SwitchScene()
    {
        StartCoroutine(SwitchView());
    }
    public void AdvanceState()
    {
        state = (GameState)(((int)state + 1) % 3);
        StartCoroutine(SwitchView());
    }
    public IEnumerator SwitchView()
    {
        switch (state)
        {
            case GameState.Menu:
                LoadScene(0);
                break;
            case GameState.DrawEvents:
                LoadScene(1);
                while (SceneManager.GetActiveScene().name != "JoelsEventScene")
                {
                    print(SceneManager.GetActiveScene().name);
                    yield return new WaitForFixedUpdate();
                }
                print(SceneManager.GetActiveScene().name);
                StartCoroutine(DrawEvents());
                break;
            case GameState.DrawBuildings:
                LoadScene(2);
                while (SceneManager.GetActiveScene().name != "3. CityView")
                {

                    yield return new WaitForFixedUpdate();
                }
                StartCoroutine(DrawBuildings());
                break;
            case GameState.ShowResources:
                LoadScene(3);
                yield return new WaitForFixedUpdate();
                while (SceneManager.GetActiveScene().name != "2. VillageHall")
                {
                    print(SceneManager.GetActiveScene().name);
                    yield return new WaitForFixedUpdate();
                }
                ShowEffects();
                //NewTurn();
                break;
            case GameState.EndGame:
                print("mip");
                LoadScene(5);
                while (SceneManager.GetActiveScene().name != "EndScene")
                {
                    print(SceneManager.GetActiveScene().name);
                    yield return new WaitForFixedUpdate();
                }
                break;
            default:
                Debug.Log("GameState hit an invalid value while trying to switch!!!!");
                throw new System.Exception("WTF");
        }

    }
    #endregion
}
