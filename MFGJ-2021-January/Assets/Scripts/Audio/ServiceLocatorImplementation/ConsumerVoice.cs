using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerVoice : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ServiceLocator.Instance.GetService<IVoiceCommands>().PlayVoiceCommand("Test");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ServiceLocator.Instance.GetService<IVoiceCommands>().PlayVoiceCommand("Morrocoyas");
    }
}
