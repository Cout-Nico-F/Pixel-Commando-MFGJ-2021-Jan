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

       

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayBulletSound()
    {
        
        weaponsAs.clip = mcBulletSound;
        weaponsAs.volume = 1;
        weaponsAs.pitch = 1;
        weaponsAs.Play();
        Debug.Log("Phew");

    }
}
