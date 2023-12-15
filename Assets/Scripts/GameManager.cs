using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentSceneIndex;
    GameState state;
    public int TurnNumber;
    ResourceHolder Resources; 
    List<Effect> Effects;
    List<BuildingScriptable> Buildings;
    
    Effect TurnSummary;
    
    //awake is caleld even if the object is disabled apaprently
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
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
        SwitchView();
        //BuildingManager.Reset();
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

        //AdvanceState();
        //TryNextEvent();
        //turn logging goes in here
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
        //List<BuildingScriptable> buildings = BuildingManager.DrawBuildings(Resources);
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
