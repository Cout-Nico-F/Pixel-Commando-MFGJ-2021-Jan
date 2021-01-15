using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music Tracks")]
    public AudioClip startScreenMx;
    public AudioClip lvl1Mx;
    public AudioClip lvl2Mx;


    [Header ("Weapon Sounds")]
    public AudioClip mcBulletSound;
    public AudioClip enemiesBulletSound;
    public AudioClip pickUpSound;

    [Header("Audiosources")]
    public AudioSource musicAudiosource;
    public AudioSource weaponsAs;

    [Header("Volume")]
    public float bulletvolume = 0.2f;
    public float musicVolume = 0.5f;

       

    void Start()
    {
        musicAudiosource.loop = true;
        musicAudiosource.volume = musicVolume;
        musicAudiosource.clip = lvl1Mx;
        musicAudiosource.Play();
    }

    public void PlayBulletSound()
    {
        
        weaponsAs.clip = mcBulletSound;
        weaponsAs.volume = bulletvolume;
        weaponsAs.pitch = 1;
        weaponsAs.Play();
        //Debug.Log("Phew");

    }
}
