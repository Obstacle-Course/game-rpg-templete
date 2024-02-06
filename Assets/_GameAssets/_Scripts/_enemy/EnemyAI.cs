using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f; // Range at which the enemy detects the player
    public float attackRange = 2f; // Range at which the enemy attacks the player
    public float moveSpeed = 3f; // Speed of the enemy movement
    public int attackDamage = 10; // Damage dealt by the enemy's attack
    public float attackDelay = 2f; // Delay between attacks
    public Animator animator; // Reference to the enemy's Animator component
    public LayerMask playerLayer; // Layer of the player

    private Transform player; // Reference to the player's transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isPlayerInRange = false; // Flag to indicate if the player is in range
    private bool isAttacking = false; // Flag to indicate if the enemy is currently attacking

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject and get its transform
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to the enemy
    }

    void Update()
    {
        // Check if the player is in detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            isPlayerInRange = true;
            // Set destination to player position
            agent.SetDestination(player.position);
        }
        else
        {
            isPlayerInRange = false;
        }

        // Attack the player if in attack range and not currently attacking
        if (isPlayerInRange && Vector3.Distance(transform.position, player.position) <= attackRange && !isAttacking)
        {
            // Attack the player
            StartCoroutine(AttackWithDelay());
        }
    }

    IEnumerator AttackWithDelay()
    {
        // Set the attacking flag to true
        isAttacking = true;

        // Play attack animation
        animator.SetTrigger("Attack");

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(attackDelay);

        // Deal damage to the player
        DealDamage();

        // Set the attacking flag to false
        isAttacking = false;
    }

    void DealDamage()
    {
        // Get the player's health component
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();

        // Deal damage to the player if the player has a health component
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
