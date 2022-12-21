using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllieZombieSpawner : MonoBehaviour
{

    // The rate at which the player can shoot projectiles
    public float fireRate = 0.5f;
    // A timer that tracks how much time has passed since the player last shot a projectile
    private float fireTimer = 2f;
    public GameObject zombiePrefab;
    public float radius = 10f;

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
            // Call the SpawnObject method and pass in the player's position
            SpawnObject(transform.position);
            // Reset the fire timer
            fireTimer = 0f;
        }
    }
}
    void SpawnObject(Vector3 playerPos)
{
    Vector2 randomPos = Random.insideUnitCircle * radius;
    Vector3 spawnPos = playerPos + new Vector3(randomPos.x, 0, randomPos.y);
    GameObject spawnedObject = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
}
}