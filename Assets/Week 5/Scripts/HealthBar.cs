using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private void Update()
    {
        slider.value = PlayerPrefs.GetFloat("health");
    }

    public void TakeDamage(float damage)
    {
        slider.value -= damage;
    }
}
