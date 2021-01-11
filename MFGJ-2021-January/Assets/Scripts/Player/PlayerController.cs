using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 moveDirection;

    public Animator animPlayer;




    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        
        
       if (moveDirection != Vector2.zero)
       {
        animPlayer.SetFloat("Horizontal", moveX);
        animPlayer.SetFloat("Vertical", moveY);
        
       }
       animPlayer.SetFloat("Speed", moveDirection.sqrMagnitude);



        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
