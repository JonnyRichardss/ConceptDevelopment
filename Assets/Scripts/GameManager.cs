using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentSceneIndex;
    GameState state;
    public int TurnNumber;
    ResourceHolder Resources; 

    public List<Effect> Effects;
    public List<BuildingScriptable> Buildings;

    

    GameState state;


    public Text summaryText;


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
        BuildingManager.Reset();
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
            DisplaySummary(); //display summary here
        }
        TurnNumber++;
        Debug.Log("Resource Showing not Implemented yet");

        //AdvanceState();
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

    public void DrawEvents()
    {
        for (int RemainingEvents=4;RemainingEvents == 0; RemainingEvents--)
        {
            EventManager.DrawEvent(Resources);
            //do more here
            //probably ApplyChoice()
        }
        AdvanceState();
        Debug.Log("Event Drawing not implemented yet");
    }

    public void DrawBuildings()
    {
        List<BuildingScriptable> buildings = BuildingManager.DrawBuildings(Resources);
        //display all
        //place one
        Debug.Log("Building placing not implemented");
        AdvanceState();
    }
    #endregion
    #region ApplyEffects

    private void ApplyAllEffects()
    {
        List<Effect> effectsTemp = new List<Effect>();
        foreach(Effect effect in Effects)
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
        foreach(BuildingScriptable b in Buildings)
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
        SwitchView();
    }
    public void SwitchView()
    {
        switch (state)
        {
            case GameState.Menu:
                LoadScene(0);
                break;
            case GameState.DrawEvents:
                LoadScene(1);
                DrawEvents();
                break;
            case GameState.DrawBuildings:
                LoadScene(2);
                DrawBuildings();
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
