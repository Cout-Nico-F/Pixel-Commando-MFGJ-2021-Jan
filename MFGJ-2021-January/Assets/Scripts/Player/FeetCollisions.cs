using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollisions : MonoBehaviour
{
    public FootStepsSound footStepsSound;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        footStepsSound.SurfaceSelection(collision);
    }
}
