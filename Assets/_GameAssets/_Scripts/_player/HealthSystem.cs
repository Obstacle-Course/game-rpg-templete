using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

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
        // Handle player death, such as game over or respawn logic
        Debug.Log("Player died!");
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
