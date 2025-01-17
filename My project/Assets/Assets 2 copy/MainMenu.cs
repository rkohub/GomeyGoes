using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject MainMenuUI;

     void Start()
    {
        settingUI.SetActive(false);
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Settings()
    {
        settingUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
