using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZoneColliders : MonoBehaviour
{
    bool canPass = false;


    public void ToggleColliderColorandState()
    {
        if (canPass)
        {
            canPass = false;
            this.GetComponent<EdgeCollider2D>().isTrigger = false;
            this.GetComponentInChildren<SpriteRenderer>().color = new UnityEngine.Color(236, 28, 35, 255);
        }
        else
        {
            canPass = true;
            this.GetComponent<EdgeCollider2D>().isTrigger = true;
            this.GetComponentInChildren<SpriteRenderer>().color = new UnityEngine.Color(90,241,0,255);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canPass && collision.CompareTag("Player"))
        {
            ToggleColliderColorandState();
        }
    }
}
