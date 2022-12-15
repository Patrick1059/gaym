using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;


    //!!!!IMPORTANT!!!! \/ \/ \/ \/ \/ \/ \/ PLEASE READ WILL SAVE YOU LOTS OF TIME!!
    //pro tip, changing the above values and rerunning the game will not change those values in the inspector, meaning that it will have no effect on the way the game behaves. You have to change them there. I think the values here are what it will set it to when the game is running outside the editor or when you launch the editor. This cost me over an hour of pain
    //!!!!IMPORTANT!!!! ^^^^^^

    // Start is called before the first frame update
    // Update is called once per frame

    void Update() //updates based on framerate
    {
        
        ProcessInputs();

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

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
//https://www.youtube.com/watch?v=7-8nE9_FwWs. Used this for mouse targeting although line defining target needs to be fixed