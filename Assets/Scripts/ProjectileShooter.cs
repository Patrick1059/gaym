using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    // The projectile prefab that will be instantiated when the player shoots
    public GameObject projectilePrefab;

    // The force that will be applied to the projectile when it is shot
    public float projectileForce = 10f;

    // The rate at which the player can shoot projectiles
    public float fireRate = 0.5f;

    // The amount of time that the projectile will exist before being destroyed
    public float projectileLifetime = 2f;

    // A timer that tracks how much time has passed since the player last shot a projectile
    private float fireTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // If the player is holding down the left mouse button
        if (Input.GetMouseButton(0))
        {
            // Increment the fire timer by the time that has passed since the last frame
            fireTimer += Time.deltaTime;

            // If the fire timer has reached the fire rate, shoot a projectile and reset the fire timer
            if (fireTimer >= fireRate)
            {
                // Get the position of the mouse cursor in world space
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Create a new projectile at the position of the player
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

                // Get the Rigidbody2D component of the projectile
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                // Calculate the direction from the player to the mouse cursor
                Vector2 direction = mousePos - transform.position;

                // Normalize the direction vector to remove any scaling caused by the distance between the player and the mouse cursor
                direction.Normalize();

                // Apply the force to the Rigidbody2D component of the projectile in the calculated direction
                rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);

                // Destroy the projectile after a certain amount of time
                Destroy(projectile, projectileLifetime);

                // Reset the fire timer
                fireTimer = 0f;
            }
        }
    }
}