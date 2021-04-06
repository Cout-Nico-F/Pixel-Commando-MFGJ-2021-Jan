using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CameraFollow cameraFollow;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        var smooth = cameraFollow.GetSmoothPosition();
    }
}
