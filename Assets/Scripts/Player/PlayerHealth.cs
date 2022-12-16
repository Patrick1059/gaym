using UnityEngine;
using System.Collections;

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
    public PlayerMovement playerMovement;
     bool deathSoundPlayed = false;

    void Awake()
    {
        // Set the initial health
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
        // Get a reference to the audio source component
        audioSource = GetComponent<AudioSource>();
        Debug.Log(audioSource);
    }

    public void TakeDamage(int damage)
{
    // If the player is still alive
    if (currentHealth > 0)
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
}

    IEnumerator WaitForAudio(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Kill the player
        Destroy(gameObject);
    }

  void Die()
{
    // Check if the death sound has already been played
    if (!deathSoundPlayed)
    {
        // If the death sound has not been played, play it
        audioSource.PlayOneShot(deathSound);
        deathSoundPlayed = true;

        // Set the player's move speed to 0
        playerMovement.moveSpeed = 0;
    }

    // Check if the audio is still playing
    if (audioSource.isPlaying)
    {
        // If the audio is still playing, wait for it to finish before destroying the GameObject
        audioSource.clip.LoadAudioData();
        StartCoroutine(WaitForAudio(audioSource.clip.length));
    }
    else
    {
        // If the audio is not playing, kill the player
        Destroy(gameObject);
    }
}

}
