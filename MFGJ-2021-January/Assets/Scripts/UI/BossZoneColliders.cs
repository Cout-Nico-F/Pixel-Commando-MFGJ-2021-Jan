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
            this.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        else
        {
            canPass = true;
            this.GetComponent<EdgeCollider2D>().isTrigger = true;
            this.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }
}