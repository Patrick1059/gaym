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
    private Vector3 target;
    public float projectileSpeed = 10.0f;
    private Camera cam;

    //!!!!IMPORTANT!!!! \/ \/ \/ \/ \/ \/ \/ PLEASE READ WILL SAVE YOU LOTS OF TIME!!
    //pro tip, changing the above values and rerunning the game will not change those values in the inspector, meaning that it will have no effect on the way the game behaves. You have to change them there. I think the values here are what it will set it to when the game is running outside the editor or when you launch the editor. This cost me over an hour of pain
    //!!!!IMPORTANT!!!! ^^^^^^

    // Start is called before the first frame update
    // Update is called once per frame

    void Start(){
        cam = Camera.main;
    }


    void Update() //updates based on framerate
    {
        
        ProcessInputs();
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); //this is broken, we need to somehow let Player interact with camera. Do we even have a camera object?
        Vector3 difference = target - rb.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize(); //all these calculations should probably be elsewhere
        fire(direction, rotationZ);
    }
    void FixedUpdate() //updates a set number of times per (frame?)
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void fire(Vector2 direction, float rotationZ){
         if ((Input.GetButton("Fire1")) && Time.time > nextFire)
        {
                nextFire = Time.time + fireRate;
                GameObject kbone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
                kbone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
                kbone.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed; 
        }
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
//https://www.youtube.com/watch?v=7-8nE9_FwWs. Used this for mouse targeting although line defining target needs to be fixed