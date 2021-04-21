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
        
    }

    void Update()
    {
        
    }

    void Attack()
    {
        //If player is close
        if (Vector2.Distance(playerScript.gameObject.transform.position, tankScript.rb.position) <= tankScript.attackminRange) 
        {
            //ATTACK

            //On Tank Bullet Collision -> Create a Bullet Tank Script
            MakeDamage(tankScript.damage);
        }
    }

    int MakeDamage(int damage)
    {
        return playerScript.healthPoints - damage;
    }


}
