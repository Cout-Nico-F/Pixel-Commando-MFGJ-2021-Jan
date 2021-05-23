using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private float totalCollisionTime;

    public float TotalCollisionTime { get => totalCollisionTime; set => totalCollisionTime = value; }

    public void OpenDoors(string doorColor)
    {
        var doors = GameObject.FindGameObjectsWithTag(doorColor);
        for (var i=0; i<doors.Length; i++)
        {
            var doorTransform = doors[i].GetComponent<Transform>().localScale;
            doors[i].GetComponent<Transform>().localScale = new Vector3(doorTransform.x, 0.1f, doorTransform.z);
        }
    }

    public void CloseDoors(string doorColor)
    {
        var doors = GameObject.FindGameObjectsWithTag(doorColor);
        
        if (totalCollisionTime <= 0)
        {
            for (var i = 0; i < doors.Length; i++)
            {
                var doorTransform = doors[i].GetComponent<Transform>().localScale;
                doors[i].GetComponent<Transform>().localScale = new Vector3(doorTransform.x, 1f, doorTransform.z);
            }
        }
        
    }

}
