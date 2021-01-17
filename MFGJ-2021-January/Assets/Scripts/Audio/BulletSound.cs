using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    AudioManager aM;

<<<<<<< HEAD
    public int enemyHealth;

=======
>>>>>>> main
    private void Awake()
    {
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
<<<<<<< HEAD
        //aM.PlayBulletSound();
        aM.PlaySound("BulletSound");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InfantryEnemy"))
        {
            aM.PlaySound("HitSoldier");
        }
        else if (collision.CompareTag("MachinegunEnemy"))
        {
            aM.PlaySound("HitMachineGunner");
        }
        else if (collision.CompareTag("SandBagEnemy"))
            {
                aM.PlaySound("HitSandbag");
            }
    
    }
    
        

} 
=======
        aM.PlayBulletSound();
    }

}
>>>>>>> main
