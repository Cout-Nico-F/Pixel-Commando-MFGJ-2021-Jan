﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehaviour : MonoBehaviour
{
    public PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.hasTools)
        {
            //UI Press F to cut the wire fence
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.GetComponentInParent<Enemy>().healthPoints -= 500;
                //play a sound.
            }
        }
    }
}