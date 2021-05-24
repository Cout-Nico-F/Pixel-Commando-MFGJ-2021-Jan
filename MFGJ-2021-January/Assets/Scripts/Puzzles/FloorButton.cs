using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public GameObject openingDoors, counter1, counter2, counter3, spriteIdle, spritePressed;
    [Tooltip("Should be assigned to a folder containing all doors of a specific color.")]
    private DoorsManager openingDoorsController;
    
    private float totalCollisionTime;
    private float collisionTime;
    private float doorCloseSpeed = 1.8f;
    
    private int objectsInTrigger;

    private int buttonHoldCounter = 0;
    private bool countingDown = false;
    private GameObject counter1Enabled, counter2Enabled, counter3Enabled, counter1Disabled, counter2Disabled, counter3Disabled;

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
        openingDoorsController = openingDoors.GetComponent<DoorsManager>();
        totalCollisionTime = openingDoorsController.TotalCollisionTime;

        counter1Enabled = counter1.transform.Find("CounterEnabled").gameObject;
        counter2Enabled = counter2.transform.Find("CounterEnabled").gameObject;
        counter3Enabled = counter3.transform.Find("CounterEnabled").gameObject;
        counter1Disabled = counter1.transform.Find("CounterDisabled").gameObject;
        counter2Disabled = counter2.transform.Find("CounterDisabled").gameObject;
        counter3Disabled = counter3.transform.Find("CounterDisabled").gameObject;
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
            //Debug.Log(collision.name + " entered");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeployedBomb"))
        {
            CollisionTime += Time.deltaTime;
            //Debug.Log(collision.name + " still around");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeployedBomb"))
        {
            objectsInTrigger -= 1;
            //Debug.Log(collision.name + " exited");
        }

        CollisionTime = buttonHoldCounter;
    }

    // I'm pretty sure there's an inifitely more efficient way to do this.
    private void ButtonHoldCounter(float iCollisionTime)
    {
        if (iCollisionTime <= 0f)
        {
            buttonHoldCounter = 0;

            counter1Disabled.SetActive(true);
            counter1Enabled.SetActive(false);

            spriteIdle.SetActive(true);
            spritePressed.SetActive(false);
        }
        if (iCollisionTime > 0f)
        {
            buttonHoldCounter = 1;
            counter1Disabled.SetActive(false);
            counter1Enabled.SetActive(true);

            counter2Disabled.SetActive(true);
            counter2Enabled.SetActive(false);

            spriteIdle.SetActive(false);
            spritePressed.SetActive(true);
        }
        if (iCollisionTime > 1f)
        {
            buttonHoldCounter = 2;
            counter2Disabled.SetActive(false);
            counter2Enabled.SetActive(true);

            counter3Disabled.SetActive(true);
            counter3Enabled.SetActive(false);
        }
        if (iCollisionTime > 2f)
        {
            buttonHoldCounter = 3;

            counter3Disabled.SetActive(false);
            counter3Enabled.SetActive(true);
        }
    }

    private void CountDown(float iCollisionTime)
    {
        if (iCollisionTime > 0 && objectsInTrigger == 0)
        {
            CollisionTime -= Time.deltaTime * doorCloseSpeed;
            countingDown = true;
        }
        else
        {
            countingDown = false;
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
