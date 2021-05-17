using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovement : MonoBehaviour
{
    public float xLimit_Right;
    public float xLimit_Left;
    public float yLimit_Up;
    public float yLimit_Down;
    
    private float xLimit_Right_Default;
    private float xLimit_Left_Default;
    private float yLimit_Up_Default;
    private float yLimit_Down_Default;

    // Start is called before the first frame update
    void Start()
    {
        xLimit_Right_Default = xLimit_Right;
        xLimit_Left_Default = xLimit_Left;
        yLimit_Down_Default = yLimit_Down;
        yLimit_Up_Default = yLimit_Up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, xLimit_Left, xLimit_Right),
            Mathf.Clamp(transform.position.y, yLimit_Down, yLimit_Up),
            transform.position.z);
    }

    private void OldCode()
    {
        if (transform.position.x > xLimit_Right)
        {
            transform.position = new Vector3(xLimit_Right, transform.position.y, transform.position.z);

        }

        if (transform.position.x < xLimit_Left)
        {
            transform.position = new Vector3(xLimit_Left, transform.position.y, transform.position.z);

        }
        if (transform.position.y < yLimit_Down)
        {
            transform.position = new Vector3(transform.position.x, yLimit_Down, transform.position.z);

        }

        if (transform.position.y > yLimit_Up)
        {
            transform.position = new Vector3(transform.position.x, yLimit_Up, transform.position.z);

        }
    }
    public void ResetConfiguration()
    {
        xLimit_Right = xLimit_Right_Default;
        xLimit_Left = xLimit_Left_Default;
        yLimit_Down = yLimit_Down_Default;
        yLimit_Up = yLimit_Up_Default;
    }
}
