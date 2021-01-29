using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VoiceCommandTrigger : MonoBehaviour
{
    AudioManager audioManager;

    public VoiceCommands voiceCommands;

    public bool playerHasEntered;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        playerHasEntered = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Entered");
            switch (voiceCommands)
            {
                case VoiceCommands.SurroundedByEnemies:
                    if (playerHasEntered == false)
                    {
                        audioManager.PlayVoiceCommand("SurroundedByEnemies");
                        playerHasEntered = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case VoiceCommands.DestroyHuts:
                    if (playerHasEntered == false)
                    {
                        audioManager.PlayVoiceCommand("DestroyHuts");
                        playerHasEntered = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case VoiceCommands.ShootFence:
                    if (playerHasEntered == false)
                    {
                        audioManager.PlayVoiceCommand("ShootFence");
                        playerHasEntered = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case VoiceCommands.None:
                    break;

            }
        }
        else
        {
            return;
        }
    }


}
