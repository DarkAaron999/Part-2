using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int playerScore;

    public void addScore()
    {
        playerScore = playerScore + 1;
    }
}
