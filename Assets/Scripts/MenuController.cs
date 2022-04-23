using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public string MainMenu;
    public string StartLevel;
    public string Options;

    public void startGame()
    {
        SceneManager.LoadScene(StartLevel);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void options()
    {
        SceneManager.LoadScene(Options);
    }
    public void exitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
