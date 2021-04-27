using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> m_VoiceCommands;
    [SerializeField] AudioSource m_VoiceCommandsAudioSource;

    [Range(0, 0.5f)]
    [SerializeField] float m_DialogueVolume;
    [SerializeField] AudioMixerGroup m_VoiceCommandsMixerGroup;


    public static VoiceManager vcMngrInstance;

    private void Awake()
    {
        if (vcMngrInstance == null)
        {
            vcMngrInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayVoiceCommand(string audioClip)
    {
        m_VoiceCommandsAudioSource.volume = m_DialogueVolume;
        switch (audioClip)
        {
            case "Brief":
                m_VoiceCommandsAudioSource.clip = m_VoiceCommands[0];
                break;
            case "SurroundedByEnemies":
                if (m_VoiceCommandsAudioSource.isPlaying)
                {
                    m_VoiceCommandsAudioSource.Stop();
                }
                m_VoiceCommandsAudioSource.clip = m_VoiceCommands[1];
                break;
            case "DestroyHuts":
                m_VoiceCommandsAudioSource.clip = m_VoiceCommands[2];
                break;
            case "ShootFence":
                m_VoiceCommandsAudioSource.clip = m_VoiceCommands[3];
                break;
            case "WireCutters":
                m_VoiceCommandsAudioSource.clip = m_VoiceCommands[4];
                break;
            case "MCdead":
                m_VoiceCommandsAudioSource.Stop();
                m_VoiceCommandsAudioSource.clip = null;
                break;
        }
        m_VoiceCommandsAudioSource.Play();
    }
}
