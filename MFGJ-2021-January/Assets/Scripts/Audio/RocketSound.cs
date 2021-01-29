using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSound : MonoBehaviour
{

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        audioManager.PlaySound("RocketFire");
        audioManager.PlaySound("RocketTrust");
    }

    private void OnDestroy()
    {
        audioManager.PlaySound("RocketExplossion");
    }

}
