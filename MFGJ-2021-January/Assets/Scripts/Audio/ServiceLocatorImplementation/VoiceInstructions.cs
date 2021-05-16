using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceInstructions : MonoBehaviour, IVoiceCommands
{
    [SerializeField] private AudioClip m_Test;
    [SerializeField] private AudioSource m_As;

    public void PlayVoiceCommand(string command)
    {
        m_As.PlayOneShot(m_Test);
    }
}
