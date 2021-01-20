using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathPrefab;
    Animation hitAnimation;
    public int healthPoints = 100;

    AudioManager audioManager;

    private void Awake()
    {

        hitAnimation = GetComponent<Animation>();
        try
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        catch
        {
            Debug.Log("no AudioManager in the Scene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            healthPoints -= collision.GetComponent<Bulleting>().damage;
            hitAnimation.Play();
        }
    }



    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
            if (this.gameObject.CompareTag("InfantryEnemy"))
            {
                audioManager.PlaySound("EnemySoldierDeath");
            }
            else if (this.gameObject.CompareTag("MachinegunEnemy"))
            {
                audioManager.PlaySound("EnemyMachineGunnerDeath");
            }
        }
    }
}
