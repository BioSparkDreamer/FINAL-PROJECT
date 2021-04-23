using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyObjective : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    EnemyHealthBar e;

    public AudioSource castleDamageAudio;

    //Destroy enemies when they reach this object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
            e.EnemyKill();
            WinCondition.totalEnemiesRemovedSoFar++;
        }

    }
    public HealthBar healthBar;
    void Start()
    {
        GameObject enemyHealthBarObject = GameObject.FindWithTag("HBar");
        e = enemyHealthBarObject.GetComponent<EnemyHealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if(currentHealth == 0)
        {
            SceneManager.LoadScene(4);
            Destroy(gameObject);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        castleDamageAudio.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            TakeDamage(1);
            e.EnemyKill();
            WinCondition.totalEnemiesRemovedSoFar++;
        }
    }
}
