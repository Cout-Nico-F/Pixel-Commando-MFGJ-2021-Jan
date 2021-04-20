using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    internal Tank tankScript;
    public PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tankScript.patrolPoints.Length; i++)
        {
            tankScript.patrolPoints[i] = GameObject.Find("Point (" + i + ")").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Look at player -> Flip Tank
        LookAtPlayer();
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
        else if (transform.position.x > playerScript.gameObject.transform..position.x && !tankScript.isFlipped)
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
        }
    }

}
