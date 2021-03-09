using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ISaveable
{
    #region Variables
    LevelManager levelManager;
    Boss boss;

    public Animator animPlayer;

    public GameObject deathPrefab;
    Animation hitAnimation;
    float timeBtwShots;
    public GameObject drop1;
    public GameObject drop2;
    public float drop1PercentChance;
    public float drop2PercentChance;
    private Vector3 directionFacing;

    Transform player;
    [Space]
    public int enemyId;
    public int healthPoints = 100;
    public float moveSpeed;
    public bool isDead = false;

    public float stoppingDistance;
    public float retreatDistance;
    public float detectionRadius;

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
        levelManager = FindObjectOfType<LevelManager>();
        boss = FindObjectOfType<Boss>();
        hitAnimation = GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (this.gameObject.tag == "InfantryEnemy" || this.gameObject.tag == "MachinegunEnemy" || this.gameObject.tag == "Hut")
        {
            levelManager.e_idSetter += 1;
            enemyId = levelManager.e_idSetter;
            levelManager._enemies.Add(this);
        }

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
        if (!levelManager.IsGameOver && Time.timeScale != 0)
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
                isDead = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else player = null;
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
            levelManager.score += 100;
        }
        else if (this.gameObject.CompareTag("MachinegunEnemy"))
        {
            audioManager.PlaySound("EnemyMachineGunnerDeath");
            levelManager.score += 300;
        }
        else if (this.gameObject.CompareTag("Hut"))
        {
            audioManager.PlaySound("DestroyHut");
            levelManager.score += 800;
        }
        levelManager._destroyedEnemies.Add(this.enemyId); //Add "Destroyed" Enemy to Data.
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
            if (this.gameObject.CompareTag("Hut"))
            {
                Instantiate(drop1, this.transform.position + new Vector3(3f, 1.5f, 0), this.transform.rotation);

            }
            else Instantiate(drop1, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation);
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
        enemyData.e_isDead = isDead;
        enemyData.e_id = enemyId;
        enemyData.e_position = this.transform.position;
        a_SaveData.m_EnemyData.Add(enemyData);
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (SaveData.EnemyData enemyData in a_SaveData.m_EnemyData)
        {
            if (enemyData.e_id == enemyId)
            {
                this.transform.position = enemyData.e_position;
                isDead = enemyData.e_isDead;
                break;
            }
        }
        if (isDead == true)
        {
            this.gameObject.SetActive(false);
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
        }
    }
    #endregion
}
