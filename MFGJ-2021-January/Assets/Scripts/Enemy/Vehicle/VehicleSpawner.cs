using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animVehicleSpawner;

    private void Awake() {
        animVehicleSpawner = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
