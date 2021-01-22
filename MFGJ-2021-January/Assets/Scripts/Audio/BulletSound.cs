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
        if (this.gameObject.CompareTag("Bullet"))
        {
            audioManager.PlaySound("BulletSound");
        }
        else if (this.gameObject.CompareTag("Damage"))
        {
            audioManager.PlaySound("Damage");
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(this.gameObject.CompareTag("Bullet"))
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
        else if (this.gameObject.CompareTag("Damage"))
        {
            if (collision.CompareTag("Player"))
            {
                audioManager.PlaySound("McHit");
            }
        }
        
    }


}
