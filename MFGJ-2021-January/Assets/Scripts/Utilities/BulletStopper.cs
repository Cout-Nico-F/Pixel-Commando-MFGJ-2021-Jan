using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Damage"))
        {
            //Debug.Log("Bullet stopped by " + this.gameObject.name);
            Destroy(collision.gameObject,0.05f);
        }
    }
}
