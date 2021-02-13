using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable
{
    #region Variables
    GameManager gameManager;
    AudioManager audioManager;
    Healing healing;

    // Stats
    [Header("Stats")]
    float moveSpeed;
    public float normalSpeed = 5f;
    public float runSpeed = 10f;
    public int healthPoints;
    public int lives;
    public int maxHealthPoints = 500;
    public HealthBar healthBar;
    public GameObject deathPrefab;
    //guns
    public Gunning gunning;
    public GameObject currentGun;
    [Header("Guns")]
    public GameObject starterPistol;
    public GameObject pistolB;


    [Header("Other variables")]
    public Rigidbody2D rb;
    Vector2 moveDirection;

    public Animator animPlayer;

    [HideInInspector]
    public bool isFacingRight = true;
    [HideInInspector]
    public GameObject deadPlayerRef;
    [HideInInspector]
    public bool isFacingLeft, isFacingUp, isFacingDown = false;
    
    public bool isRunning = false;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        gunning = GetComponentInChildren<Gunning>();
        healing = FindObjectOfType<Healing>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = maxHealthPoints;
        healthBar.SetHealth(healthPoints, maxHealthPoints);
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver && Time.timeScale != 0)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            animPlayer.SetFloat("Horizontal", difference.x);
            animPlayer.SetFloat("Vertical", difference.y);
            animPlayer.SetFloat("Speed", moveDirection.sqrMagnitude);

            UpdateDirection(rotZ);
        }
        if (healthPoints <= 0)
        {
            Die();
            audioManager.MusicChangerLevels("Die");
            audioManager.PlaySound("PlayerDeath");
        }
        gameManager.lastRocketsAmmo = gunning.rocketsAmmo;
        gameManager.lastJavelinAmmo = gunning.javelinAmmo;
        gameManager.lastSelectedSpecial = gunning.selectedSpecial;
    }
    void FixedUpdate()
    {
        CharacterRun();
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Damage":
                healthPoints -= collision.GetComponent<Bulleting>().damage;
                healthBar.SetHealth(healthPoints, maxHealthPoints);
                break;
            case "Heal":
                healthPoints += collision.GetComponent<Healing>().amount;
                if (healthPoints >= maxHealthPoints) { healthPoints = maxHealthPoints;}
                healthBar.SetHealth(healthPoints, maxHealthPoints);

                collision.gameObject.SetActive(false);
                gameManager._grabbedRecollectables.Add(healing.itemsId); //Add "Destroyed" Item to Data.

                audioManager.PlayHealingSound("Heal"); 
                break;
            case "Gun":
                collision.gameObject.SetActive(false);
                gameManager._grabbedRecollectables.Add(healing.itemsId); //Add "Grabbed" Weapons to Data.
                GunSwap(collision.GetComponent<Healing>().prefab, currentGun);
                break;
                //Special Ammo pickup is managed on Gunning script.
            default:
                break;
        }
    }
    #endregion

    #region Player Methods
    public void LoadBasicGun()
    {
        GunSwap(starterPistol, currentGun);
    }
    /// <summary>
    /// Replaces oldGun with newGun and sets the gunning.cs variables.
    /// </summary>
    /// <param name="newGun"> The gun you want to give to the player</param>
    /// <param name="oldGun"> The gun the player had before swaping</param>
    private void GunSwap(GameObject newGun, GameObject oldGun)
    {
        var position = currentGun.transform.position;
        var rotation = currentGun.transform.rotation;

        gameManager.lastSelectedSpecial = gunning.selectedSpecial;

        Destroy(oldGun.gameObject);
        currentGun = Instantiate(newGun, position, rotation) as GameObject;
        currentGun.transform.parent = this.transform;
        this.GetComponentInChildren<Gunning>().shotPoint = currentGun.transform;
        gunning = FindObjectOfType<Gunning>();
        gunning.rocketsAmmo = gameManager.lastRocketsAmmo;
        gunning.javelinAmmo = gameManager.lastJavelinAmmo;
        gunning.selectedSpecial = gameManager.lastSelectedSpecial;
    }
    private void Die()
    {
        lives--;

        if (gameManager.score > 10000)
        {
            gameManager.score -= gameManager.score / 5; //dead penalty
        }
        else
        gameManager.score -= gameManager.score / 3 ; //dead penalty

        gameManager.lastLives = lives;
        gameManager.lastJavelinAmmo = gunning.javelinAmmo;
        gameManager.lastRocketsAmmo = gunning.rocketsAmmo;
        gameManager.lastSelectedSpecial = gunning.selectedSpecial;

        this.gameObject.SetActive(false);
        healthBar.SetHealth(100, 100); // this line is needed to update the healthbar UI when respawn.

        deadPlayerRef = Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
        deadPlayerRef.tag = "Untagged"; //To keep enemies from detecting deadPlayer like as player
        Destroy(deadPlayerRef, 0.028f);
    }
    public void UpdateDirection(float rotZ)
    {
        if (-45 < rotZ && rotZ < 45)
        {
            isFacingRight = true;
            isFacingLeft = false;
            isFacingUp = false;
            isFacingDown = false;
        }
        else if (rotZ < -135 || rotZ > 136)
        {
            isFacingRight = false;
            isFacingLeft = true;
            isFacingUp = false;
            isFacingDown = false;
        }
        else if (46 < rotZ && rotZ < 135)
        {
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = true;
            isFacingDown = false;
        }
        else if (-136 < rotZ && rotZ < -46)
        {
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = false;
            isFacingDown = true;
        }
    }
    void CharacterRun()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }
        else
        {
            isRunning = false;
            moveSpeed = normalSpeed;
        }

    }
    #endregion

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        //Player Data
        SaveData.PlayerData playerData = new SaveData.PlayerData();
        playerData.p_lives = lives;
        playerData.p_health = healthPoints;
        playerData.p_position = FindObjectOfType<GameManager>().Checkpoint.position;
        a_SaveData.m_PlayerData = playerData;
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        //Player Data        
        lives = a_SaveData.m_PlayerData.p_lives;
        healthPoints = a_SaveData.m_PlayerData.p_health;
        transform.position = a_SaveData.m_PlayerData.p_position;
    }
    #endregion
}
