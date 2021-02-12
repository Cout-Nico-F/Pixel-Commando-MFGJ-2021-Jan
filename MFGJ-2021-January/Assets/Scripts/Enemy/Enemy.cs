using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ISaveable
{
    #region Variables
    GameManager gameManager;
    Boss boss;

    public Animator animPlayer;

    public GameObject deathPrefab;
    Animation hitAnimation;
    float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject enemyBullet;
    public GameObject drop1;
    public GameObject drop2;
    public float drop1PercentChance;
    public float drop2PercentChance;
    private Vector3 directionFacing;
    public Transform shotpoint;

    Transform player;
    [Space]
    public int enemyId;
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
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        boss = FindObjectOfType<Boss>();
        hitAnimation = GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (this.gameObject.tag == "InfantryEnemy" || this.gameObject.tag == "MachinegunEnemy")
        {
            gameManager.e_idSetter +=1;
            enemyId = gameManager.e_idSetter;
            gameManager._enemies.Add(this);
        }

        timeBtwShots = startTimeBtwShots;
        waitTime = startWaitTime;
        randomStep = new Vector3(Random.Range(-randomStepSize, randomStepSize), Random.Range(-randomStepSize, randomStepSize), 0);
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
        //Soldiers Damage
        if (collision.CompareTag("Bullet") || collision.CompareTag("Explosion"))
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


            if (gameObject.CompareTag("InfantryEnemy"))
            {
                MoveEnemy();
                Patrol();
            }

            if (healthPoints > 0 && player != null)
            {
                UpdateAnimator();
            }

            if (healthPoints <= 0)
            {
                Die();
            }
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else player = null;
    }
    private void FixedUpdate()
    {
        TryShoot();
    }
    #endregion

    #region Enemy States
    private void Patrol()
    {
        if (patrolling)
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
            Instantiate(enemyBullet, shotpoint.position, shotpoint.rotation);
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
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        Instantiate(deathPrefab, this.transform.position, this.transform.rotation);

        DropRoll();

        if (this.gameObject.CompareTag("InfantryEnemy"))
        {
            audioManager.PlaySound("EnemySoldierDeath");
            gameManager.score += 100;
        }
        else if (this.gameObject.CompareTag("MachinegunEnemy"))
        {
            audioManager.PlaySound("EnemyMachineGunnerDeath");
            gameManager.score += 300;
        }
        else if (this.gameObject.CompareTag("Hut"))
        {
            audioManager.PlaySound("DestroyHut");
            gameManager.score += 800;
        }
        gameManager._destroyedEnemies.Add(this.enemyId); //Add "Destroyed" Enemy to Data.
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
    private void UpdateAnimator()
    {
        if (gameObject.CompareTag("InfantryEnemy") && patrolling)
        {
            directionFacing = (patrolTarget - transform.position).normalized;
        }
        else
        {
            directionFacing = (player.position - transform.position).normalized;
        }

        if ((gameObject.CompareTag("InfantryEnemy") || gameObject.CompareTag("MachinegunEnemy")) && directionFacing.sqrMagnitude > 0)
        {
            animPlayer.SetFloat("Horizontal", directionFacing.x);
            animPlayer.SetFloat("Vertical", directionFacing.y);
            animPlayer.SetFloat("Speed", directionFacing.sqrMagnitude);
        }
    }
    #endregion

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.EnemyData enemyData = new SaveData.EnemyData();
        enemyData.e_health = healthPoints;
        enemyData.e_id = enemyId;
        a_SaveData.m_EnemyData.Add(enemyData);
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (SaveData.EnemyData enemyData in a_SaveData.m_EnemyData)
        {
            if (enemyData.e_id == enemyId)
            {
                healthPoints = enemyData.e_health;
                break;
            }
        }
        if (healthPoints <= 0)
        {
            Die();
        }
    }
    #endregion
}
