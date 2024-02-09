using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Plane : MonoBehaviour
{

    public List<Vector2> points;
    public float newPositionThreshold = 0.2f;
    Vector2 lastPosition;
    LineRenderer lineRenderer;
    Vector2 currentPosition;
    Rigidbody2D rigidbody;
    public float speed = 1;
    public AnimationCurve landing;
    public float landingTimer;
    public float minValue = -5f;
    public float maxValue = 5f;
    public Sprite[] spriteArray;
    public Color inDangerZoneColor;
    public Color outDangerZoneColor;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        rigidbody = GetComponent<Rigidbody2D>();

        Random.Range(minValue, maxValue);

        Vector2 direction = new Vector3(Random.Range(minValue,maxValue), Random.Range(minValue, maxValue), 0f);
        transform.position = direction;

        transform.rotation = Quaternion.Euler (0f, 0f, Random.Range(minValue, maxValue));

        speed = Random.Range(1f, 3f);

        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = spriteArray[Random.Range(1, spriteArray.Length)];

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = outDangerZoneColor;


    }

    private void FixedUpdate()
    {
        currentPosition = transform.position;
        
        if(points.Count > 0 )
        {
            Vector2 direction = points[0] - currentPosition;
            float angle = Mathf.Atan2( direction.x, direction.y ) * Mathf.Rad2Deg;
            rigidbody.rotation = -angle;
        }
        rigidbody.MovePosition(rigidbody.position + (Vector2)transform.up * speed * Time.deltaTime);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            landingTimer += 0.5f * Time.deltaTime;
            float interpolation = landing.Evaluate(landingTimer);
            if (transform.localScale.z < 0.1f)
            {
                Destroy(gameObject);
            }
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, interpolation);
        }

        lineRenderer.SetPosition(0, transform.position);
        if ( points.Count > 0 )
        {
            if(Vector2.Distance(currentPosition, points[0]) < newPositionThreshold)
            {
                points.RemoveAt(0);

                for(int i = 0; i < lineRenderer.positionCount - 2; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
                }
                lineRenderer.positionCount--;
            }
        }
    }

    private void OnMouseDown()
    {
        points = new List<Vector2>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Vector2.Distance(lastPosition, newPosition) > newPositionThreshold)
        {
            points.Add(newPosition);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            lastPosition = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(" In danger zone: " + collision.gameObject);
        spriteRenderer.color = inDangerZoneColor;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(" Out danger zone: " + collision.gameObject);
        spriteRenderer.color = outDangerZoneColor;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float dist = Vector3.Distance(currentPosition, transform.position);
        if (dist < 1f)
        {
            spriteRenderer.color = inDangerZoneColor;
            Debug.Log(" distance: " + dist);
        }
    }
}
