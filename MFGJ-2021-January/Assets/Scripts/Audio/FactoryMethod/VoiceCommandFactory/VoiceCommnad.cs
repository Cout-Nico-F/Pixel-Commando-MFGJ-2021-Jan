using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCommnad : MonoBehaviour
{  
    [SerializeField] protected string id;
    [SerializeField] protected AudioClip audioClip;

    public string Id => id;
    public AudioClip AudioClip => audioClip;
}
