using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentSceneIndex;
    GameState state;
    public int TurnNumber;
    ResourceHolder Resources;
    public List<Effect> Effects;
    public List<BuildingScriptable> Buildings;

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
        TurnSummary = new Effect();
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
        if(TurnNumber != 0)
        {
            AddBuildings(); //add building effects to effects list
            ApplyAllEffects(); //apply
            //display summary here
        }
        TurnNumber++;
        Debug.Log("Resource Showing not Implemented yet");

        AdvanceState();
        //TryNextEvent();
        //turn logging goes in here
    }
    private void DisplaySummary()
    {
        //Canvas with a Text UI element named "SummaryText"
        if (summaryText != null)
        {
            // Customize this message based on the summary you want to display
            string summaryMessage = $"Turn {TurnNumber} summary:\nBuildings added, effects applied.";

            // Set the text of the UI element
            summaryText.text = summaryMessage;

            // Activate the Canvas or show the pop-up
            summaryText.gameObject.SetActive(true);

            //Coroutine or a timer to hide the pop-up after some time
            StartCoroutine(HideSummaryAfterDelay(3f)); // Hide after 3 seconds (adjust as needed)
        }
    }

    private IEnumerator HideSummaryAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Canvas with a Text UI element named "SummaryText"
        if (summaryText != null)
        {
            //Hide the pop-up
            summaryText.gameObject.SetActive(false);
        }
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
        AdvanceState();
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
        yield return new WaitForSeconds(1f);
        //display all
        //place one
        Debug.Log("Building placing is implemented");
        AdvanceState();
    }
    #endregion
    #region ApplyEffects
    private void ApplyAllEffects()
    {
        List<Effect> effectsTemp = new List<Effect>();
        foreach (Effect effect in Effects)
        {
            if (Resources.TryApplyEffect(effect))
            {
                effectsTemp.Add(effect);
            }
            TurnSummary += effect;
        }
        Effects = effectsTemp;
    }
    public void AddBuildings()
    {
        foreach (BuildingScriptable b in Buildings)
        {
            Effects.Add(b.m_buildingEffect);
        }
    }
    public void ApplyChoice(ChoiceScriptable choice)
    {
        Resources.TryApplyEffect(choice.m_choiceEffect);
        TurnSummary += choice.m_choiceEffect;
    }
    #endregion
    #region GameState
    public void LoadScene(int sceneIndex)
    {
        currentSceneIndex = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
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
                print("hi");
                StartCoroutine(DrawEvents());
                break;
            case GameState.DrawBuildings:
                LoadScene(2);
                while (SceneManager.GetActiveScene().name != "3. CityView")
                {

                    yield return new WaitForFixedUpdate();
                }
                print("hi");
                StartCoroutine(DrawBuildings());
                break;
            case GameState.ShowResources:
                LoadScene(1);
                NewTurn();
                break;
            default:
                Debug.Log("GameState hit an invalid value while trying to switch!!!!");
                throw new System.Exception("WTF");
        }

    }
    #endregion
}
