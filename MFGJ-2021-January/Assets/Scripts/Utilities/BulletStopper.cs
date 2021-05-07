using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStopper : MonoBehaviour
{
    public GameObject customBulletHitEffect;
    private GameObject bulletHitEffect;

    private void Start()
    {
        if (customBulletHitEffect)
        {
            bulletHitEffect = customBulletHitEffect;
        }
        else
        {
            bulletHitEffect = Resources.Load("BulletHit") as GameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Damage"))
        {
            HitEffect(bulletHitEffect,collision);
        }
    }

    public static void HitEffect(GameObject bulletHitEffect, Collider2D collision)
    {
        if (bulletHitEffect)
        {
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, collision.transform.eulerAngles.z - 270);

            GameObject _bulletHitEffect = GameObject.Instantiate(bulletHitEffect, collision.transform.position, bulletRotation) as GameObject;
            Destroy(_bulletHitEffect, 0.5f);
        }
        Destroy(collision.gameObject, 0.05f);
    }
}
