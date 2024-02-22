using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    //Reference for the rigidbody
    Rigidbody2D rb;
    //Reference for the animator
    Animator animator;
    //Reference for the health
    public float health;
    //Reference for the max health
    public float maxHealth = 2;
    //Reference for is hurting
    bool isHurting = false;
    //Reference for is dead
    bool isDead = false;
    //Reference for is closer
    bool isCloser = false;
    //Reference for the traget transform
    public Transform traget;
    //Reference for the player gameobject
    public GameObject player;
    //Reference for the enemy position
    Vector2 enemyPosition;
    //Reference for the player position
    Vector2 playerPosition;
    //Reference for the missile station
    public MissileStation missileStation;
    //Reference for the animation curve 
    public AnimationCurve enemyTraveling;
    //Reference for the travel timer
    public float travelTimer;
    //Reference for the destory timer
    public float destroyTimer;
    //Reference for the score
    public Score score;
    // Start is called before the first frame update
    void Start()
    {
        //Get the component rigidbody 
        rb = GetComponent<Rigidbody2D>();
        //Get the component animator
        animator = GetComponent<Animator>();

        //Health is to max health
        health = maxHealth;

        //Score is gameobject find oject with tag playerscore get component score  
        score = GameObject.FindGameObjectWithTag("PlayerScore").GetComponent<Score>();

        //Missile station is gameobject find object with tag missileStation get component missileStation
        missileStation = GameObject.FindGameObjectWithTag("MissileStation").GetComponent<MissileStation>();

    }
    //Function that runs once pre frame
    private void FixedUpdate()
    {
        //Enemy posisiton is transfrom position
        enemyPosition = transform.position;
        //Player position is missileStation transform position
        playerPosition = missileStation.transform.position;

        //Add time to destory timer
        destroyTimer +=  Time.deltaTime;
        //interpolation is enemy traveling to destory timer
        float interpolation = enemyTraveling.Evaluate(destroyTimer);
        //Tranform position is vector 3 using lerp to enemy position, player position, and interpolation divided travel timer
        transform.position = Vector3.Lerp(enemyPosition, playerPosition, interpolation / travelTimer * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        //If dead return to isdead
        if (isDead) return;
    }
    //Function for enemy take damage
    public void EnemyTakeDamage(float damage)
    {
        //Minus health when damaged
        health -= damage;
        //Using maths for helth
        health = Mathf.Clamp(health, 0, maxHealth);

        //If health is less than 2 run this
        if (health < 2)
        {
            //Play the hurting animation
            isHurting = true;
            animator.SetTrigger("Hurt");
        }
        
        //If health is equal to 0 run this
        if (health == 0)
        {
            //Play the dead animation
            isDead = true;
            animator.SetTrigger("Death");

            //Add 200 money
            missileStation.money += 200;
            //Update money text to money
            missileStation.moneyText.text = missileStation.money.ToString();

            //Add 1 score to score
            score.addScore(1);

            //Destroy this gameobject
            Destroy(gameObject);
        }
        //If health is not at 0 run this instead 
        else
        {
            //Do not play dead animation
            isDead = false;
        }
    }
    //Function for on trigger stay
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Find the distance between enemy position and collision transform position
        float dist = Vector3.Distance(enemyPosition, collision.transform.position);

        //If collision compare tag missile station run this
        if (collision.CompareTag("MissileStation"))
        {
            //print distance to the console
            Debug.Log(" distance: " + dist);

            //If distance is less than 0.7 run this
            if (dist < 0.7f)
            {
                //Destory this gameobject
                Destroy(gameObject);
                //On collision gameobject send message to player take damage 1
                collision.gameObject.SendMessage("PlayerTakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
