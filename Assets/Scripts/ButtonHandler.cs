using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class ButtonHandler : MonoBehaviour
{
    //this script is just used so that the buttons can access gamemanager while its in dontdestroyonload
     public void LoadScene(int state)
    {
        switch (state)
        {
            case 0:
                GameManager.instance.state = GameState.Menu;
                break;
            case 1:
                GameManager.instance.state = GameState.DrawEvents;
                break;
            case 2:
                GameManager.instance.state = GameState.DrawBuildings;
                break;
            case 3:
                GameManager.instance.state = GameState.ShowResources;
                break;
            case 4:
                GameManager.instance.state = GameState.EndGame;
                break;
            default:
                break;
        }
        GameManager.instance.SwitchScene();
        //indices in buildsettings
        /*
         * 0 - Main Menu
         * 1 - VillageHall
         * 2 - CityView
         * 3 - Options
         *
         */
    }
    public void NewGame()
    {
        GameManager.instance.NewGame();
    }
    public void ExitToMenu()
    {
        GameManager.instance.ExitToMenu();
    }
    public void EventsButton()
    {
        GameManager.instance.AdvanceState();
    }

    public void NextTurn()
    {
        GameManager.instance.NewTurn();
    }
}
