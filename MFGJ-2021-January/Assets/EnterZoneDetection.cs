using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZoneDetection : MonoBehaviour
{
    BossZoneColliders bzc;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bzc = GetComponentInParent<BossZoneColliders>();
            bzc.ToggleColliderColorandState();

            Destroy(this.gameObject);
        }
    }
}
