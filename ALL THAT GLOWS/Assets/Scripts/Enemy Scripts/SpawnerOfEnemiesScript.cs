using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerOfEnemiesScript : MonoBehaviour
{
    //.....................................Variables

    private float timer = 0;

        [Header("Put models to spawn here")]
    public GameObject normalEnemyPrefab;
    public GameObject lightningEnemyPrefab;
    public GameObject fireEnemyPrefab;
    public GameObject iceEnemyPrefab;


    [Header("GameObjects with 'EnemyMovementPaths' go here")]
        [Tooltip("This one needs a GameObject with the 'EnemyMovementPath' Script attached.")]
    public GameObject[] enemyMovementPathsObjects;

        [Header ("These control mini-waves of enemies.")]
        [Tooltip ("0=neutral; 1=lightning; 2=fire; 3=ice.")]
    public int[] waveEnemyElementToSpawn;
        [Tooltip ("Choose which path that you added above to use. Starts at 0.")]
    public int[] waveRouteToTake;
        [Tooltip("How many in this mini-wave of this type with this route to spawn")]
    public int[] waveNumberOfEnemies;
        [Tooltip("Delay before spawning this mini-wave")]
    public float[] waveDelayBeforeWave;
        [Tooltip("The delay between spawning inside a wave of enemies.")]
    public float delayInsideWave = .5f;

    private int indexEnemyElement = 0;
    private int indexRouteToTake = 0;
    private int indexNumberOfEnemies = 0;
    private int indexDelayBeforeWaves = 0;
    private int enemyAmountInWave;
    private bool waveIsSpawning = false;
    private int totalWaves = 0;
    private int currentWaves = 0;

    EnemyHealthBar enemyHealthBarScript;

    private void Start()
    {
        //find which "mini-wave" array has biggest ammount of values, makes it the wave total
        totalWaves = Mathf.Max(Mathf.Max(Mathf.Max(waveEnemyElementToSpawn.Length, waveRouteToTake.Length), waveNumberOfEnemies.Length), waveDelayBeforeWave.Length);

        //find total number of enemies that must be defeated for the "WinCondition" script
        for (int i = 0; i < waveNumberOfEnemies.Length; i++)
        {
            WinCondition.totalEnemiesToRemoveToWin = WinCondition.totalEnemiesToRemoveToWin + waveNumberOfEnemies[i];
        }

        //set healthbar max to found total
        print("Total enemies to be destroyed for win condition: " + WinCondition.totalEnemiesToRemoveToWin);
        enemyHealthBarScript.maxHealth = WinCondition.totalEnemiesToRemoveToWin;

    }

    void Update()
    {
        //track time
        timer = timer + Time.deltaTime;

        //....................................Spawn 'wave' at delay time
        if (timer >= waveDelayBeforeWave[indexDelayBeforeWaves] && waveIsSpawning == false && currentWaves < totalWaves)
        {
            timer = 0;
            waveIsSpawning = true;
        }

        //....................................Spawn enemy inside 'wave' at delay time
        if (timer >= delayInsideWave && waveIsSpawning == true)
        {
            timer = 0;
            enemyAmountInWave++;
            GameObject prefabToSpawn = normalEnemyPrefab;

            //....................................Set enemy type
            if (waveEnemyElementToSpawn[indexEnemyElement] == 0)
            {
                prefabToSpawn = normalEnemyPrefab;
            }
            if (waveEnemyElementToSpawn[indexEnemyElement] == 1)
            {
                prefabToSpawn = lightningEnemyPrefab;
            }
            if (waveEnemyElementToSpawn[indexEnemyElement] == 2)
            {
                prefabToSpawn = fireEnemyPrefab;
            }
            if (waveEnemyElementToSpawn[indexEnemyElement] == 3)
            {
                prefabToSpawn = iceEnemyPrefab;
            }

            //....................................Spawn enemy
            GameObject newEnemy = Instantiate(prefabToSpawn, gameObject.transform.position, gameObject.transform.rotation);

            //....................................Give spawned unit movement orders
            newEnemy.GetComponent<EnemyMoveToWaypoints>().OnCreationWaypoints(enemyMovementPathsObjects[waveRouteToTake[indexRouteToTake]]);

            //....................................Turn off 'wave' when limit reached, go to next 'wave' in arrays
            if (enemyAmountInWave >= waveNumberOfEnemies[indexNumberOfEnemies])
            {
                waveIsSpawning = false;

                enemyAmountInWave = 0;

                currentWaves++;

                //cycle wave indexes; this way, if they have less actions than the max number of waves, they start from the top again
                if (indexDelayBeforeWaves + 1 >= waveDelayBeforeWave.Length)
                {
                    indexDelayBeforeWaves = 0;
                }
                else
                {
                    indexDelayBeforeWaves++;
                }

                if (indexEnemyElement + 1 >= waveEnemyElementToSpawn.Length)
                {
                    indexEnemyElement = 0;
                }
                else
                {
                    indexEnemyElement++;
                }

                if (indexNumberOfEnemies + 1 >= waveNumberOfEnemies.Length)
                {
                    indexNumberOfEnemies = 0;
                }
                else
                {
                    indexNumberOfEnemies++;
                }

                if (indexRouteToTake + 1 >= waveRouteToTake.Length)
                {
                    indexRouteToTake = 0;
                }
                else
                {
                    indexRouteToTake++;
                }   
            }
        }
    }
}
