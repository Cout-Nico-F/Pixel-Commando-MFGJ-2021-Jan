using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 moveDirection;

    public Animator animPlayer;

    [HideInInspector]
    public bool isFacingRight = true;
    [HideInInspector]
    public bool isFacingLeft, isFacingUp, isFacingDown = false;


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
        else if(rotZ < -135 || rotZ > 135)
        {
            isFacingRight = false;
            isFacingLeft = true;
            isFacingUp = false;
            isFacingDown = false;
        }
        else if(45 < rotZ && rotZ < 135)
        {
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = true;
            isFacingDown = false;
        }
        else if(-135 < rotZ && rotZ < -45)
        {
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = false;
            isFacingDown = true;
        }
    }
}
