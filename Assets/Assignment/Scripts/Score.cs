using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int playerScore;
    public int playerHighScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    void Start()
    {
        playerHighScore = PlayerPrefs.GetInt("Highscore");
        highScore.text = PlayerPrefs.GetInt("Highscore").ToString();
    }
    void Update()
    {
        if (playerScore == 10)
        {
            SceneManager.LoadScene("Victroy");
        }
    }
    public void addScore()
    {
        playerScore = playerScore + 1;
        playerHighScore = playerScore;
        
        score.text = playerScore.ToString();
        highScore.text = playerHighScore.ToString();

        PlayerPrefs.SetInt("Highscore", playerHighScore);
    }
}
