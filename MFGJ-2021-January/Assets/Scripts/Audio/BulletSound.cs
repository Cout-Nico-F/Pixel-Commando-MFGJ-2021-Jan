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
        //MC Bullet sound
        if (this.gameObject.CompareTag("Bullet"))
        {
            audioManager.PlaySound("BulletSound");
        } // Enemies bullet sound
        else if (this.gameObject.CompareTag("Damage"))
        {
            audioManager.PlaySound("Damage");
        }
        else if(this.gameObject.CompareTag("Rocket"))
        {
            Debug.Log("Rock on!");
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(this.gameObject.CompareTag("Bullet")) //MC Bullet
        {
            if (collision.CompareTag("InfantryEnemy")) //hit an enemy
            {
                audioManager.PlaySound("HitSoldier");
            }
            else if (collision.CompareTag("MachinegunEnemy")) //hit machine gunner
            {
                audioManager.PlaySound("HitMachineGunner");
            }
            else if (collision.CompareTag("SandBagEnemy")) // hit sandbag
            {
                audioManager.PlaySound("HitSandbag");
            }
        }
        else if (this.gameObject.CompareTag("Damage")) //Enemy Bullet
        {
            if (collision.CompareTag("Player"))
            {
                audioManager.PlaySound("McHit"); //enemy bullet hits MC
            }
        }
        
    }


}
