using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float hookRange = 10f; // Range of the hook
    public float hookSpeed = 20f; // Speed of the hook
    public float hookCooldown = 2f; // Cooldown time for using the hook
    public float maxMana = 100f; // Maximum mana
    public float manaRegenRate = 5f; // Rate at which mana regenerates per second
    public float healthPickupAmount = 20f; // Amount of health restored by a health pickup
    public LineRenderer hookLineRenderer; // Reference to the LineRenderer component for the hook

    private float currentMana; // Current mana of the player
    private HealthSystem healthSystem; // Reference to the HealthSystem component
    private bool canUseHook = true; // Flag to indicate if the hook can be used
    private bool isRegeneratingMana = false; // Flag to indicate if mana is regenerating

    private Vector3 hookEndPoint; // End point of the hook
    public Image manaBarImage;
    void Start()
    {
        currentMana = maxMana; // Set current mana to max mana at the start
        healthSystem = GetComponent<HealthSystem>(); // Get reference to the HealthSystem component

        // Disable the LineRenderer initially
        hookLineRenderer.enabled = false;
    }

    void Update()
    {
        // Regenerate mana over time
        if (!isRegeneratingMana)
        {
            isRegeneratingMana = true;
            InvokeRepeating("RegenerateMana", 1f, 1f);
        }

        // Use hook mechanic
        if (Input.GetKeyDown(KeyCode.Q) && canUseHook && currentMana >= 10f)
        {
            UseHook();
        }

        // Update mana bar UI
        UpdateManaBar();
    }
    void UpdateManaBar()
    {
        // Calculate fill amount based on current mana level
        float fillAmount = currentMana / maxMana;

        // Update the mana bar UI fill amount
        manaBarImage.fillAmount = fillAmount;
    }
    void UseHook()
    {
        // Consume mana
        currentMana -= 10f;
        if (currentMana < 0f)
        {
            currentMana = 0f;
        }

        // Start hook cooldown
        canUseHook = false;
        Invoke("ResetHookCooldown", hookCooldown);

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, hookRange);
        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Cast a ray towards the detected enemy
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (enemy.transform.position - transform.position).normalized, out hit, hookRange))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        // Pull the enemy towards the player
                        hookEndPoint = hit.point;
                        StartCoroutine(PullEnemy(enemy.gameObject));
                        return; // Exit the method if an enemy is successfully hooked
                    }
                }
            }
        }

        // If no enemy is found within hook range or an enemy was not successfully hooked, set the hook end point to the maximum range
        hookEndPoint = transform.position + transform.forward * hookRange;
        hookLineRenderer.positionCount = 2;
        hookLineRenderer.SetPosition(0, transform.position);
        hookLineRenderer.SetPosition(1, hookEndPoint);
        hookLineRenderer.enabled = true;
    }

    IEnumerator PullEnemy(GameObject enemy)
    {
        hookLineRenderer.positionCount = 2;
        hookLineRenderer.SetPosition(0, transform.position);
        hookLineRenderer.enabled = true;

        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();

        float distance = Vector3.Distance(transform.position, hookEndPoint);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * hookSpeed / distance;
            Vector3 newPos = Vector3.Lerp(transform.position, hookEndPoint, t);
            hookLineRenderer.SetPosition(1, newPos);

            // Calculate the direction to pull the enemy
            Vector3 pullDirection = (newPos - enemy.transform.position).normalized;

            // Apply force to pull the enemy towards the player
            enemyRigidbody.AddForce(pullDirection * hookSpeed * 100f * Time.deltaTime);

            yield return null;
        }

        // Hide the hook
        hookLineRenderer.enabled = false;
    }


    void RegenerateMana()
    {
        // Increase current mana up to the maximum value
        currentMana += manaRegenRate * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);

        // Check if mana is fully regenerated
        if (currentMana >= maxMana)
        {
            CancelInvoke("RegenerateMana");
            isRegeneratingMana = false;
        }
    }

    void ResetHookCooldown()
    {
        canUseHook = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a health pickup
        if (other.CompareTag("HealthPickup"))
        {
            // Restore player's health
            healthSystem.TakeDamage(-(int)healthPickupAmount); // Restore health using negative damage
            Destroy(other.gameObject); // Destroy the health pickup object
        }
    }
}
