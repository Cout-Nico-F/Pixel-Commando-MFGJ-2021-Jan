﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    public float blockProbability;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Sandbag collision detected:");

        if (collision.CompareTag("Bullet"))
        {
            float result = Random.value;
            Debug.Log("Sandbag block num:" + result);
            if (result < blockProbability)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
