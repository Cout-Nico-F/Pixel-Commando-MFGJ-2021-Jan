using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsReferenceForEquip : MonoBehaviour
{
    [SerializeField] private string nameOfGun;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            var gunAndEquipGuns = playerController.GetGunsAndEquips();
            gunAndEquipGuns.EquipNewGun(nameOfGun);
        }
    }
}
