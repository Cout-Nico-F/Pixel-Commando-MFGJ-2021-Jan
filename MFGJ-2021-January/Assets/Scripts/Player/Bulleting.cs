using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleting : MonoBehaviour
{
    public float lifeTime = 0.3f;
    public int damage = 10;

    void Start()
    {
        
        Invoke("DestroyBullet", lifeTime);
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (this.gameObject.name == "Rocket_Blue(Clone)")
        {
            if (collision.gameObject.CompareTag("InfantryEnemy") || collision.gameObject.CompareTag("MachinegunEnemy"))
            {
                Debug.Log("BOOOOOM");
                Destroy(this.gameObject);
            }
        }
    }
}
