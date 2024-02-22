 using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //Function for load scene how to play
    public void LoadSceneHowToPlay()
    {
        //Load scene how to play
        SceneManager.LoadScene("HowToPlay");
    }
    //Function for load scene menu
    public void LoadSceneMenu()
    {
        //Load scene menu
        SceneManager.LoadScene("Menu");
    }
    //Function for load scene game
    public void LoadSceneGame()
    {
        //Load scene game
        SceneManager.LoadScene("Game");
    }
    //Function for quiting the game
    public void Quit()
    {
        //Quit the game
        Application.Quit();
    }
}
