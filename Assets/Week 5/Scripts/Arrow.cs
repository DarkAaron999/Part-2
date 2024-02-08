using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 speed = new Vector2 (10, 0);
    Rigidbody2D rb;
    //public float destroyTimer = 5f;
    public float destoryTimerValue = 5f;
    public float destroyTimerTraget = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed *  Time.deltaTime);
    }

    void Update()
    {
        destroyTimerTraget -= 1f * Time.deltaTime;

        if (destoryTimerValue < destroyTimerTraget)
        {
            destoryTimerValue = 5f;
            Destroy(gameObject);
        }

        //This code the same thing from the code above just it is just shorter I only choosed the one above because Time.deltaTime
        //Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
