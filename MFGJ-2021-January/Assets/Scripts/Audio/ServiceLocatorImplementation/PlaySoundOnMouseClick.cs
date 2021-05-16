using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnMouseClick : MonoBehaviour
{
    public void PlayVoice()
    {
        ServiceLocator.Instance.GetService<IVoiceCommands>().PlayVoiceCommand("Test");
    }
}
