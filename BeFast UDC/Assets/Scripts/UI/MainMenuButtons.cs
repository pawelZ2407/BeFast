using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButtons : MonoBehaviour
{
    
    [SerializeField] GameObject upgradesScreen;
    [SerializeField] GameObject mainMenuScreen;
    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }
        


    public void GoToUpgrades()
    {
        mainMenuScreen.SetActive(false);
        upgradesScreen.SetActive(true);
    }
    public void Settings()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }

}
