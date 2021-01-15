using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthPoits = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            healthPoits -= collision.GetComponent<Bulleting>().damage;
        }
    }

    private void Update()
    {
        if (healthPoits <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
