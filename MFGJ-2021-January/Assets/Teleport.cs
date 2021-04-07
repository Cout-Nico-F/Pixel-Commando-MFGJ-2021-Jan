using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPosition;
    [SerializeField]
    private bool isExit;

    [SerializeField] private float RightLimit;
    [SerializeField] private float LeftLimit;
    [SerializeField] private float UpLimit;
    [SerializeField] private float DownLimit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //turn camera follow Smooth effect off.
            float speed = FindObjectOfType<CameraFollow>().SmoothSpeed;
            Camera.main.GetComponent<CameraFollow>().SmoothSpeed = 0;
            if (isExit)
            {
                Camera.main.GetComponent<LimitMovement>().ResetConfiguration();
            }
            else
            {
                Camera.main.GetComponent<LimitMovement>().xLimit_Right = RightLimit;
                Camera.main.GetComponent<LimitMovement>().xLimit_Left = LeftLimit;
                Camera.main.GetComponent<LimitMovement>().yLimit_Up = UpLimit;
                Camera.main.GetComponent<LimitMovement>().yLimit_Down = DownLimit;
            }   
            //move player to desired position
            collision.transform.position = teleportPosition.position;

            //return camera follow to normal.
            Camera.main.GetComponent<CameraFollow>().SmoothSpeed = speed;
        }
    }
}
