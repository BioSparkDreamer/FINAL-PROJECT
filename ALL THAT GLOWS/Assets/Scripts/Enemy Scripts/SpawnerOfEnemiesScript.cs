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

    private int indexEnemyMovementPathsObjects = 0;

    [Header("GameObjects with 'SingleWaveObject' go here")]
    [Tooltip("This one needs a GameObject with the 'SingleWaveObject' Script attached.")]
    public GameObject[] singleWaveObjects;

    private int indexSingleWaveObject = 0;

    [Header("GameObjects with the location to spawn at go here")]
    [Tooltip("Does not need a script attached.")]
    public GameObject[] spawnerLocationObjects;

    private int indexSpawnerLocationObjects = 0;

    private bool fullWaveIsSpawning = false;
    private SingleWaveObject currentWaveObjectReference;

    private int indexEnemyElement = 0;
    private int indexNumberOfEnemies = 0;
    private int indexDelayBeforeWaves = 0;
    private int enemyAmountInMiniWave;
    private bool miniWaveIsSpawning = false;
    private int totalMiniWaves = 0;
    private int currentMiniWaves = 0;
    private int enemiesToDefeatCurrentWave = 0;

    [Header("Healthbar object with script goes here")]
    public GameObject healthBar;

    EnemyHealthBar enemyHealthBarScript;

    private void Start()
    {
        //get healthbar
        enemyHealthBarScript = healthBar.GetComponent<EnemyHealthBar>();

        //find total number of enemies that must be defeated for the "WinCondition" script
        for (int j = 0; j < singleWaveObjects.Length; j++)
        {
            for (int i = 0; i < singleWaveObjects[j].GetComponent<SingleWaveObject>().waveNumberOfEnemies.Length; i++)
            {
                WinCondition.totalEnemiesToRemoveToWin = WinCondition.totalEnemiesToRemoveToWin + singleWaveObjects[j].GetComponent<SingleWaveObject>().waveNumberOfEnemies[i];
            }

            print("Total enemies to be destroyed this game: " + WinCondition.totalEnemiesToRemoveToWin);
            enemyHealthBarScript.maxHealth = WinCondition.totalEnemiesToRemoveToWin;
        }

        //set initial wave object
        currentWaveObjectReference = singleWaveObjects[indexSingleWaveObject].GetComponent<SingleWaveObject>();
    }

    void Update()
    {
        //track time
        timer = timer + Time.deltaTime;

        //....................................Spawn 'full-wave' at delay time
        if (timer >= currentWaveObjectReference.delayBeforeThisWave && fullWaveIsSpawning == false)
        {
            timer = 0;
            fullWaveIsSpawning = true;

            enemiesToDefeatCurrentWave = 0;

            indexDelayBeforeWaves = 0;
            indexEnemyElement = 0;
            indexNumberOfEnemies = 0;
            indexEnemyMovementPathsObjects = 0;


            //get wave object reference
            currentWaveObjectReference = singleWaveObjects[indexSingleWaveObject].GetComponent<SingleWaveObject>();

            //set mini wave total from wave object
            totalMiniWaves = currentWaveObjectReference.waveNumberOfEnemies.Length;

            //find number of enemies to be defeated in wave for the healthbar
            for (int i = 0; i < currentWaveObjectReference.waveNumberOfEnemies.Length; i++)
            {
                enemiesToDefeatCurrentWave = enemiesToDefeatCurrentWave + currentWaveObjectReference.waveNumberOfEnemies[i];
            }

            //set healthbar max to found total
            print("Total enemies to be destroyed for this wave: " + enemiesToDefeatCurrentWave);
            int enemyDifference = enemyHealthBarScript.maxHealth - enemyHealthBarScript.currentEnemyHealth; //how many are left?
            enemyHealthBarScript.maxHealth = enemyDifference + enemiesToDefeatCurrentWave;
            enemyHealthBarScript.currentEnemyHealth = enemyDifference;
        }

        //....................................Spawn 'mini-wave' at delay time if full-wave is spawning
        if (timer >= currentWaveObjectReference.waveDelayBeforeWave[indexDelayBeforeWaves] && miniWaveIsSpawning == false && fullWaveIsSpawning == true && currentMiniWaves < totalMiniWaves)
        {
            timer = 0;
            miniWaveIsSpawning = true;
        }

        //....................................Spawn enemy inside 'mini-wave' at delay time
        if (timer >= currentWaveObjectReference.delayInsideWave && miniWaveIsSpawning == true)
        {
            timer = 0;
            enemyAmountInMiniWave++;
            GameObject prefabToSpawn = normalEnemyPrefab;

            //....................................Set enemy type
            if (currentWaveObjectReference.waveEnemyElementToSpawn[indexEnemyElement] == 0)
            {
                prefabToSpawn = normalEnemyPrefab;
            }
            if (currentWaveObjectReference.waveEnemyElementToSpawn[indexEnemyElement] == 1)
            {
                prefabToSpawn = lightningEnemyPrefab;
            }
            if (currentWaveObjectReference.waveEnemyElementToSpawn[indexEnemyElement] == 2)
            {
                prefabToSpawn = fireEnemyPrefab;
            }
            if (currentWaveObjectReference.waveEnemyElementToSpawn[indexEnemyElement] == 3)
            {
                prefabToSpawn = iceEnemyPrefab;
            }

            //....................................Spawn enemy at a spawner location
            GameObject newEnemy = Instantiate(prefabToSpawn, spawnerLocationObjects[currentWaveObjectReference.waveSpawnerToUse[indexSpawnerLocationObjects]].transform.position, spawnerLocationObjects[currentWaveObjectReference.waveSpawnerToUse[indexSpawnerLocationObjects]].transform.rotation);

            //....................................Give spawned unit movement orders
            newEnemy.GetComponent<EnemyMoveToWaypoints>().OnCreationWaypoints(enemyMovementPathsObjects[currentWaveObjectReference.waveRouteToTake[indexEnemyMovementPathsObjects]]);

            //....................................Turn off 'mini-wave' when limit reached, go to next 'mini-wave' in arrays
            if (enemyAmountInMiniWave >= currentWaveObjectReference.waveNumberOfEnemies[indexNumberOfEnemies])
            {
                miniWaveIsSpawning = false;

                enemyAmountInMiniWave = 0;

                currentMiniWaves++;


                //cycle mini-wave indexes; this way, if they have less actions than the max number of mini-waves, they start from the top again
                if (indexDelayBeforeWaves + 1 >= currentWaveObjectReference.waveDelayBeforeWave.Length)
                {
                    indexDelayBeforeWaves = 0;
                }
                else
                {
                    indexDelayBeforeWaves++;
                }

                if (indexEnemyElement + 1 >= currentWaveObjectReference.waveEnemyElementToSpawn.Length)
                {
                    indexEnemyElement = 0;
                }
                else
                {
                    indexEnemyElement++;
                }

                if (indexNumberOfEnemies + 1 >= currentWaveObjectReference.waveNumberOfEnemies.Length)
                {
                    indexNumberOfEnemies = 0;
                }
                else
                {
                    indexNumberOfEnemies++;
                }

                if (indexEnemyMovementPathsObjects + 1 >= currentWaveObjectReference.waveRouteToTake.Length)
                {
                    indexEnemyMovementPathsObjects = 0;
                }
                else
                {
                    indexEnemyMovementPathsObjects++;
                }

                if (indexSpawnerLocationObjects + 1 >= currentWaveObjectReference.waveSpawnerToUse.Length)
                {
                    indexSpawnerLocationObjects = 0;
                }
                else
                {
                    indexSpawnerLocationObjects++;
                }
            }
        }

        //....................................Start next wave after finishing all mini-waves of this wave
        if (currentMiniWaves == totalMiniWaves && fullWaveIsSpawning == true && indexSingleWaveObject + 1 < singleWaveObjects.Length)
        {
            timer = 0;
            fullWaveIsSpawning = false;
            indexSingleWaveObject++;
            print("Next wave activated");
            currentMiniWaves = 0;

            //reset mini-wave indexes
            indexDelayBeforeWaves = 0;
            indexEnemyElement = 0;
            indexNumberOfEnemies = 0;
            indexEnemyMovementPathsObjects = 0;
            indexSpawnerLocationObjects = 0;
        }
    }
}
