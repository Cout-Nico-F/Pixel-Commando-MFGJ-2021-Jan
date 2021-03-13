using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float shootRange;
    float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject enemyBullet;
    public Transform shotpoint;
    Transform player;

    private void Awake()
    {
        timeBtwShots = startTimeBtwShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        TryShoot();
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
            if ( ! this.CompareTag("Hut"))
            {
                RotateToPlayerDir();
            }
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void RotateToPlayerDir()
    {
        Vector3 shootingDirection = new Vector2(player.position.x, player.position.y);

        float angle = Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg;
        shootingDirection.Normalize();
        enemyBullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * 100.0f;
        shotpoint.GetComponent<Transform>().Rotate(0.0f, 0.0f, angle);
    }
}
