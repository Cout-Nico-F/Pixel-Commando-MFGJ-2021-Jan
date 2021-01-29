using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAndSpearSounds : MonoBehaviour
{

    AudioManager audioManager;
    public WeaponSounds weaponSounds = new WeaponSounds(); 

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        switch (weaponSounds)
        {
            case WeaponSounds.RocketSounds:
                audioManager.PlaySound("RocketFire");
                audioManager.PlaySound("RocketTrust");
                break;
            case WeaponSounds.SpearSounds:
                audioManager.PlaySound("TrowSpear");
                break;

        }      
    }

    private void OnDestroy()
    {
        switch (weaponSounds)
        {
            case WeaponSounds.RocketSounds:
                audioManager.PlaySound("RocketExplossion");
                break;
            default:
                return;
                break;

        } 
    }

}
