using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunning : MonoBehaviour, ISaveable
{
    #region Variables
    LevelManager levelManager;
    UI_BeltInventory uiBeltInventory;
    private Camera mainCam;
    public float offset;

    public GameObject bulletPrefab;
    public bool needsAmmo;
    public int initial_Ammo;
    public GameObject rocketPrefab;
    public GameObject javelinPrefab;

    public int rocketsAmmo = 0;
    public int javelinAmmo = 0;

    [HideInInspector]
    public string selectedSpecial = "Rocket";

    public float bulletForce = 20f;
    public float specialForce = 800f;
    [SerializeField]
    private Transform shotPoint;
    [SerializeField]
    private Transform weaponPrefabTransform;

    float nextBulletFire;
    float nextRocketFire;
    float nextJavelinFire;

    [SerializeField] float bulletCooldown = 0.5f;
    [SerializeField] float rocketCooldown = 2f;
    [SerializeField] float javelinCooldown = 1f;
    [SerializeField] float cameraShakeDuration = 0.04f;
    [SerializeField] float cameraShakeAmount = 0.045f;


    public PlayerController playerController;

    private GameObject javelinUI;
    private GameObject rocketsUI;

    private AudioManager m_audioManager;

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        javelinUI = levelManager.javelinUI;
        rocketsUI = levelManager.rocketsUI;
        playerController = FindObjectOfType<PlayerController>();
        uiBeltInventory = FindObjectOfType<UI_BeltInventory>();
        mainCam = Camera.main;
    }
    void Update()
    {
        if (!levelManager.IsGameOver && Time.timeScale != 0)
        {
            Vector3 difference = mainCam.ScreenToWorldPoint(Input.mousePosition) - weaponPrefabTransform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            UpdateWeaponRotation();
            weaponPrefabTransform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            LeftClickListener();
            RightClickListener();

            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                ChangeSpecial();
            }
        }
    }
    private void FixedUpdate()
    {
        UpdateAmmoUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RocketAmmo"))
        {
            rocketsAmmo += collision.GetComponent<Healing>().amount;
            levelManager.lastRocketsAmmo = rocketsAmmo;

            collision.gameObject.SetActive(false);

            m_audioManager.PlaySound("PickUpWeapon");
        }
        if (collision.CompareTag("JavelinAmmo"))
        {
            javelinAmmo += collision.GetComponent<Healing>().amount;
            levelManager.lastJavelinAmmo = javelinAmmo;

            collision.gameObject.SetActive(false);

            m_audioManager.PlaySound("PickUpWeapon");
        }
        //collision with Explosives ammo is managed on Specials.cs
    }


    #endregion

    #region Gunning Methods
    private void UpdateAmmoUI()
    {
        javelinUI.GetComponentInChildren<UnityEngine.UI.Text>().text = javelinAmmo.ToString();
        rocketsUI.GetComponentInChildren<UnityEngine.UI.Text>().text = rocketsAmmo.ToString();
    }
    private void RightClickListener()
    {
        switch (selectedSpecial)
        {
            case "Rocket":
                if (Time.time > nextRocketFire && Input.GetButton("Fire2"))
                {
                    if (rocketsAmmo >= 1)
                    {
                        nextRocketFire = Time.time + rocketCooldown;
                        RocketShoot();
                    }
                }
                break;

            case "Javelin":
                if (Time.time > nextJavelinFire && Input.GetButton("Fire2"))
                {
                    if (javelinAmmo >= 1)
                    {
                        nextJavelinFire = Time.time + javelinCooldown;
                        JavelinShoot();
                    }
                }
                break;
            default:
                Debug.LogError("Gunning.cs Line 114: Switch case not exists");
                break;
        }
    }
    private void LeftClickListener()
    {
        if (Time.time > nextBulletFire && Input.GetButton("Fire1"))
        {
            nextBulletFire = Time.time + bulletCooldown;

            if (needsAmmo)
            {
                if (initial_Ammo > 0)
                {
                    initial_Ammo--;
                    Shoot();
                }
                else DelegateLoadBasicGun();
            }
            else Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode2D.Impulse);

        // Shake the camera for (duration, amount)
        CameraShake.Shake(cameraShakeDuration, cameraShakeAmount);
    }
    public void RocketShoot()
    {
        GameObject special = Instantiate(rocketPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = special.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * specialForce, ForceMode2D.Force); //for a bazooka rocket propeller.
        rocketsAmmo--;
    }
    public void JavelinShoot()
    {
        if (javelinAmmo >= 1)
        {
            GameObject special = Instantiate(javelinPrefab, shotPoint.position, shotPoint.rotation);
            Rigidbody2D rb = special.GetComponent<Rigidbody2D>();
            rb.AddForce(shotPoint.up * specialForce, ForceMode2D.Force);
            javelinAmmo--;
        }
    }
    private void ChangeSpecial()
    {
        //when change button was pressed:
        switch (selectedSpecial)
        {
            case "Rocket":
                selectedSpecial = "Javelin";
                
                uiBeltInventory.SwapLaunchables();
                //javelinUI.SetActive(true);
                //rocketsUI.SetActive(false);

                break;
            case "Javelin":
                selectedSpecial = "Rocket";

                uiBeltInventory.SwapLaunchables();
                //javelinUI.SetActive(false);
                //rocketsUI.SetActive(true);

                break;
            default:
                Debug.LogError("Gunning.cs Line 111: Switch case not exists");
                break;
        }
    }
    private void DelegateLoadBasicGun()
    {
        this.GetComponentInParent<PlayerController>().LoadBasicGun();
    }
    public void UpdateWeaponRotation()
    {
        if (playerController.isFacingRight)
        {
            if (weaponPrefabTransform.localPosition.x < 0)
            {
                Vector3 pos = weaponPrefabTransform.localPosition;
                pos.x *= -1;
                weaponPrefabTransform.localPosition = pos;
            }
            if (weaponPrefabTransform.localScale.x < 0)
            {
                Vector3 scale = weaponPrefabTransform.localScale;
                scale.x *= -1;
                weaponPrefabTransform.localScale = scale;
            }
        }
        else if (playerController.isFacingLeft)
        {
            if (weaponPrefabTransform.localPosition.x > 0)
            {
                Vector3 pos = weaponPrefabTransform.localPosition;
                pos.x *= -1;
                weaponPrefabTransform.localPosition = pos;
            }
            if (weaponPrefabTransform.localScale.x > 0)
            {
                Vector3 scale = weaponPrefabTransform.localScale;
                scale.x *= -1;
                weaponPrefabTransform.localScale = scale;
            }
        }
    }
    #endregion

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        //Ammo Data
        SaveData.AmmoData ammoData = new SaveData.AmmoData();
        ammoData.a_rocketAmmo = rocketsAmmo;
        ammoData.a_javelinAmmo = javelinAmmo;
        ammoData.a_initialAmmo = initial_Ammo;
        a_SaveData.m_AmmoData = ammoData;
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        //Ammo Data
        rocketsAmmo = a_SaveData.m_AmmoData.a_rocketAmmo;
        javelinAmmo = a_SaveData.m_AmmoData.a_javelinAmmo;
    }
    #endregion

}
