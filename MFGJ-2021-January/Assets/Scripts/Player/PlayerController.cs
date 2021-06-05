using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ISaveable
{
    #region Variables
    LevelManager levelManager;
    AudioManager audioManager;
    Animation hitAnimation;
    BorderFlasher borderFlasher;

    // UI
    UI_BeltInventory uiBeltInventory;
    UI_Stamina uiStamina;

    // Stats
    [Header("Stats")]
    float moveSpeed;
    public float normalSpeed = 6f;
    public float runSpeed = 17f;
    public int healthPoints;
    public int lives;
    public int maxHealthPoints = 500;
    public HealthBar healthBar;
    public GameObject deathPrefab;

    private float stamina;
    private readonly float maxStamina = 3;
    private bool isrunning;

    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Color lowColor;
    [SerializeField] private Color highColor;
    [SerializeField] private GameObject color;
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
    public bool isFacingLeft = false;

    public float trapTickDuration = 0.5f;
    private float trapEnterTime;
    private CoroutineAux coroutineAux;
    public float MaxStamina { get => maxStamina; }
    public float Stamina { get => stamina; set => stamina = value; }

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        gunning = GetComponentInChildren<Gunning>();
        levelManager = FindObjectOfType<LevelManager>();
        uiBeltInventory = FindObjectOfType<UI_BeltInventory>();
        uiStamina = FindObjectOfType<UI_Stamina>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        hitAnimation = GetComponent<Animation>();
        coroutineAux = FindObjectOfType<CoroutineAux>();
    }
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        healthPoints = maxHealthPoints;      
        healthBar.SetHealth(healthPoints, maxHealthPoints);
        borderFlasher = FindObjectOfType<BorderFlasher>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!levelManager.IsGameOver && Time.timeScale != 0)
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
            StopAudioWhenDead();
            audioManager.MusicChangerLevels("Die");
            audioManager.PlaySound("PlayerDeath");
        }
        levelManager.lastRocketsAmmo = gunning.rocketsAmmo;
        levelManager.lastJavelinAmmo = gunning.javelinAmmo;
        levelManager.lastSelectedSpecial = gunning.selectedSpecial;

        StaminaBar();
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
                if (hitAnimation != null)
                {
                    hitAnimation.Play();
                    borderFlasher.FlashBorder("damage");
                }
                break;
            case "Explosion":
                healthPoints -= 20; //placeholder way to make explosions weaker against player than against enemies.
                healthBar.SetHealth(healthPoints, maxHealthPoints);
                if (hitAnimation != null)
                {
                    hitAnimation.Play();
                    borderFlasher.FlashBorder("damage");
                    borderFlasher.FlashBorder("damage");
                }
                break;
            case "Heal":
                healthPoints += collision.GetComponent<Healing>().amount;
                if (healthPoints >= maxHealthPoints) { healthPoints = maxHealthPoints;}
                healthBar.SetHealth(healthPoints, maxHealthPoints);

                collision.gameObject.SetActive(false);

                audioManager.PlayHealingSound("Heal"); 
                borderFlasher.FlashBorder("heal");
                break;
            case "Gun":
                collision.gameObject.SetActive(false);

                GunSwap(collision.GetComponent<Healing>().prefab, currentGun);
                uiBeltInventory.SetPrimaryWeaponImage(collision.GetComponent<SpriteRenderer>().sprite);
                uiBeltInventory.TriggerPickupAnimation(collision.gameObject);
                audioManager.PlaySound("PickUpWeapon");
                break;
            case "JavelinAmmo":
                break;
                //Special Ammo pickup is managed on Gunning script.
            case "Trap":
                healthPoints -= collision.GetComponent<Trap>().damage;
                healthBar.SetHealth(healthPoints, maxHealthPoints);
                if (hitAnimation != null)
                {
                    hitAnimation.Play();
                    borderFlasher.FlashBorder("damage");
                }
                trapEnterTime = Time.time;
                break;
            case "HealHutLoot":
                healthPoints += collision.GetComponent<Healing>().amount;
                if (healthPoints >= maxHealthPoints) { healthPoints = maxHealthPoints; }
                healthBar.SetHealth(healthPoints, maxHealthPoints);

                coroutineAux.HideObject(20, collision.gameObject);

                audioManager.PlayHealingSound("Heal");
                borderFlasher.FlashBorder("heal");
                break;
            case "GunHutLoot":
                coroutineAux.HideObject(30, collision.gameObject);

                GunSwap(collision.GetComponent<Healing>().prefab, currentGun);
                uiBeltInventory.SetPrimaryWeaponImage(collision.GetComponent<SpriteRenderer>().sprite);
                uiBeltInventory.TriggerPickupAnimation(collision.gameObject);
                audioManager.PlaySound("PickUpWeapon");
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Trap":
                if(Time.time - trapEnterTime > this.trapTickDuration)
                {
                    trapEnterTime = Time.time;
                    healthPoints -= collision.GetComponent<Trap>().damage;
                    healthBar.SetHealth(healthPoints, maxHealthPoints);
                    if (hitAnimation != null)
                    {
                        hitAnimation.Play();
                        borderFlasher.FlashBorder("damage");
                    }
                }
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

        levelManager.lastSelectedSpecial = gunning.selectedSpecial;

        Destroy(oldGun.gameObject);
        currentGun = Instantiate(newGun, position, rotation) as GameObject; 
        currentGun.transform.parent = this.transform;
        gunning = FindObjectOfType<Gunning>();
        gunning.rocketsAmmo = levelManager.lastRocketsAmmo;
        gunning.javelinAmmo = levelManager.lastJavelinAmmo;
        gunning.selectedSpecial = levelManager.lastSelectedSpecial;

    }
    private void Die()
    {
        lives--;

        if (levelManager.score > 10000)
        {
            levelManager.score -= levelManager.score / 5; //dead penalty
        }
        else
        levelManager.score -= levelManager.score / 3 ; //dead penalty

        levelManager.lastLives = lives;
        levelManager.lastJavelinAmmo = gunning.javelinAmmo;
        levelManager.lastRocketsAmmo = gunning.rocketsAmmo;
        levelManager.lastSelectedSpecial = gunning.selectedSpecial;

        this.gameObject.SetActive(false);
        healthBar.SetHealth(healthPoints, maxHealthPoints); // this line is needed to update the healthbar UI when respawn.

        deadPlayerRef = Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
        deadPlayerRef.tag = "Untagged"; //To keep enemies from detecting deadPlayer like as player
        Destroy(deadPlayerRef, 0.028f);
    }
    public void UpdateDirection(float rotZ)
    {
        if (-90 < rotZ && rotZ < 90)
        {
            isFacingRight = true;
            isFacingLeft = false;
        }
        else if (rotZ < -90 || rotZ > 90)
        {
            isFacingRight = false;
            isFacingLeft = true;
        }
    }

    void StopAudioWhenDead()
    {
        audioManager.bombFallingAudioSource.Stop();
        audioManager.helicopterAudioSource.Stop();
        audioManager.PlayVoiceCommand("MCdead");
        audioManager.rocketTrustAudioSource.Stop();
    }
    void CharacterRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            if (stamina > maxStamina /3 || isrunning)
            {
                moveSpeed = runSpeed;
                isrunning = true;
                stamina -= Time.deltaTime;
                if (stamina < 0) stamina = 0;
            }   
        }
        else
        {
            moveSpeed = normalSpeed;
            isrunning = false;
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime; //regenerate stamina
            }
        }
    }

    private void StaminaBar()
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.minValue = 0;
        staminaSlider.value = stamina;
        //color.GetComponent<Image>().color = Color.Lerp(lowColor, highColor, staminaSlider.normalizedValue); why doesnt works?
        
        uiStamina.SetUIStamina(stamina, maxStamina);

        if (staminaSlider.value >= staminaSlider.maxValue )
        {
            staminaSlider.gameObject.SetActive(false);
        }
        else staminaSlider.gameObject.SetActive(true);
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
        playerData.p_position = this.transform.position;
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
