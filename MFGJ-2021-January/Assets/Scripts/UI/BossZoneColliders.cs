using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZoneColliders : MonoBehaviour
{
    bool canPass = false;

    // this.GetComponent<EdgeCollider2D>().isTrigger = true;

    public void toggleColor()
    {
        if (canPass)
        {
            canPass = false;
        }
       else canPass = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canPass && collision.CompareTag("Player"))
        {
            toggleColor();
        }
    }
}
