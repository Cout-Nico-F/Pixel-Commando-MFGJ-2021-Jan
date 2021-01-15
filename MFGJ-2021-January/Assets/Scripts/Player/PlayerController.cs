using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stats
    float moveSpeed;
    public float normalSpeed = 5f;
    public float runSpeed = 10f;
    public int healthPoints = 100; 

    public Rigidbody2D rb;

    Vector2 moveDirection;

    public Animator animPlayer;

    [HideInInspector]
    public bool isFacingRight = true;
    [HideInInspector]
    public bool isFacingLeft, isFacingUp, isFacingDown = false;

    public bool isRunning = false;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        animPlayer.SetFloat("Horizontal", difference.x);
        animPlayer.SetFloat("Vertical", difference.y);
        animPlayer.SetFloat("Speed", moveDirection.sqrMagnitude);

        UpdateDirection(rotZ);
    }

    void FixedUpdate()
    {
        CharacterRun();
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void UpdateDirection(float rotZ)
    {
        if(-45 < rotZ && rotZ < 45)
        {
            isFacingRight = true;
            isFacingLeft = false;
            isFacingUp = false;
            isFacingDown = false;
        }
        else if(rotZ < -135 || rotZ > 136)
        {
            isFacingRight = false;
            isFacingLeft = true;
            isFacingUp = false;
            isFacingDown = false;
        }
        else if(46 < rotZ && rotZ < 135)
        {
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = true;
            isFacingDown = false;
        }
        else if(-136 < rotZ && rotZ < -46)
        {
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = false;
            isFacingDown = true;
        }
    }

    void CharacterRun()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else
        {
            isRunning = false;
            moveSpeed = normalSpeed;
        }

    }
    void RunningAnimation()
    {
        if(isRunning)
        {
            
        }
        else
        {
            
        }
    }

}
