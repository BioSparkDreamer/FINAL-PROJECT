using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentEnemyHealth;
    //Destroy enemies when they reach this object
    public HealthBar healthBar;
    void Start()
    {
        currentEnemyHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        EnemyTakeDamage(1);
    }
    void EnemyTakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        healthBar.SetHealth(currentEnemyHealth);
    }
    public void EnemyKill()
    {
        EnemyTakeDamage(1);
        Debug.Log("Working");
    }
}
