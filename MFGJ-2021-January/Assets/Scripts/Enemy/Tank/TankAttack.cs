using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] internal Tank tankScript;
    PlayerController playerScript;

    void Awake()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        tankScript.timeBtwShots = tankScript.startTimeBtwShots;
    }

    void FixedUpdate()
    {
        //If player is close
        if (Vector2.Distance(playerScript.gameObject.transform.position, tankScript.rb.position) <= tankScript.attackminRange)
        {
            //ATTACK
            Attack();

            //On Tank Bullet Collision -> Create a Bullet Tank Script
            MakeDamage(tankScript.damage);
        }
    }

    void Attack()
    {
        if (tankScript.timeBtwShots <= 0)
        {
            Vector3 dir = playerScript.transform.position - tankScript.shotpoint.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Instantiate(tankScript.enemyBullet, tankScript.shotpoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
            tankScript.timeBtwShots = tankScript.startTimeBtwShots;
        }
        else
        {
            tankScript.timeBtwShots -= Time.deltaTime;
        }
    }

    int MakeDamage(int damage)
    {
        return playerScript.healthPoints - damage;
    }


}
