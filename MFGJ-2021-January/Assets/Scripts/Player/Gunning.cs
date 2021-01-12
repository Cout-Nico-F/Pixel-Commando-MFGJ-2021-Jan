using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunning : MonoBehaviour
{
    public float offset;

   
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public Transform shotPoint;

    

    float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

          
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode2D.Impulse);
    }

  
}
