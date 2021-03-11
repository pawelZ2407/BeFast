using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreenButtons : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject upgradesScreen;
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void GoToUpgrades()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
