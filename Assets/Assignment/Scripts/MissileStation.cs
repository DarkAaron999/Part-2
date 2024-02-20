using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject missilePrefab;
    public Transform missileSpawner;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;
        money = 250;
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(1))
        {
            money += 100;
            moneyText.text = money.ToString();
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health < 5)
        {
            isHurting = true;
            animator.SetTrigger("Hurt");
        }

        if (health == 0)
        {
            isDead = true;
            animator.SetTrigger("Death");

            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            isDead = false;
        }
    }

    public void Shooting()
    {
        if (money >= 100)
        {
            isShooting = true;
            animator.SetTrigger("Shooting");

            Instantiate(missilePrefab, missileSpawner.position, missileSpawner.rotation);

            money -= 100;
            moneyText.text = money.ToString();
        }
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
