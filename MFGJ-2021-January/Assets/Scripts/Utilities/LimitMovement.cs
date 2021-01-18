using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovement : MonoBehaviour
{
    public float xLimit_Right;
    public float xLimit_Left;

    public float yLimit_Up;
    public float yLimit_Down;


    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTransform.position.x > xLimit_Right)
        {
            thisTransform.position = new Vector3(xLimit_Right, thisTransform.position.y, thisTransform.position.z);

        }

        if (thisTransform.position.x < xLimit_Left)
        {
            thisTransform.position = new Vector3(xLimit_Left, thisTransform.position.y, thisTransform.position.z);

        }
        if (thisTransform.position.y < yLimit_Down)
        {
            thisTransform.position = new Vector3(thisTransform.position.x, yLimit_Down, thisTransform.position.z);

        }

        if (thisTransform.position.y > yLimit_Up)
        {
            thisTransform.position = new Vector3(thisTransform.position.x, yLimit_Up, thisTransform.position.z);

        }
    }
}
