using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    private float totalCollisionTime;

    public float TotalCollisionTime { get => totalCollisionTime; set => totalCollisionTime = value; }

    private void Start()
    {
        
    }

    private string getChildTag(string doorsTag)
    {
        var doorsFolder = GameObject.FindGameObjectWithTag(doorsTag);
        var doorTag = doorsFolder.GetComponentInChildren<Transform>().GetChild(0).GetChild(0).tag;

        return doorTag;
    }

    public void OpenDoors(string doorsTag)
    {
        var doorTag = getChildTag(doorsTag);
        var doors = GameObject.FindGameObjectsWithTag(doorTag);
        for (var i=0; i<doors.Length; i++)
        {
            doors[i].GetComponent<SingleDoorManager>().openDoor();
            //var doorTransform = doors[i].GetComponentInChildren<Transform>().localScale;
            //doors[i].GetComponentInChildren<Transform>().localScale = new Vector3(doorTransform.x, 0.1f, doorTransform.z);
        }
    }

    public void CloseDoors(string doorsTag)
    {
        var doorTag = getChildTag(doorsTag);
        var doors = GameObject.FindGameObjectsWithTag(doorTag);

        if (totalCollisionTime <= 0)
        {
            for (var i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<SingleDoorManager>().closeDoor();
                //var doorTransform = doors[i].GetComponentInChildren<Transform>().localScale;
                //doors[i].GetComponentInChildren<Transform>().localScale = new Vector3(doorTransform.x, 1f, doorTransform.z);
            }
        }
    }
}
