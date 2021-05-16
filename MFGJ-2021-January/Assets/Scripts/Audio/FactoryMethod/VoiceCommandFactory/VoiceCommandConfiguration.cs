using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ivan/VoiceCommandConfiguration")]
public class VoiceCommandConfiguration : ScriptableObject
{
    [SerializeField] private VoiceCommnad[] voiceCommands;
    private Dictionary<string, VoiceCommnad> idToVoiceCommand;

    private void Awake()
    {
        idToVoiceCommand = new Dictionary<string, VoiceCommnad>(voiceCommands.Length);
        foreach (var voiceCommnad in voiceCommands)
        {
            idToVoiceCommand.Add(voiceCommnad.Id, voiceCommnad);
        }
    }

    public VoiceCommnad GetVoiceCommandPrefabById(string id)
    {
        if (!idToVoiceCommand.TryGetValue(id, out var voice))
        {
            throw new Exception($"VoiceCommand with id {id} does not exit");
        }
        return voice;
    }
}
