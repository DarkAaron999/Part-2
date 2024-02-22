using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float health;
    public float maxHealth = 2;
    bool isHurting = false;
    bool isDead = false;
    bool isCloser = false;
    public float speed = 1f;
    public Transform traget;
    Vector2 enemyPosition;
    Vector2 playerPosition;
    public MissileStation missileStation;
    public AnimationCurve enemyTraveling;
    public float travelTimer;
    public float destroyTimer;
    public Score score;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;
    }

    private void FixedUpdate()
    {
        enemyPosition = transform.position;

        playerPosition = traget.position;

        destroyTimer +=  Time.deltaTime;
        float interpolation = enemyTraveling.Evaluate(destroyTimer);
        transform.position = Vector3.Lerp(enemyPosition, playerPosition, interpolation / travelTimer * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
    }

    public void EnemyTakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health < 2)
        {
            isHurting = true;
            animator.SetTrigger("Hurt");
        }
        
        if (health == 0)
        {
            isDead = true;
            animator.SetTrigger("Death");

            missileStation.money += 200;
            missileStation.moneyText.text = missileStation.money.ToString();

            score.addScore();

            Destroy(gameObject);

        }
        else
        {
            isDead = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float dist = Vector3.Distance(enemyPosition, collision.transform.position);

        if (collision.CompareTag("MissileStation"))
        {
            Debug.Log(" distance: " + dist);

            if (dist < 0.7f)
            {
                Destroy(gameObject);
                collision.gameObject.SendMessage("PlayerTakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
