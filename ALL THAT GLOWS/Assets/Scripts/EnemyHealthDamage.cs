using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealthDamage : MonoBehaviour
{
    //.................................Variables
    public int defaultHealth = 100;
    private int currentHealth = 0;

    public int mismatchElementResistPercentage = 50;
    public int matchElementWeaknessPercentage = 100;

    public int defaultElement0_None1_Lightning2_Fire3_Ice = 0;
    private int currentElement;
    EnemyHealthBar e;
    public Action onEnemyDeath;
    void Start()
    {
        GameObject enemyHealthBarObject = GameObject.FindWithTag("HBar");
        e = enemyHealthBarObject.GetComponent<EnemyHealthBar>();

        //.................................Set default "backup" values
        if (currentHealth == 0)
        {
            currentHealth = defaultHealth;
            currentElement = defaultElement0_None1_Lightning2_Fire3_Ice;
        }
    }

    //.................................Function take damage
    public void EnemyTakeDamage(int damageAmmount, int elementType)
    {
        //damage from matching element
        if (currentElement == elementType && currentElement != 0)
        {
            currentHealth -=  damageAmmount * matchElementWeaknessPercentage / 100;
        }

        //damage from mismatched element
        if (currentElement != elementType && currentElement != 0)
        {
            currentHealth -=  damageAmmount * mismatchElementResistPercentage / 100;
        }

        //damage if enemy has no element

        if (currentElement == 0)
        {
            currentHealth -= damageAmmount;
        }

        //destroy if health reaches 0
        if (currentHealth <= 0)
        {
            e.EnemyKill();

            WinCondition.totalEnemiesRemovedSoFar++;
            Destroy(gameObject);
        }
    }
}
