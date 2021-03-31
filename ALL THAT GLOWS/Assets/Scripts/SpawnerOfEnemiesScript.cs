using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfEnemiesScript : MonoBehaviour
{
    //.....................................Variables
    private float timer = 0;
    public float spawnRate = 5f;
    public GameObject normalEnemyPrefab;
    public GameObject lightningEnemyPrefab;
    public GameObject fireEnemyPrefab;
    public GameObject iceEnemyPrefab;
    private int enemyType = 0;
    
    void Update()
    {
        //track time
        timer = timer + Time.deltaTime;

        //....................................Spawn at time
        if (timer >= spawnRate)
        {
            timer = 0;

            if (enemyType == 3)
            {
                Instantiate(iceEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }

            if (enemyType == 2)
            {
                Instantiate(fireEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }

            if (enemyType == 1)
            {
                Instantiate(lightningEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }

            if (enemyType == 0)
            {
                Instantiate(normalEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }

            enemyType += 1;

            if (enemyType == 4)
            {
                enemyType = 0;
            }
        }
    }
}
