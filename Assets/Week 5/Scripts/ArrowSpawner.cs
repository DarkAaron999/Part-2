using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    void Start()
    {
        
    }

    public void arrowSpawner()
    {
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
