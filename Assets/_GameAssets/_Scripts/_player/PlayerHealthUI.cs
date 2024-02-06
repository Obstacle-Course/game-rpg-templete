using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image healthImage; // Reference to the health image for the player
    private HealthSystem playerHealth; // Reference to the player's health system

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>(); // Get the player's health system
    }

    void Update()
    {
        // Update the fill amount of the health image based on the player's current health
        healthImage.fillAmount = (float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth;
    }
}
