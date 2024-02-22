using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissileStation : MonoBehaviour
{
    //Referening: Used some of the same code in the in class assignemnts
    //Reference for rigidbody
    Rigidbody2D rb;
    //Reference for annimator
    Animator animator;
    //Reference for health
    public float health;
    //Reference for max health
    public float maxHealth = 5;
    //Reference for money
    public float money;
    //Reference for money text
    public TextMeshProUGUI moneyText;
    //Reference for hurting animation
    bool isHurting = false;
    //Reference for healing animation
    bool isHealing = false;
    //Reference for the dead animation
    bool isDead = false;
    //Reference for the shooting animation
    bool isShooting = false;
    //Reference for missile prefab gameobject
    public GameObject missilePrefab;
    //Reference for the missile spawner prefab
    public Transform missileSpawner;
    
    //Function runs at start time
    void Start()
    {
        //Gets the rigidboyd component
        rb = GetComponent<Rigidbody2D>();
        //Gets the animator component
        animator = GetComponent<Animator>();

        //health is equal to max health
        health = maxHealth;
        
        //money at start is 500
        money = 500;
        //money text is equal to money
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //If dead return to isDead
        if (isDead) return;

        //For testing
        //If left mouse button click run this
        if (Input.GetMouseButtonDown(1))
        {
            //Add 100 to money
            money += 100;
            //Update money text to money
            moneyText.text = money.ToString();
        }
    }

    //Function for player taking damage
    public void PlayerTakeDamage(float damage)
    {
        //Misus health when damaged
        health -= damage;
        //Use maths for health
        health = Mathf.Clamp(health, 0, maxHealth);

        //If health is less than 5 run this
        if (health < 5)
        {
            //Play the hurting animation
            isHurting = true;
            animator.SetTrigger("Hurt");
        }

        //If health is equal to 0 run this
        if (health == 0)
        {
            //play the Dead animation and do not play healing, shooting, and hurting animation
            isDead = true;
            isHealing = false;
            isShooting = false;
            isHurting = false;
            animator.SetTrigger("Death");

            //Destroy this gameobject
            Destroy(gameObject);
            //Load the game over scene
            SceneManager.LoadScene("GameOver");
        }
        //Else the player is not at 0 health
        else
        {
            //Do not play isDead animation
            isDead = false;
        }
    }

    //Function for shooting
    public void Shooting()
    {
        //If the player is at 100 money run this
        if (money >= 100)
        {
            //Play the shooting animation
            isShooting = true;
            animator.SetTrigger("Shooting");

            //Spawn a missile
            Instantiate(missilePrefab, missileSpawner.position, missileSpawner.rotation);

            //Minus 100 money
            money -= 100;
            //Update the money text to money
            moneyText.text = money.ToString();
        }
    }

    //Function for heal
    public void Heal(float heal)
    {
        //If money is at or greater than 200 run this
        if (money >= 200)
        {
            //Add health to the player
            health += heal;
            //Using math for the health
            health = Mathf.Clamp(health, 0, maxHealth);

            //Play the healing animation
            isHealing = true;
            animator.SetTrigger("Heal");

            //Minus 200 money
            money -= 200;
            //Update money text to money
            moneyText.text = money.ToString();
        }
    }
}
