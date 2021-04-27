using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VoiceCommandTrigger : MonoBehaviour
{
    [SerializeField] VoiceManager m_VoiceManager;
    HintsManager hintsManager;

    public VoiceCommands voiceCommands;

    public bool playerHasEntered;

    private void Awake()
    {
        if(m_VoiceManager == null)
        {
            m_VoiceManager = FindObjectOfType<VoiceManager>();
        }
        
        hintsManager = FindObjectOfType<HintsManager>();
    }

    private void Start()
    {
        playerHasEntered = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Entered");
            switch (voiceCommands)
            {
                case VoiceCommands.SurroundedByEnemies:
                    if (playerHasEntered == false)
                    {
                        hintsManager.ShowHintPanel("move", 5f);
                        m_VoiceManager.PlayVoiceCommand("SurroundedByEnemies");
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
                        m_VoiceManager.PlayVoiceCommand("DestroyHuts");
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
                        m_VoiceManager.PlayVoiceCommand("ShootFence");
                        playerHasEntered = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case VoiceCommands.WireCutters:
                    if (playerHasEntered == false)
                    {
                        m_VoiceManager.PlayVoiceCommand("WireCutters");
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
