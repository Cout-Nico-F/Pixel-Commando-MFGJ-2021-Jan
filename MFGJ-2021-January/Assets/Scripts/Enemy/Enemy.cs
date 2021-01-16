using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathPrefab;
    Animation hitAnimation;
    public int healthPoits = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            healthPoits -= collision.GetComponent<Bulleting>().damage;
            hitAnimation.Play();
        }
    }

    private void Awake()
    {
        hitAnimation = GetComponent<Animation>();
    }

    private void Update()
    {
        if (healthPoits <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
        }
    }
}
