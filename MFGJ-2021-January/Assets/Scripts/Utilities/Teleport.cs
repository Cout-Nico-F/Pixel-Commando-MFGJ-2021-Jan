using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPosition;
    [SerializeField]
    private bool needsOtherReference;
    [SerializeField]
    private bool isTypeB;
    [SerializeField]
    private Transform exitSpot;

    private Teleport otherTeleportReference;

    [SerializeField]
    private bool isExit;
    float normalSize;
    float normalSpeed;

    [SerializeField] private float RightLimit;
    [SerializeField] private float LeftLimit;
    [SerializeField] private float UpLimit;
    [SerializeField] private float DownLimit;

    public Transform TeleportPosition { set => teleportPosition = value; }

    private void Awake()
    {
        normalSize = Camera.main.orthographicSize;
        normalSpeed = FindObjectOfType<CameraFollow>().SmoothSpeed;

        if (needsOtherReference)
        {
            if (isTypeB)
            {
                otherTeleportReference = GameObject.Find("/JungleHut_B_inside/ExitHut_B_Trigger").GetComponent<Teleport>();
            }
            else
            {
                otherTeleportReference = GameObject.Find("/JungleHut_inside/ExitHutTrigger").GetComponent<Teleport>();
            }
        }
        
        if (teleportPosition == null)
        {
            if (isTypeB)
            {
                teleportPosition = GameObject.Find("/JungleHut_B_inside/Hut_B_EntranceSpawnPosition").transform;
            }
            else
            {
                var temp = GameObject.Find("/JungleHut_inside/HutEntranceSpawnPosition");
                if (temp != null)
                {
                    teleportPosition = temp.transform;
                }            
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (needsOtherReference)
            {
                otherTeleportReference.teleportPosition = exitSpot;
            }
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
