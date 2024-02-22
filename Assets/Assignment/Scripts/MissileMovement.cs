using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MissileMovement : MonoBehaviour
{
    //Reference for vector 2 point
    public List<Vector2> points;
    //Reference for new position
    public float newPositionThreshold = 0.2f;
    //Reference for vector 2 last position
    Vector2 lastPosition;
    //Reference for line renderer
    LineRenderer lineRenderer;
    //Reference for rigidbody
    Rigidbody2D rb;
    //Reference for speed
    public float speed = 2;
    //Reference for exploding animation
    bool isExploding = false;
    //Reference for vector 2 missile position
    Vector2 missilePosition;
    // Start is called before the first frame update
    void Start()
    {
        //Get the component for line renderer
        lineRenderer = GetComponent<LineRenderer>();
        //Get the position count
        lineRenderer.positionCount = 1;
        //Get the line renderer position
        lineRenderer.SetPosition(0, transform.position);

        //Get the rigidbody component
        rb = GetComponent<Rigidbody2D>(); 
    }

    //Function for fixed update run once pre frame
    private void FixedUpdate()
    {
        //missile position is equal to transfrom position 
        missilePosition = transform.position;

        //If point cout is greater than 0 run this
        if (points.Count > 0)
        {
            //Vector2 direction is equal to point minuses the missile position
            Vector2 direction = points[0] - missilePosition;
            //Using maths for x, and y direction
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            //The rigidbody rotation misuses the angle
            rb.rotation = -angle;
        }
        //Rigidbody move position to the rigidbody position vector 2 transfrom up based on speed
        rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.deltaTime);
    }

    private void Update()
    {
        //If exploding return to exploding
        if (isExploding) return;

        //Set the position of the line renderer
        lineRenderer.SetPosition(0, transform.position);
        //If point coun to greater than 0 run this
        if (points.Count > 0)
        {
            //The missile position distance and point is less than new position threshold run this
            if (Vector2.Distance(missilePosition, points[0]) < newPositionThreshold)
            {
                //Remove points
                points.RemoveAt(0);

                //For loop for the line renderer position count
                for (int i = 0; i < lineRenderer.positionCount - 2; i++)
                {
                    //Set and get the position for the line renderer 
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
                }
                //line rebderer and position count --
                lineRenderer.positionCount--;
            }
        }
    }

    //Function for on mouse down
    private void OnMouseDown()
    {
        //Add new points
        points = new List<Vector2>();
        //Line renderer position count equal to 1
        lineRenderer.positionCount = 1;
        //set the position for line renderer 
        lineRenderer.SetPosition(0, transform.position);
    }
    //Function for on mouse drag
    private void OnMouseDrag()
    {
        //New position equal to camera in the world point of veiw getting the mouse position
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //If the las position distance to greater than new position threshold run this
        if (Vector2.Distance(lastPosition, newPosition) > newPositionThreshold)
        {
            //Add new point to the new position 
            points.Add(newPosition);
            //Add position count to line renderer
            lineRenderer.positionCount++;
            //Set line renderer position to line renderer position count misus 1 to new position
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            //last position equal to new position
            lastPosition = newPosition;
        }
    }

    //Function for on trigger stay
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Find the distance between the missile and collision transform position
        float dist = Vector3.Distance(missilePosition, collision.transform.position);

        //If collision tag enemy run this
        if (collision.CompareTag("Enemy"))
        {
            //Prints the distance in the console
            Debug.Log(" distance: " + dist);

            //If distance is less than 0.7 run this
            if (dist < 0.7f)
            {
                //Play exploding animation
                isExploding = true;

                //Destroy this gameobject
                Destroy(gameObject);
                //When collision with gameobject send message to enemy take damage 1
                collision.gameObject.SendMessage("EnemyTakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
