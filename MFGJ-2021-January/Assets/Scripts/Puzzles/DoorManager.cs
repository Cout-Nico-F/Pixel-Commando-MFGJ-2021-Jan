using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private float totalCollisionTime;

    public float TotalCollisionTime { get => totalCollisionTime; set => totalCollisionTime = value; }

    private string getTag(string doorsTag)
    {
        var doorsFolder = GameObject.FindGameObjectWithTag(doorsTag);
        var doorTag = doorsFolder.GetComponentInChildren<Transform>().GetChild(0).tag;

        return doorTag;
    }

    public void OpenDoors(string doorsTag)
    {
        var doorTag = getTag(doorsTag);
        var doors = GameObject.FindGameObjectsWithTag(doorTag);
        for (var i=0; i<doors.Length; i++)
        {
            var doorTransform = doors[i].GetComponentInChildren<Transform>().localScale;
            doors[i].GetComponentInChildren<Transform>().localScale = new Vector3(doorTransform.x, 0.1f, doorTransform.z);
        }
    }

    public void CloseDoors(string doorsTag)
    {
        var doorTag = getTag(doorsTag);
        var doors = GameObject.FindGameObjectsWithTag(doorTag);

        if (totalCollisionTime <= 0)
        {
            for (var i = 0; i < doors.Length; i++)
            {
                var doorTransform = doors[i].GetComponentInChildren<Transform>().localScale;
                doors[i].GetComponentInChildren<Transform>().localScale = new Vector3(doorTransform.x, 1f, doorTransform.z);
            }
        }
    }
}
