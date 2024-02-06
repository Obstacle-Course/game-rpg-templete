using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Image healthImage; // Reference to the health image for the enemy
    private EnemyHealth enemyHealth; // Reference to the enemy's health system

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>(); // Get the enemy's health system
    }

    void Update()
    {
        // Update the fill amount of the health image based on the enemy's current health
        healthImage.fillAmount = (float)enemyHealth.GetCurrentHealth() / enemyHealth.maxHealth;
        
    }
}
