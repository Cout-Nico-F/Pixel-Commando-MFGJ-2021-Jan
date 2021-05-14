using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawnTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animVehicleSpawner;
    Enemy enemy;
    bool firstTime = true;
    

    private void Awake() {
        animVehicleSpawner = transform.parent.GetComponentInChildren<Animator>();
        enemy = transform.parent.GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.IsDead && firstTime)
        {
            Debug.Log("Child enemy is dead, disabling gameobject.");
            animVehicleSpawner.SetBool("anim_isDead", true);
            animVehicleSpawner.enabled = false;
            firstTime = false;
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
