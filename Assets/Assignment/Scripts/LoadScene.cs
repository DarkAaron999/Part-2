using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadSceneGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
