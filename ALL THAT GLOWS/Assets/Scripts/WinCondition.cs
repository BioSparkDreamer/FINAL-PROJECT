using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    //...................................Variables
    public static int totalEnemiesRemovedSoFar = 0;
    public static int totalEnemiesToRemoveToWin = 0;


    void Update()
    {
        if (totalEnemiesRemovedSoFar >= totalEnemiesToRemoveToWin)
        {
            //win actions go here
            SceneManager.LoadScene(3);
        }
    }
}
