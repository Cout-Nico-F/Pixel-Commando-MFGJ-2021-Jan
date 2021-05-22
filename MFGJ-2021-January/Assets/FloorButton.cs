using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public GameObject openingDoors;
    public GameObject counter1;
    public GameObject counter2;
    public GameObject counter3;
    private OpeningDoors openingDoorsController;

    private float collisionTime;
    private int objectsInTrigger;

    private int buttonHoldCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        openingDoorsController = openingDoors.GetComponent<OpeningDoors>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonHoldCounter(collisionTime);
        CountDown(collisionTime);
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
            collisionTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DeployedBomb"))
        {
            Debug.Log(collision.name + " exited");
            objectsInTrigger -= 1;
        }
        
        collisionTime = buttonHoldCounter;
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
            collisionTime -= Time.deltaTime;
        }
    }

    private void DoorTimerController(int ibuttonHoldCounter)
    {
        if (buttonHoldCounter > 0)
        {
            openingDoorsController.OpenDoors("Puzzle_GreenDoor");
        }
        else
        {
            openingDoorsController.CloseDoors("Puzzle_GreenDoor");
        }
    }

}
