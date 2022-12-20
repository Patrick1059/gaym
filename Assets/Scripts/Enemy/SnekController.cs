using UnityEngine;
using System.Collections;

public class SnekController : MonoBehaviour
{
    // The speed at which the enemy moves
    public float speed = 5f;

    // The amount of damage the enemy does when it contacts the player
    public int damage = 10;

    // The interval at which the enemy does damage to the player (in seconds)
    public float damageInterval = 1f;

    // A reference to the player game object
    public GameObject player;

    // A reference to the player's health script
    private PlayerHealth playerHealth;

    // A reference to the enemy's damage sound
    public AudioClip damageSound;

    // A reference to the audio source component on the enemy
    private AudioSource audioSource;

    // A timer to track when to do damage to the player
    private float damageTimer;

    // A flag to track if the enemy is currently in contact with the player
    private bool inContactWithPlayer = false;
    public float chaseTime = 3f;
    public float stopTime = 3f;
    private string state;
    public float elapsedTime;

    void Awake()
{
    // Find the player GameObject in the scene using its tag
    player = GameObject.FindGameObjectWithTag("Player");

    // Get a reference to the player's health script
    playerHealth = player.GetComponent<PlayerHealth>();

    // Get a reference to the audio source component
    audioSource = GetComponent<AudioSource>();

    // Set the initial value of the damage timer
    damageTimer = damageInterval;
    state = "chasing";
}

    void Update()
    {
        elapsedTime += Time.deltaTime;

    // If the enemy is in the "chasing" state and the elapsed time is greater than or equal to the chase time, change the state to "stopped"
    if (state == "chasing" && elapsedTime >= chaseTime)
    {
        state = "stopped";
        elapsedTime = 0f;
    }
    // If the enemy is in the "stopped" state and the elapsed time is greater than or equal to the stop time, change the state to "chasing"
    else if (state == "stopped" && elapsedTime >= stopTime)
    {
        state = "chasing";
        elapsedTime = 0f;
    }

    // If the enemy is in the "chasing" state, move towards the player
    if (state == "chasing")
    {
        // Get the direction to the player
        Vector2 direction = player.transform.position - transform.position;

        // Normalize the direction
        direction.Normalize();

        // Move the enemy in the direction of the player
        transform.position = transform.position + (Vector3) direction * speed * Time.deltaTime;
    }
    // If the enemy is in the "stopped" state, do not move
    else if (state == "stopped")
    {
        // Do not move
    }
    // If the enemy is in the "stopped" state, do not move
    else if (state == "stopped")
    {
        // Decrement the stop time
        stopTime -= Time.deltaTime;

        // If the stop time has reached zero, change the state to "chasing" and reset the chase and stop times
        if (stopTime <= 0)
        {
            state = "chasing";
            chaseTime = 3f;
            stopTime = 3f;
        }
    }
        // If the enemy is in contact with the player, update the damage timer
        if (inContactWithPlayer)
        {
            // Update the damage timer
            damageTimer -= Time.deltaTime;

            // If the damage timer has reached zero, do damage to the player
            if (damageTimer <= 0)
            {
                // Play the damage sound
                audioSource.PlayOneShot(damageSound);

                // Do damage to the player
                playerHealth.TakeDamage(damage);

                // Reset the damage timer
                damageTimer = damageInterval;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the enemy contacts the player, set the in contact flag to true
        // and immediately do damage to the player
        if (other.gameObject == player)
        {
            inContactWithPlayer = true;
            playerHealth.TakeDamage(damage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If the enemy is no longer in contact with the player, set the in contact flag to false
        if (other.gameObject == player)
        {
            inContactWithPlayer = false;
        }
    }
    IEnumerator ChasePlayer()
{
    while (true)
    {
        // Get the direction to the player
        Vector2 direction = player.transform.position - transform.position;

        // Normalize the direction
        direction.Normalize();

        // Move the enemy in the direction of the player
        transform.position = transform.position + (Vector3) direction * speed * Time.deltaTime;

        // Wait for the chase time
        yield return new WaitForSeconds(chaseTime);

        // Stop moving for the stop time
        transform.position = transform.position;

        // Wait for the stop time
        yield return new WaitForSeconds(stopTime);
    }
}
}
