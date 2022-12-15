using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // The maximum health the player can have
    public int maxHealth = 100;

    // The current health the player has
    public int currentHealth;

    // A reference to the player's damage sound
    public AudioClip damageSound;

    // A reference to the player's death sound
    public AudioClip deathSound;

    // A reference to the audio source component on the player
    private AudioSource audioSource;

    void Awake()
    {
        // Set the initial health
        currentHealth = maxHealth;

        // Get a reference to the audio source component
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        // Play the damage sound
        audioSource.PlayOneShot(damageSound);

        // Reduce the current health by the amount of damage taken
        currentHealth -= damage;

        // If the player has no health, kill them
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play the death sound
        audioSource.PlayOneShot(deathSound);

        // Kill the player
        Destroy(gameObject);
    }
}