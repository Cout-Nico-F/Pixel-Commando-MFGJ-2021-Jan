using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ISaveable
{
    #region Variables
    LevelManager levelManager;

    public GameObject deathPrefab;
    Animation hitAnimation;
    public GameObject drop1;
    public GameObject drop2;
    public float drop1PercentChance;
    public float drop2PercentChance;

    [Space]
    public int enemyId;
    public int healthPoints = 100;

    private bool isDead = false;
    [SerializeField]
    private int regenRate;
    [SerializeField]
    private bool regeneratesHP;

    private int maxHealthpoints;

    [SerializeField]
    private bool doesPatrol;

    AudioManager audioManager;

    public LevelManager LevelManager { get => levelManager; }
    public bool IsDead { get => isDead; }
    public bool DoesPatrol { get => doesPatrol; }
    private int repeat = 0;

    private GameObject bulletHitEffect;
    [SerializeField]
    private GameObject customBulletHitEffect;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {

        levelManager = FindObjectOfType<LevelManager>();
        hitAnimation = GetComponent<Animation>();
        if (this.CompareTag("alwaysLoaded") == false) //avoid disabling fences and watchtowers until i find a way to handle objects with more than one component of the same.
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        //if (this.gameObject.tag == "InfantryEnemy" || this.gameObject.tag == "MachinegunEnemy" || this.gameObject.tag == "Hut")
        //{
        //    levelManager.e_idSetter += 1;
        //    enemyId = levelManager.e_idSetter;
        //    levelManager._enemies.Add(this);
        //}

        try
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        catch
        {
            Debug.Log("no AudioManager in the Scene");
        }
        maxHealthpoints = healthPoints;

        if (customBulletHitEffect)
        {
            bulletHitEffect = customBulletHitEffect;
        }
        else
        {
            bulletHitEffect = Resources.Load("BulletHit_Red") as GameObject;
        }
    }

    private void Update()
    {
        if (regeneratesHP && healthPoints < maxHealthpoints)
        {
            healthPoints += (int)(regenRate * Time.deltaTime);
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
                 Quaternion bulletRotation = Quaternion.Euler(0f, 0f, collision.transform.eulerAngles.z - 270);

                GameObject _bulletHitEffect = GameObject.Instantiate(bulletHitEffect, collision.transform.position, bulletRotation) as GameObject;
                Destroy(_bulletHitEffect, 0.5f);
            }
        }
    }
    private void LateUpdate()
    {
        if (!levelManager.IsGameOver && Time.timeScale != 0)
        {
            if (healthPoints <= 0)
            {
                Die();
            }
        }
        if (repeat == 0)
        {
            if (this.gameObject.tag == "InfantryEnemy" || this.gameObject.tag == "MachinegunEnemy" || this.gameObject.tag == "Hut")
            {
                levelManager.e_idSetter += 1;
                enemyId = levelManager.e_idSetter;
                levelManager._enemies.Add(this);
            }
            repeat++;
        }

    }

    #endregion

    #region Enemy States
    
    
    public void Die()
    {
        isDead = true;

        this.gameObject.SetActive(false);

        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
        }
        else Debug.Log("No death prefab here");

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
