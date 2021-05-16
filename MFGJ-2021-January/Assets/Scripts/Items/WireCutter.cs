using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCutter : MonoBehaviour
{
    public PlayerController player;
    private UI_BeltInventory uiBeltInventory;
    private AudioManager m_AudioManager;

    private void Awake()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerController>();
        uiBeltInventory = FindObjectOfType<UI_BeltInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_AudioManager.PlaySound("Wirecutter");
            Specials.HasTools = true;
            uiBeltInventory.EnableUIItem("Wirecutter");
            //pick wirecutter Sound
            Destroy(this.gameObject);
        }
    }
}
