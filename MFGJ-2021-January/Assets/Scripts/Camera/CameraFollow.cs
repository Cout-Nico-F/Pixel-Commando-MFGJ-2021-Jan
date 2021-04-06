using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target = null;
    [SerializeField]
    float smoothSpeed = 0.125f;
    [SerializeField]
    public Vector3 offset;

    private void FixedUpdate()
    {
        try
        {
            target = FindObjectOfType<PlayerController>().transform;

        }
        catch (System.Exception)
        {
            //this try-catch block is here to prevent a warning from player being null ( when is dead )
        }

        if (target != null)
        {
            Vector3 smoothedPosition = GetSmoothPosition();
            transform.position = smoothedPosition;
        }
    }
    public Vector3 GetSmoothPosition()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 a = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        return a;
    }
}
