using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentSceneIndex;
    GameState state;
    ResourceHolder Resources; 
    List<Effect> Effects;
    List<BuildingScriptable> Buildings;
    
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

    public void NewGame()
    {
        Resources = new ResourceHolder(100f, 100f, 0f, 0, 0);
        Effects = new List<Effect>();
        Buildings = new List<BuildingScriptable>();
        TurnSummary = new Effect();
        state = GameState.VillageHall;
        SwitchView();
    }
    public void ExitToMenu()
    {
        state = GameState.Menu;
        SwitchView();
    }
    public void LoadScene(int sceneIndex)
    {
        currentSceneIndex = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    private void AdvanceState()
    {
        state = (GameState) (((int)state + 1) % 3);
    }
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
    public void ApplyChoice(ChoiceScriptable choice)
    {
        Resources.TryApplyEffect(choice.m_choiceEffect);
        TurnSummary += choice.m_choiceEffect;
    }
    private void SwitchView()
    {
        switch (state)
        {
            case GameState.Menu:
                LoadScene(0);
                break;
            case GameState.VillageHall:
                LoadScene(1);
                break;
            case GameState.CityView:
                LoadScene(2);
                break;
            case GameState.ShowResources:
                //dothings
                break;
            default:
                Debug.Log("GameState hit an invalid value while trying to switch!!!!");
                throw new System.Exception("WTF");
        }

    }
}
