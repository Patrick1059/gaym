using UnityEngine;

public class EnemyController : MonoBehaviour
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

    void Awake()
    {
        // Get a reference to the player's health script
        playerHealth = player.GetComponent<PlayerHealth>();

        // Get a reference to the audio source component
        audioSource = GetComponent<AudioSource>();

        // Set the initial value of the damage timer
        damageTimer = damageInterval;
    }

    void Update()
    {
        // Get the direction to the player
        Vector2 direction = player.transform.position - transform.position;

        // Normalize the direction
        direction.Normalize();

        // Move the enemy in the direction of the player
        transform.position = transform.position + (Vector3) direction * speed * Time.deltaTime;

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
}
