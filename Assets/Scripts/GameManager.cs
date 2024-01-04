using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    ResourceHolder Resources; 
    List<Effect> Effects;
    List<BuildingScriptable> Buildings;
<<<<<<< Updated upstream
    GameState state;
=======

    public Text summaryText;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
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
>>>>>>> Stashed changes
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
