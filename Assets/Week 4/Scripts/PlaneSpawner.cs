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

    void Update()
    {
        timerValue += 1f * Time.deltaTime;
        if (timerValue > timerTraget)
        {
            Instantiate(planePrefab);
            timerValue = Random.Range(1f, 5f);
        }
    }
}
