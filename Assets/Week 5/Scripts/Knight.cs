using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    public float speed = 3;
    Rigidbody2D rb;
    Animator animator;
    bool clickingOnSelf = false;
    public float health;
    public float maxHealth = 5;
    bool isDead = false;
    bool isAttacking = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        movement = destination - (Vector2)transform.position;
        if (movement.magnitude < 0.1f)
        {
            movement = Vector2.zero;
        }
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }
    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0) && !clickingOnSelf && !EventSystem.current.IsPointerOverGameObject())
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        animator.SetFloat("Movement", movement.magnitude);

        if (Input.GetMouseButtonDown(1))
        {
            SendMessage("Attack");
        }
    }
    private void OnMouseDown()
    {
        if (isDead) return;

        clickingOnSelf = true;
        SendMessage("TakeDamage", 1);  
    }
    private void OnMouseUp()
    {
        clickingOnSelf = false;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
        {
            //dead
            isDead = true;
            isAttacking = false;
            animator.SetTrigger("Death");
            //To stop the repeating of taking damage animation by turning off the collider
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            isDead = false;
            animator.SetTrigger("TakeDamage");
            //To turn the collider back on so the taking damage animation works
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }
}
