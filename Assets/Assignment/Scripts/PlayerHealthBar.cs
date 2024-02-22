using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public void PlayerTakeDamage(float damage)
    {
        slider.value -= damage;
    }
    public void Heal(float heal)
    {
        slider.value += heal;
    }
}
