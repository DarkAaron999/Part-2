using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    //Referening: Used some of the same code in the in class assignemnts
    //Reference for slider
    public Slider slider;

    //Function for player take damage
    public void PlayerTakeDamage(float damage)
    {
        //Makes the slier sclae small based on the player health when it goes down  
        slider.value -= damage;
    }
    public void Heal(float heal)
    {
        //Makes the slider scale bigger based on the player health when it goes up
        slider.value += heal;
    }
}
