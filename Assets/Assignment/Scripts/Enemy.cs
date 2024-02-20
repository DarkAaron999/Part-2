using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float health;
    public float maxHealth = 2;
    bool isHurting = false;
    bool isDead = false;
    public float speed = 1f;
    public Transform traget;
    Vector2 enemyPosition;
    public MissileStation missileStation;
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
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, traget.position, speed * Time.deltaTime);
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
