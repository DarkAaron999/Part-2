using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab;
    public float timerValue = 0f;
    public float timerTraget = 5f;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        timerValue += 1f * Time.deltaTime;
        if (timerValue > timerTraget)
        {
            Instantiate(planePrefab);
            timerValue = 0f;
        }
    }
}
