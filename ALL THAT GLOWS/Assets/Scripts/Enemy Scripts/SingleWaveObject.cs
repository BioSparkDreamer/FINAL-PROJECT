using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWaveObject : MonoBehaviour
{
    [Tooltip("Choose how long to wait before spawning this wave.")]
    public float delayBeforeThisWave;

    [Header("These control mini-waves of enemies.")]
    [Tooltip("0=neutral; 1=lightning; 2=fire; 3=ice.")]
    public int[] waveEnemyElementToSpawn;
    [Tooltip("Choose which spawner to create this mini-wave at in the Spawn/Wave Controller. Starts at 0.")]
    public int[] waveSpawnerToUse;
    [Tooltip("Choose which path in the Spawn/Wave Controller this mini-wave should take. Starts at 0.")]
    public int[] waveRouteToTake;
    [Tooltip("How many in this mini-wave of this type with this route to spawn. Also determines how many waves there are.")]
    public int[] waveNumberOfEnemies;
    [Tooltip("Delay before spawning this mini-wave")]
    public float[] waveDelayBeforeWave;
    [Tooltip("The delay between spawning inside a wave of enemies.")]
    public float delayInsideWave = .5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
