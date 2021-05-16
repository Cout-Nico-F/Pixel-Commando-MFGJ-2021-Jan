using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleting : MonoBehaviour
{
    public float lifeTime = 0.3f;
    public int damage = 10;
    public int damageToBoss = 2;

    private void Start()
    {
        Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.name == "Rocket_Blue(Clone)")
        {
            //Soldiers
            if (collision.gameObject.CompareTag("InfantryEnemy") || 
                collision.gameObject.CompareTag("MachinegunEnemy") || 
                collision.gameObject.CompareTag("Hut") ||
                collision.gameObject.CompareTag("Boss"))
            {
                Destroy(this.gameObject);
            }
        }

    }
}
