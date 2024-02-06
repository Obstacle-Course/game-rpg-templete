using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Animator animator; // Reference to the player's Animator component
    public float attackRange = 2f; // Range of the melee attack
    public int attackDamage = 10; // Damage dealt by the melee attack
    public LayerMask enemyLayers; // Layers of the enemies

    void Update()
    {
        // Perform melee attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Detect enemies in attack range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);

        // Apply damage to each detected enemy
        foreach (Collider enemy in hitEnemies)
        {
            // Get the enemy's health component
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            // Check if the enemy is dead
            if (enemyHealth != null && !enemyHealth.IsDead())
            {
                // Deal damage if the enemy is not dead
                enemyHealth.TakeDamage(attackDamage);
            }
            else
            {
                // Debug message if the enemy is dead
                Debug.Log("There is no enemy to attack.");
                GameManager.instance.LevelText("find the key to open the door");
            }
        }
    }

    // Visualizing the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
