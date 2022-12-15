using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // The maximum health of the enemy
    public int maxHealth = 100;

    // The current health of the enemy
    private int currentHealth;

    void Awake()
    {
        // Set the initial value of the current health to the maximum health
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce the current health by the specified amount of damage
        currentHealth -= damage;

        // If the current health is less than or equal to zero, destroy the enemy game object
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
