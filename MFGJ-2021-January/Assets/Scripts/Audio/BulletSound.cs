using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        audioManager.PlaySound("BulletSound");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InfantryEnemy"))
        {
            audioManager.PlaySound("HitSoldier");
        }
        else if (collision.CompareTag("MachinegunEnemy"))
        {
            audioManager.PlaySound("HitMachineGunner");
        }
        else if (collision.CompareTag("SandBagEnemy"))
        {
            audioManager.PlaySound("HitSandbag");
        }

    }


}
