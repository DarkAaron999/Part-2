using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FootballPlayer : MonoBehaviour
{
    bool clickingOnSelf = false;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float speed = 100;
    public Color selectedColor;
    public Color unSelectedColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Selected(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Controller.SetSelectedPlayer(this);
    }

    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
             sr.color = selectedColor;
        }
        else
        {
            sr.color = unSelectedColor;
        }
    }

    public void Move(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
}
