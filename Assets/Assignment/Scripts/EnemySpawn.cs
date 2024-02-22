using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //Reference for enemy prefab
    public GameObject enemyPrefab;
    //Reference for enemy spawn point
    public Transform enemySpawnPoint;
    //Reference for spawn time
    public float spawningTime = 0f;
    //Reference for spawn timer
    public float spawningTimer = 5f;

    // Update is called once per frame
    void Update()
    {
        //Add 1f to spawn time
        spawningTime += 1f *Time.deltaTime;

        //If spawn tine is greater than spawn timer run this  
        if (spawningTime > spawningTimer)
        {
            //Spawn enemy prefab at transfrom position and rotation
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            //Spawn time is equal to a random number between 0f and 5f
            spawningTime = Random.Range(0f, 5f);
        }
    }
}
