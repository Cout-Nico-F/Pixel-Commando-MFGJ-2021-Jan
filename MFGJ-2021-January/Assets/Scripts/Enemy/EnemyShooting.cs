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
        if (player != null & player.CompareTag("Player"))
        {
            playerInRange = Vector2.Distance(transform.position, player.position) < shootRange;
        }
        else return;

        if (timeBtwShots <= 0 && playerInRange)
        {
            Vector3 dir = player.transform.position - shotpoint.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Instantiate(enemyBullet, shotpoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    }
