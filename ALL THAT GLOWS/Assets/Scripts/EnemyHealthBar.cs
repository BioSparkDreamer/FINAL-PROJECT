﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentEnemyHealth;
    public HealthBar healthBar;
    void Start()
    {
        currentEnemyHealth = 0;
        
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        EnemyTakeDamage(1);
    }
    void EnemyTakeDamage(int damage)
    {
        currentEnemyHealth += damage;
        healthBar.SetHealth(currentEnemyHealth);
    }
    public void EnemyKill()
    {
        EnemyTakeDamage(1);
        Debug.Log("Working");
    }
}
