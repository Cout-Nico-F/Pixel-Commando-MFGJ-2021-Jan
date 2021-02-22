using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform RespawnPosition;
    private LevelManager lm;

    private void Awake()
    {
        lm = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lm.Checkpoint = RespawnPosition;

            Destroy(this.gameObject);
        }
    }
}
