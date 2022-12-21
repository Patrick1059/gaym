using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed; // The speed at which the game object will move
    public float attackRange; // The distance at which the game object will attack the enemy
    public int damage; // The amount of damage that the game object will deal upon contact with the enemy
    public float attackInterval = 0.5f;
    private float timeSinceLastAttack;
    public float projectileLifetime = 2f;

    private GameObject closestEnemy; // A reference to the closest enemy game object

    void Update()
    {
        // Find the closest enemy game object
        closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // Calculate the distance to the enemy
            float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);

            // If the distance to the enemy is within the attack range, attack the enemy
            if (distanceToEnemy <= attackRange)
            {
                AttackEnemy();
            }
            // If the distance to the enemy is outside the attack range, move towards the enemy
            else
            {
                MoveTowardsEnemy();
            }
        }
        Destroy(gameObject, projectileLifetime);
    }

    // This function finds the closest enemy game object
    private GameObject FindClosestEnemy()
    {
        // Find all enemy game objects in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Initialize the closest enemy to be null
        GameObject closest = null;

        // Initialize the distance to the closest enemy to be infinity
        float closestDistance = Mathf.Infinity;

        // Iterate through each enemy game object
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance to the enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // If the distance to the enemy is smaller than the current closest distance, set the closest enemy to be this enemy
            if (distanceToEnemy < closestDistance)
            {
                closest = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        // Return the closest enemy game object
        return closest;
    }

    // This function moves the game object towards the closest enemy
    private void MoveTowardsEnemy()
    {
        // Calculate the direction to the enemy
        Vector3 direction = closestEnemy.transform.position - transform.position;
        direction.Normalize();

        // Move the game object towards the enemy
        transform.position += direction * speed * Time.deltaTime;
    }

    // This function attacks the closest enemy
    private void AttackEnemy()
{
    // Increment the time since the last attack
    timeSinceLastAttack += Time.deltaTime;

    // If the attack interval has been reached, deal damage to the enemy and reset the time since the last attack
    if (timeSinceLastAttack >= attackInterval)
    {
        // Deal damage to the enemy
        closestEnemy.GetComponent<EnemyHealth>().TakeDamage(damage);

        // Reset the time since the last attack
        timeSinceLastAttack = 0;
    }
}
}