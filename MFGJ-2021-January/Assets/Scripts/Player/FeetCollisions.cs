using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollisions : MonoBehaviour
{
    [SerializeField] FootStepsSound m_FootStepsSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WaterOnConcrete" ||
            collision.gameObject.tag == "Sand" ||
            collision.gameObject.tag == "Wood"||
            collision.gameObject.tag == "Water"||
            collision.gameObject.tag == "Concrete")
        {
            m_FootStepsSound.SurfaceSelection(collision);
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WoodInsideRoom")
        {
            m_FootStepsSound.SurfaceSelection(collision);
        }
    }*/
}
