using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public GameObject bonePrefab;
    public float lastFired;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        if ((Input.GetButton("Fire1")) && Time.time > nextFire)
        {
                nextFire = Time.time + fireRate;
                GameObject kbone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
                kbone.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f,0.0f);
                Debug.Log(fireRate);
                Debug.Log(Time.time);
        }
    }
    void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
