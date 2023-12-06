using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class ButtonHandler : MonoBehaviour
{
    //this script is just used so that the buttons can access gamemanager while its in dontdestroyonload
     public void LoadScene(int sceneIndex)
    {
        GameManager.instance.LoadScene(sceneIndex);
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
}
