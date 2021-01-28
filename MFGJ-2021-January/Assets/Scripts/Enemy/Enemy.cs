using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;

    public GameObject deathPrefab;
    Animation hitAnimation;
    float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject enemyBullet;
    public GameObject drop1;
    public GameObject drop2;
    public float drop1PercentChance;
    public float drop2PercentChance;


    Transform player;
    [Space]
    public int healthPoints = 100;
    public float moveSpeed;

    public float stoppingDistance;
    public float retreatDistance;
    public float detectionRadius;
    public float shootRange;
    
    //Patrol
    
    [Header("Patrol")]
    public float startWaitTime;
    public float randomStepSize;
    private Vector3 randomStep;
    private Vector3 patrolTarget;
    private bool patrolling = true;
    private float waitTime;

    AudioManager audioManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        hitAnimation = GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        waitTime = startWaitTime;
        randomStep = new Vector3(Random.Range(-randomStepSize, randomStepSize), Random.Range(-randomStepSize, randomStepSize),0);
        patrolTarget = transform.position + randomStep;

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
        if (collision.CompareTag("Bullet") || collision.CompareTag("Explosion") )
        {
            healthPoints -= collision.GetComponent<Bulleting>().damage;
            if (hitAnimation != null)
            {
                hitAnimation.Play();
            }
        }
    }


    private void Update()
    {
        if (!gameManager.IsGameOver && Time.timeScale != 0)
        {
            Patrol();

            if (gameObject.CompareTag("InfantryEnemy"))
            {
                MoveEnemy();
            }

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }

    private void FixedUpdate()
    {
        TryShoot();
    }

    private void Patrol()
    {
        if (gameObject.CompareTag("InfantryEnemy") && patrolling)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolTarget, moveSpeed * Time.deltaTime);

            if (waitTime <= 0)
            {
                randomStep = new Vector3(Random.Range(-randomStepSize, randomStepSize), Random.Range(-randomStepSize, randomStepSize), 0);
                patrolTarget = transform.position + randomStep;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void TryShoot()
    {
        bool playerInRange = false;
        if (player != null)
        {
            playerInRange = Vector2.Distance(transform.position, player.position) < shootRange;
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
    }
    private void MoveEnemy()
    {
        if (player != null)
        {

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) < detectionRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                patrolling = false;
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            }
            else patrolling = true;
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
        Instantiate(deathPrefab, this.transform.position, this.transform.rotation);

        DropRoll();

        if (this.gameObject.CompareTag("InfantryEnemy"))
        {
            audioManager.PlaySound("EnemySoldierDeath");
        }
        else if (this.gameObject.CompareTag("MachinegunEnemy"))
        {
            audioManager.PlaySound("EnemyMachineGunnerDeath");
        }
    }
    private void DropRoll()
    {
        float badLuck = Random.Range(0.1f, 1f);

        if (badLuck <= drop2PercentChance / 100)
        {
            Instantiate(drop2, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation);
            return;
        }
        if (badLuck <= drop1PercentChance / 100) //DROP
        {
            Instantiate(drop1, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation);
        }
    }
}
