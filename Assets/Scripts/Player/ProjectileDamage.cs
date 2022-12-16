using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 1; // The amount of damage the projectile does

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile has collided with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // Deal damage to the enemy
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
