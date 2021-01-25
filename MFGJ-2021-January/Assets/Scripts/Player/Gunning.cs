using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunning : MonoBehaviour
{
    GameManager gameManager;
    public float offset;

    public GameObject bulletPrefab;
    public GameObject specialPrefab;

    public int specialAmmo = 0;
    public float bulletForce = 20f;
    public float specialForce = 800f;
    public Transform shotPoint;

    float timeBtwShots;
    float specialCooldown;

    public float startTimeBtwShots;
    public float startSpecialCooldown;

    public PlayerController playerController;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        specialCooldown = startSpecialCooldown;
    }
    void Update()
    {
        if (!gameManager.IsGameOver && Time.timeScale != 0)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            UpdateShotPoint();
            shotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            LeftClickListener();
            RightClickListener();
        }
    }
    private void FixedUpdate()
    {
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (specialCooldown > 0)
        {
            specialCooldown -= Time.deltaTime;
        }
    }

    private void RightClickListener()
    {
        if (specialCooldown <= 0 &&  Input.GetMouseButtonDown(1))
        {
            SpecialShoot();
            specialCooldown = startSpecialCooldown;
        }
    }
    private void LeftClickListener()
    {
        if (timeBtwShots <= 0 && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode2D.Impulse);
    }
    public void SpecialShoot()
    {
        if (specialAmmo >= 1)
        {
            GameObject special = Instantiate(specialPrefab, shotPoint.position, shotPoint.rotation);
            Rigidbody2D rb = special.GetComponent<Rigidbody2D>();
            rb.AddForce(shotPoint.up * specialForce, ForceMode2D.Force); //for a bazooka rocket propeller.
            specialAmmo--;
        }
    }


    public void UpdateShotPoint()
    {
        if (playerController.isFacingRight)
        {
            if (shotPoint.localPosition.x < 0)
            {
                Vector3 pos = shotPoint.localPosition;
                pos.x *= -1;
                shotPoint.localPosition = pos;
            }
            if (shotPoint.localScale.x < 0)
            {
                Vector3 scale = shotPoint.localScale;
                scale.x *= -1;
                shotPoint.localScale = scale;
            }
        }
        else if (playerController.isFacingLeft)
        {
            if (shotPoint.localPosition.x > 0)
            {
                Vector3 pos = shotPoint.localPosition;
                pos.x *= -1;
                shotPoint.localPosition = pos;
            }
            if (shotPoint.localScale.x > 0)
            {
                Vector3 scale = shotPoint.localScale;
                scale.x *= -1;
                shotPoint.localScale = scale;
            }
        }
    }

}
