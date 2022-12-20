using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePowerUp : MonoBehaviour
{
    // The increase in fire rate that the coffee power-up will give to the player
    public float fireRateIncrease = 1.5f;

    // This function is called when the coffee power-up is collided with
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collided object is the player...
        if (collision.gameObject.tag == "Player")
        {
            // ... get the PlayerShoot script component on the player...
            ProjectileShooter ProjectileShooterScript = collision.gameObject.GetComponent<ProjectileShooter>();

            // ... increase the fire rate of the player...
            ProjectileShooterScript.fireRate *= 1/fireRateIncrease;

            // ... and destroy the coffee power-up
            Destroy(gameObject);
        }
    }
}