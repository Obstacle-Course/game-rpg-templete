using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy

    public EnemyHealthUI enemyHealthUI; // Reference to the EnemyHealthUI component

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health at the start
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        currentHealth -= damage;

        // Check if health is depleted
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (enemyHealthUI == null)
        {
            Debug.LogWarning("EnemyHealthUI reference is null!");
        }
        else
        {
            // Disable the health UI element
            enemyHealthUI.healthImage.gameObject.SetActive(false);
        }

        // Access GameManager through the singleton instance
        GameManager.instance.LevelText("find the key");
        GameManager.instance.keyObject.SetActive(true);

        // Handle enemy death, such as destruction or playing death animation
        Destroy(gameObject);
    }


    // Getter method to retrieve current health
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
