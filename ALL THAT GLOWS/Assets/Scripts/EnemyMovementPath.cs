using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPath : MonoBehaviour
{
    [Header("Put waypoints below (ending at castle).")]
    [Tooltip("This 'script' just passes the waypoints you put into here off into the SpawnerOfEnemiesScript.")]
    public GameObject[] wayPoints;
}
