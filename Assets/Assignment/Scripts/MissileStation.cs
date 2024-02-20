using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissileStation : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float health;
    public float maxHealth = 5;
    public float money;
    public TextMeshProUGUI moneyText;
    bool isHurting = false;
    bool isHealing = false;
    bool isDead = false;
    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            money += 100;
            moneyText.text = money.ToString();
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        if (health < 5)
        {
            isHurting = true;
            animator.SetTrigger("Hurt");
        }

        if (health == 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
        else
        {
            isDead = false;
        }
    }

    public void Shooting()
    {
        isShooting = true;
        animator.SetTrigger("Shooting");
    }

    public void Heal(float heal)
    {
        if (money >= 200)
        {
            health += heal;
            health = Mathf.Clamp(health, 0, maxHealth);

            isHealing = true;
            animator.SetTrigger("Heal");

            money -= 200;
            moneyText.text = money.ToString();
        }
    }
}
