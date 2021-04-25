using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animVehicleSpawner;
    Enemy enemy;
    bool isVehicleDead;
    

    private void Awake() {
        animVehicleSpawner = GetComponentInChildren<Animator>();
        enemy = GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.IsDead)
        {
            Debug.Log("Child enemy is dead, disabling gameobject.");
            animVehicleSpawner.SetBool("anim_isDead", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player entered");
            animVehicleSpawner.SetBool("anim_playerInRange", true);
        }
    }
}
