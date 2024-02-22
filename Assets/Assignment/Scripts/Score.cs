using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //Reference for player score
    public int playerScore;
    //Reference for player highscore
    public int playerHighScore;
    //Reference for score text
    public TextMeshProUGUI score;
    //Reference for highscore
    public TextMeshProUGUI highScore;
    
    //Function that runs at start time
    void Start()
    {
        //Saves the players score in game and on object
        playerHighScore = PlayerPrefs.GetInt("Highscore");
        highScore.text = PlayerPrefs.GetInt("Highscore").ToString();
    }
    void Update()
    {
        //When player gets 10 score it will load victroy scene
        if (playerScore == 10)
        {
            SceneManager.LoadScene("Victroy");
        }
    }

    //Function to add score
    public void addScore(int scoreToAdd)
    {
        //The playerscoe is equal to the playerscore then add number
        playerScore = playerScore + scoreToAdd;
        //Player highscore is equal to player score
        playerHighScore = playerScore;
        
        //Score text is equal to the player score
        score.text = playerScore.ToString();
        //high score text is equal to the player high score 
        highScore.text = playerHighScore.ToString();

        //Saves the high scrore text
        PlayerPrefs.SetInt("Highscore", playerHighScore);
    }
}
