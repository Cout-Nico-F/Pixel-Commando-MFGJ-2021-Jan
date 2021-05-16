using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private VoiceInstructions m_VoiceInstrucions;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IVoiceCommands>(m_VoiceInstrucions);
    }
}
