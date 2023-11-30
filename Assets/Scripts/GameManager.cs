using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ResourceHolder Resources; 
    List<Effect> Effects;
    List<BuildingScriptable> Buildings;
    GameState state;
    Effect TurnSummary;
    // Start is called before the first frame update
    void Start()
    {
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
        state = GameState.ThroneRoom;
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
                //dothings
                break;
            case GameState.ThroneRoom:
                //dothings
                break;
            case GameState.CityView:
                //dothings
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
