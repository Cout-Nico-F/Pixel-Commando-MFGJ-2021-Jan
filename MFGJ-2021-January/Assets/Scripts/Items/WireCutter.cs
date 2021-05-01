using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCutter : MonoBehaviour
{
    public PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Specials.HasTools = true;
            //pickBombsSound
            Destroy(this.gameObject);
        }
    }
}
