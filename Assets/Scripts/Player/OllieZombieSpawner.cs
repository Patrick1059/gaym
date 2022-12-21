using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllieZombieSpawner : MonoBehaviour
{
    // The projectile prefab that will be instantiated when the player shoots
    public GameObject projectilePrefab;

    // The rate at which the player can shoot projectiles
    public float fireRate = 0.5f;

    // The amount of time that the projectile will exist before being destroyed
    public float projectileLifetime = 2f;

    // A timer that tracks how much time has passed since the player last shot a projectile
    private float fireTimer = 2f;

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        // If the player is holding down the left mouse button
        if (Input.GetMouseButton(0))
        {
            // If the fire timer has reached the fire rate, shoot a projectile and reset the fire timer
            if (fireTimer >= fireRate)
            {

                // Create a new projectile at the position of the player
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

                // Destroy the projectile after a certain amount of time
                Destroy(projectile, projectileLifetime);

                // Reset the fire timer
                fireTimer = 0f;
            }
        }
    }
}