using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public GameObject openingDoors;
    public GameObject counter1;
    public GameObject counter2;
    public GameObject counter3;
    [Tooltip("Should be assigned to a folder containing all doors of a specific color.")]
    private DoorManager openingDoorsController;
    
    private float totalCollisionTime;
    private float collisionTime;
    private float doorCloseSpeed = 1.8f;
    
    private int objectsInTrigger;

    private int buttonHoldCounter = 0;

    public float CollisionTime
    {
        get => collisionTime; 
        set
        {
            collisionTime = value;
            openingDoorsController.TotalCollisionTime = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        openingDoorsController = openingDoors.GetComponent<DoorManager>();
        totalCollisionTime = openingDoorsController.TotalCollisionTime;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonHoldCounter(CollisionTime);
        CountDown(CollisionTime);
        //Debug.Log(collisionTime);

        DoorTimerController(buttonHoldCounter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeployedBomb"))
        {
            objectsInTrigger += 1;
            Debug.Log(collision.name + " entered");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeployedBomb"))
        {
            CollisionTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeployedBomb"))
        {
            Debug.Log(collision.name + " exited");
            objectsInTrigger -= 1;
        }
        
        CollisionTime = buttonHoldCounter;
    }

    // I'm pretty sure there's an inifitely more efficient way to do this.
    private void ButtonHoldCounter(float iCollisionTime)
    {
        if (iCollisionTime <= 0f)
        {
            buttonHoldCounter = 0;

            counter1.transform.Find("CounterDisabled").gameObject.SetActive(true);
            counter1.transform.Find("CounterEnabled").gameObject.SetActive(false);
        }
        if (iCollisionTime > 0f)
        {
            buttonHoldCounter = 1;
            counter1.transform.Find("CounterDisabled").gameObject.SetActive(false);
            counter1.transform.Find("CounterEnabled").gameObject.SetActive(true);

            counter2.transform.Find("CounterDisabled").gameObject.SetActive(true);
            counter2.transform.Find("CounterEnabled").gameObject.SetActive(false);
        }
        if (iCollisionTime > 1f)
        {
            buttonHoldCounter = 2;
            counter2.transform.Find("CounterDisabled").gameObject.SetActive(false);
            counter2.transform.Find("CounterEnabled").gameObject.SetActive(true);

            counter3.transform.Find("CounterDisabled").gameObject.SetActive(true);
            counter3.transform.Find("CounterEnabled").gameObject.SetActive(false);
        }
        if (iCollisionTime > 2f)
        {
            buttonHoldCounter = 3;

            counter3.transform.Find("CounterDisabled").gameObject.SetActive(false);
            counter3.transform.Find("CounterEnabled").gameObject.SetActive(true);
        }
    }

    private void CountDown(float iCollisionTime)
    {
        if (iCollisionTime > 0 && objectsInTrigger == 0)
        {
            CollisionTime -= Time.deltaTime * doorCloseSpeed;
        }
    }

    private void DoorTimerController(int ibuttonHoldCounter)
    {
        if (buttonHoldCounter > 0)
        {
            openingDoorsController.OpenDoors(openingDoorsController.tag);
        }
        else
        {
            openingDoorsController.CloseDoors(openingDoorsController.tag);
        }
    }

}
