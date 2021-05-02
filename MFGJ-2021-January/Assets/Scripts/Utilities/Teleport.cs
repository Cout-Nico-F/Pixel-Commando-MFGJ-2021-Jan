using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPosition;
    [SerializeField]
    private bool isExit;
    float normalSize;
    float normalSpeed;

    [SerializeField] private float RightLimit;
    [SerializeField] private float LeftLimit;
    [SerializeField] private float UpLimit;
    [SerializeField] private float DownLimit;

    private void Awake()
    {
        normalSize = Camera.main.orthographicSize;
        normalSpeed = FindObjectOfType<CameraFollow>().SmoothSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //turn camera follow Smooth effect off.
            Camera.main.GetComponent<CameraFollow>().SmoothSpeed = 0;

            if (isExit)
            {
                Camera.main.GetComponent<LimitMovement>().ResetConfiguration();
                Camera.main.transform.position = teleportPosition.position;
                //zoom out
                Camera.main.orthographicSize = normalSize;
            }
            else
            {
                Camera.main.GetComponent<LimitMovement>().xLimit_Right = RightLimit;
                Camera.main.GetComponent<LimitMovement>().xLimit_Left = LeftLimit;
                Camera.main.GetComponent<LimitMovement>().yLimit_Up = UpLimit;
                Camera.main.GetComponent<LimitMovement>().yLimit_Down = DownLimit;
                //zoom in
                Camera.main.orthographicSize *= 0.5f;
            }   
            //move player to desired position
            collision.transform.position = teleportPosition.position;

            //return camera follow to normal.
            Camera.main.GetComponent<CameraFollow>().SmoothSpeed = normalSpeed;
        }
    }
}
