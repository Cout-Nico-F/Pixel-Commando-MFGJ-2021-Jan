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
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
