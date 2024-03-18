using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject goalkeeper;
    public float distanceOff = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = goalkeeper.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.transform.position = transform.position;

        if (Controller.SelectedPlayer == null) return;

        Vector3 player = Controller.SelectedPlayer.transform.position;

        Vector3 dirtion = (Controller.SelectedPlayer.transform.position - transform.position).normalized;

        // rb.position = transform.position + dirtion*distanceOff;
        goalkeeper.transform.position = transform.position + dirtion * distanceOff;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
