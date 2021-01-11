using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovement : MonoBehaviour
{
    public float xLimit;
    public float yLimit;

    private Transform thisTransform;

    
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        LimitObjectMovement();
    }

    void LimitObjectMovement()
    {
        if (thisTransform.position.x > xLimit)
        {
            thisTransform.position = new Vector3(xLimit, thisTransform.position.y, thisTransform.position.z);

        }

        if (thisTransform.position.x < -xLimit)
        {
            thisTransform.position = new Vector3(-xLimit, thisTransform.position.y, thisTransform.position.z);

        }
        if (thisTransform.position.y > yLimit)
        {
            thisTransform.position = new Vector3(thisTransform.position.x, yLimit, thisTransform.position.z);

        }

        if (thisTransform.position.y < -yLimit)
        {
            thisTransform.position = new Vector3(thisTransform.position.x, -yLimit, thisTransform.position.z);

        }
    }
}
