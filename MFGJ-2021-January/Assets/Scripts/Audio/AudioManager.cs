using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    [Header("Enemy Sounds")]
    public AudioClip hitEnemy;
    public AudioClip soldierDeath;
    public AudioClip machinegunnerDeath;
    public AudioClip hitSandbag;


    [Header("Audiosources")]
    public AudioSource musicAudiosource;
    public AudioSource weaponsAs;
    public AudioSource enemySoundsAudiosource;


    [Header("Volume")]
    public float bulletvolume = 0.2f;
    public float musicVolume = 0.5f;


    Enemy enemy;

    public static AudioManager instance;

    private float pitchVariation = 1;

    public float PitchVariation
    {
        get { return pitchVariation; }
        set { pitchVariation = value; }
    }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

            DontDestroyOnLoad(gameObject);
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    void Start()
    {
        StartMusic();
    }

    
    void StartMusic()
    {
        musicAudiosource.loop = true;
        musicAudiosource.volume = musicVolume;
        musicAudiosource.clip = lvl1Mx;
        musicAudiosource.Play();
    }

    public void PlaySound(string audioClip)
    {
        if (audioClip == "BulletSound")
        {
            weaponsAs.clip = mcBulletSound;
            weaponsAs.volume = bulletvolume;
            weaponsAs.pitch = pitchVariation;
            weaponsAs.Play();
        }
        else
        {
            switch (audioClip)
            {
                case "HitSoldier":
                    enemySoundsAudiosource.clip = hitEnemy;
                    enemySoundsAudiosource.volume = Random.Range(0.2f, 0.4f);
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
                case "HitMachineGunner":
                    pitchVariation = Random.Range(1.1f, 1.22f);
                    enemySoundsAudiosource.clip = hitEnemy;
                    enemySoundsAudiosource.volume = Random.Range(0.2f, 0.4f);
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
                case "HitSandbag":
                    pitchVariation = Random.Range(0.6f, 1.1f);
                    enemySoundsAudiosource.clip = hitSandbag;
                    enemySoundsAudiosource.volume = Random.Range(0.2f, 0.4f); ;
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
                case "EnemySoldierDeath":
                    enemySoundsAudiosource.clip = soldierDeath;
                    enemySoundsAudiosource.volume = bulletvolume;
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
                case "EnemyMachineGunnerDeath":
                    enemySoundsAudiosource.clip = machinegunnerDeath;
                    enemySoundsAudiosource.volume = bulletvolume;
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
            }
            enemySoundsAudiosource.Play();
        }
    }
    /*public void PlayBulletSound()
    {
        weaponsAs.clip = mcBulletSound;
        weaponsAs.volume = bulletvolume;
        weaponsAs.pitch = 1;
        weaponsAs.Play();  
    }

    public void PlayHitSoldierSound()
    {
        pitchVariation = Random.Range(1.5f, 1.7f);
        weaponsAs.clip = hitEnemy;
        weaponsAs.volume = Random.Range(0.2f, 0.4f);
        weaponsAs.pitch = pitchVariation;
        weaponsAs.Play();
    }

    public void PlayHitMachinegunnerSound()
    {
        weaponsAs.clip = hitEnemy;
        weaponsAs.volume = Random.Range(0.2f, 0.4f);
        weaponsAs.pitch = Random.Range(1.1f, 1.22f);
        weaponsAs.Play();
    }

    public void PlayHitSandbagSound()
    {

        weaponsAs.clip = hitSandbag;
        weaponsAs.volume = Random.Range(0.2f, 0.4f);
        weaponsAs.pitch = Random.Range(0.1f, 1.1f);
        weaponsAs.Play();
    }*/
    
}
