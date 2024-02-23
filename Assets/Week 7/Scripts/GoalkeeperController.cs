using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject goalkeeper;

    // Start is called before the first frame update
    void Start()
    {
        rb = goalkeeper.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.transform.position = transform.position;
 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
