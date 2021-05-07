using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverseWeaponsSound : MonoBehaviour
{

    AudioManager audioManager;
    public WeaponSounds weaponSounds = new WeaponSounds(); 

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        switch (weaponSounds)
        {
            case WeaponSounds.EnemyBulletSound:
                audioManager.PlaySound("Damage");
                break;
            case WeaponSounds.McBulletSound:
                audioManager.PlaySound("BulletSound");
            break;
            case WeaponSounds.RocketSounds:
                audioManager.PlaySound("RocketFire");
                audioManager.PlaySound("RocketTrust");
                break;
            case WeaponSounds.SpearSounds:
                audioManager.PlaySound("TrowSpear");
                break;
            case WeaponSounds.RapidFireSound:
                audioManager.PlaySound("RapidFire");
                break;
            case WeaponSounds.Bomb:
                audioManager.PlaySound("FallingBomb");
                    break;
            case WeaponSounds.BossLvl1Explode:
                audioManager.PlaySound("BossExplode");
                break;
            case WeaponSounds.Skar:
                audioManager.PlaySound("Skar");
                break;
            
        }      
    }

    private void OnDestroy()
    {
        switch (weaponSounds)
        {
            case WeaponSounds.RocketSounds:
                audioManager.PlaySound("RocketExplossion");
                break;
            case WeaponSounds.Bomb:
                audioManager.PlaySound("BombExplossion");
                break;
            default:
                return;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (this.gameObject.CompareTag("Bullet")) //MC Bullet
        {
            if (collision.CompareTag("InfantryEnemy")) //hit an enemy
            {
                if(this.gameObject.name == "Spear(Clone)")
                {
                    audioManager.PlaySound("Splat");
                }

                audioManager.PlaySound("HitSoldier");
            }
            else if (collision.CompareTag("MachinegunEnemy")) //hit machine gunner
            {
                audioManager.PlaySound("HitMachineGunner");
            }
            else if (collision.CompareTag("SandBagEnemy")) // hit sandbag
            {
                //audioManager.PlaySound("HitSandbag");
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
