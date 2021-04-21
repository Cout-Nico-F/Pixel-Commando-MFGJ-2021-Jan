using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] internal Tank tankScript;
    PlayerController playerScript;

    void Awake()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tankScript.lastXVal = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Look at player -> Flip Tank
        //LookAtPlayer();

        Move();
        SimpleFlip();
    }

    private void SimpleFlip()
    {
        if(transform.position.x > tankScript.lastXVal)
        {
            tankScript.render.flipX = true;
            tankScript.isFlipped = true;
        }
        else if (transform.position.x < tankScript.lastXVal)
        {
            tankScript.render.flipX = false;
            tankScript.isFlipped = false;
        }
    }

    private void CheckLastValX()
    {
        tankScript.lastXVal = transform.position.x;
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < playerScript.gameObject.transform.position.x && tankScript.isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            tankScript.isFlipped = false;
        }
        else if (transform.position.x > playerScript.gameObject.transform.position.x && !tankScript.isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            tankScript.isFlipped = true;
        }
    }

    private void Move()
    {
        if (this.transform.position != tankScript.patrolPoints[tankScript.currentPoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, tankScript.patrolPoints[tankScript.currentPoint].position, tankScript.speed * Time.deltaTime);
        }
        else
        {
            //Get next point (in order of patrolPoitns list) 
            tankScript.currentPoint = (tankScript.currentPoint + 1) % tankScript.patrolPoints.Length;
            CheckLastValX();
        }
    }

}
