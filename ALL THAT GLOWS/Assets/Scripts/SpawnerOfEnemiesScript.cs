using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfEnemiesScript : MonoBehaviour
{
    //.....................................Variables

    private float timer = 0;

        [Header ("Default Spawn Value (may remove)")]
    public float spawnRate = 5f;

        [Header ("Put models to spawn here")]
    public GameObject normalEnemyPrefab;
    public GameObject lightningEnemyPrefab;
    public GameObject fireEnemyPrefab;
    public GameObject iceEnemyPrefab;

    private int enemyType = 0;
    private int enemyAmount = 0;

        [Header("GameObjects with 'EnemyMovementPaths' go here")]
        [Tooltip("This one needs a GameObject with the 'EnemyMovementPath' Script attached.")]
    public GameObject[] enemyMovementPathsObjects;

        [Header ("These control waves of enemies.")]
        [Tooltip ("0=neutral; 1=lightning; 2=fire; 3=ice.")]
    public int[] waveEnemyElementToSpawn;
        [Tooltip ("Choose which path that you added above to use. Starts at 0.")]
    public int[] waveRouteToTake;
        [Tooltip("How many in this wave of this type with this route to spawn")]
    public int[] waveNumberOfEnemies;
        [Tooltip("Delay before spawning this wave")]
    public float[] waveDelayBeforeWave;
        [Tooltip("The delay between spawning inside a wave of enemies.")]
    public float delayInsideWave = .5f;

    private int indexEnemyElement = 0;
    private int indexRouteToTake = 0;
    private int indexNumberOfEnemies = 0;
    private int indexDelayBeforeWaves = 0;
    private int enemyAmountInWave;
    private bool waveIsSpawning = false;

    private void Start()
    {
        
    }

    void Update()
    {
        //track time
        timer = timer + Time.deltaTime;

        //....................................Spawn wave at delay time
        if (timer >= waveDelayBeforeWave[indexDelayBeforeWaves] && waveIsSpawning == false)
        {
            timer = 0;
            waveIsSpawning = true;
        }

        //....................................Spawn enemy inside wave at delay time
        if (timer >= delayInsideWave && waveIsSpawning == true)
        {
            timer = 0;
            enemyAmount++;
            enemyAmountInWave++;

            //....................................Spawn enemy of type
            if (waveEnemyElementToSpawn[indexEnemyElement] == 0)
            {
                GameObject newEnemy = Instantiate(normalEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);

            }
            if (waveEnemyElementToSpawn[indexEnemyElement] == 1)
            {
                Instantiate(lightningEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            if (waveEnemyElementToSpawn[indexEnemyElement] == 2)
            {
                Instantiate(fireEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            if (waveEnemyElementToSpawn[indexEnemyElement] == 3)
            {
                Instantiate(iceEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }


            //....................................Turn off wave when limit reached, go to next wave in arrays
            if (enemyAmountInWave >= waveNumberOfEnemies[indexNumberOfEnemies])
            {
                enemyAmountInWave = 0;

                waveIsSpawning = false;

                indexDelayBeforeWaves++;
                indexEnemyElement++;
                indexNumberOfEnemies++;
                indexRouteToTake++;
            }
        }
    }
}
