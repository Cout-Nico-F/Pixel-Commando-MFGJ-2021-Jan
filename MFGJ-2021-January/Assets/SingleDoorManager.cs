using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorManager : MonoBehaviour
{
    public GameObject openedDoor, closedDoor;
    
    public void openDoor()
    {
        openedDoor.SetActive(true);
        closedDoor.SetActive(false);
    }

    public void closeDoor()
    {
        openedDoor.SetActive(false);
        closedDoor.SetActive(true);
    }
}
