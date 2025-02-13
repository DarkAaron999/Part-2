using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    public Slider chargeSlider;
    float chargerValue;
    public float maxCharge = 1;
    Vector2 direction;
    public TextMeshProUGUI scoreUI;
   
    public static int score;
    public static FootballPlayer SelectedPlayer { get; private set; }   

    public static void SetSelectedPlayer(FootballPlayer player)
    {
        if (SelectedPlayer != null)
        {
            SelectedPlayer.Selected(false);
        }
        SelectedPlayer = player;
        SelectedPlayer.Selected(true);
    }

    public static void addSocre(int addToScore)
    {
        score = score + (addToScore);
        Debug.Log(score);
    }

    private void Start()
    {
        score = 0;
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            SelectedPlayer.Move(direction);
            direction = Vector2.zero;
            chargerValue = 0;
            chargeSlider.value = chargerValue;
        }

        scoreUI.text = score.ToString();
    }

    private void Update()
    {
        if (SelectedPlayer == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            chargerValue = 0;
            direction = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            chargerValue += Time.deltaTime;
            chargerValue = Mathf.Clamp(chargerValue, 0, maxCharge);
            chargeSlider.value = chargerValue;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)SelectedPlayer.transform.position).normalized * chargerValue;
        }
    }
}
