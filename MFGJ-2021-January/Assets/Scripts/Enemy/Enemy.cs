using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathPrefab;
    Animation hitAnimation;
    public int healthPoints = 100;

    float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject enemyBullet;
    Transform player;

    public float moveSpeed;
    public float stoppingDistance;
    public float retreatDistance;
    public float detectionRadius;
    public float shootRange;

    AudioManager audioManager;

    private void Awake()
    {

        hitAnimation = GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
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
        bool playerInRange = Vector2.Distance(transform.position, player.position) < shootRange;

        if (gameObject.CompareTag("InfantryEnemy"))
        {
            MoveEnemy();
        }

        if (timeBtwShots <= 0 && playerInRange)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


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

    private void MoveEnemy()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) < detectionRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }
    }
}
