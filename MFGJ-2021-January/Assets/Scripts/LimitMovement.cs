using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovement : MonoBehaviour
{
    public float xLimit;
    public float yLimit;

    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
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
