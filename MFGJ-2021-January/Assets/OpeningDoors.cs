using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        for (var i = 0; i < doors.Length; i++)
        {
            var doorTransform = doors[i].GetComponent<Transform>().localScale;
            doors[i].GetComponent<Transform>().localScale = new Vector3(doorTransform.x, 1f, doorTransform.z);
        }
    }


}
