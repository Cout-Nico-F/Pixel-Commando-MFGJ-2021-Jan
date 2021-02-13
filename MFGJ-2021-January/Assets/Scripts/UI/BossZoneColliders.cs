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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canPass && collision.CompareTag("Player"))
        {
            ToggleColliderColorandState();
        }
    }
}

    //bool canPass_col1 = false;
    //bool canPass_col2 = false;

    //public GameObject collider1;
    //public GameObject collider2;

    //public enum ZoneColliders
    //{
    //    first,
    //    second
    //}

    //public void ToggleCollider(ZoneColliders x)
    //{
    //    switch (x)
    //    {
    //        case ZoneColliders.first:
    //            if (canPass_col1)
    //            {
    //                canPass_col1 = false;
    //                collider1.GetComponent<EdgeCollider2D>().isTrigger = false;
    //                collider1.GetComponentInChildren<SpriteRenderer>().color = Color.red;//new UnityEngine.Color(236, 28, 35, 255);
    //            }
    //            else
    //            {
    //                canPass_col1 = true;
    //                collider1.GetComponent<EdgeCollider2D>().isTrigger = true;
    //                collider1.GetComponentInChildren<SpriteRenderer>().color = Color.green;//new UnityEngine.Color(90, 241, 0, 255);
    //            }
    //            break;

    //        case ZoneColliders.second:
    //            if (canPass_col2)
    //            {
    //                canPass_col2 = false;
    //                collider2.GetComponent<EdgeCollider2D>().isTrigger = false;
    //                collider2.GetComponentInChildren<SpriteRenderer>().color = Color.red; //new UnityEngine.Color(236, 28, 35, 255);
    //            }
    //            else
    //            {
    //                canPass_col2 = true;
    //                collider2.GetComponent<EdgeCollider2D>().isTrigger = true;
    //                collider2.GetComponentInChildren<SpriteRenderer>().color = Color.green; //new UnityEngine.Color(90, 241, 0, 255);
    //            }
    //            break;

    //        default:
    //            break;
    //    }       
    //}

