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


    [Header("Weapon Sounds")]
    public AudioClip mcBulletSound;
    public AudioClip enemiesBulletSound;
    public AudioClip pickUpSound;

    [Header("MC Sounds")]
    public List<AudioClip> mcGrunts;

    [Header("Enemy Sounds")]
    public List<AudioClip> hitEnemy;
    public List<AudioClip> soldierDeath;
    public AudioClip machinegunnerDeath;
    public AudioClip hitSandbag;


    [Header("Audiosources")]
    public AudioSource musicAudiosource;
    public AudioSource weaponsAs;
    public AudioSource enemySoundsAudiosource;
    public AudioSource machineGunnerAudiosource;
    public AudioSource mcAudioSource;


    [Header("Volume")]
    [Range(0f, 1f)]
    public float bulletvolume = 0.2f;
    [Range(0f, 1f)]
    public float musicVolume = 0.3f;
    [Range(0.2f, 1f)]
    public float enemyDeathVolume;
    [Range(0.2f, 1f)]
    public float machinegunnerDeathVolume;
    [Range(0.2f, 1f)]
    public float enemyHitVolume;
    [Range(0.2f, 1f)]
    public float mcHitVolume;

    int enemyDeathIndex;
    int enemyHitIndex;

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
    }

    void Start()
    {
        StartMusic();
    }


    void StartMusic()
    {
        musicAudiosource.playOnAwake = true;
        musicAudiosource.loop = true;
        musicAudiosource.volume = musicVolume;
        musicAudiosource.clip = lvl1Mx;
        musicAudiosource.Play();
    }

    public void PlaySound(string audioClip)
    {
        if (audioClip == "BulletSound" || audioClip == "McHit")
        {
            if (audioClip == "BulletSound")
            {
                weaponsAs.clip = mcBulletSound;
                weaponsAs.volume = bulletvolume;
                weaponsAs.pitch = pitchVariation;
                weaponsAs.Play();
            }
            else if (audioClip == "McHit")
            {
                if (mcAudioSource.isPlaying)
                {
                    return;
                }
                else
                {
                    mcAudioSource.volume = mcHitVolume - Random.Range(0.2f, 0.5f);
                    mcAudioSource.pitch = Random.Range(1.3f, 1.5f);
                    mcAudioSource.PlayOneShot(mcGrunts[Random.Range(0, mcGrunts.Count)]);
                }
            }

        }
        else
        {
            EnemySoundSelection(audioClip);
        }

    }


    void EnemySoundSelection(string audioClip)
    {
        if (audioClip == "HitSoldier" || audioClip == "EnemySoldierDeath")
        {
            switch (audioClip)
            {
                case "HitSoldier":
                    enemyHitIndex = Random.Range(0, hitEnemy.Count);
                    pitchVariation = Random.Range(1f, 1.8f);
                    enemySoundsAudiosource.clip = hitEnemy[enemyHitIndex];
                    enemySoundsAudiosource.volume = enemyHitVolume - Random.Range(0.2f, 0.4f);
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
                
                case "EnemySoldierDeath":
                    pitchVariation = Random.Range(0.9f, 1.1f);
                    enemyDeathIndex = Random.Range(0, soldierDeath.Count);
                    enemySoundsAudiosource.clip = soldierDeath[enemyDeathIndex];
                    enemySoundsAudiosource.volume = enemyDeathVolume - Random.Range(0, 0.3f);
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;
                
            }

            enemySoundsAudiosource.Play();

        }
        else if(audioClip == "HitMachineGunner" || audioClip == "EnemyMachineGunnerDeath" || audioClip == "HitSandbag")
        {
            switch (audioClip)
            {
                case "HitMachineGunner":
                    enemyHitIndex = Random.Range(0, hitEnemy.Count);
                    pitchVariation = Random.Range(0.85f, 1f);
                    machineGunnerAudiosource.clip = hitEnemy[enemyHitIndex];
                    machineGunnerAudiosource.volume = Random.Range(0.2f, 0.4f);
                    machineGunnerAudiosource.pitch = pitchVariation;
                    break;
                case "EnemyMachineGunnerDeath":
                    machineGunnerAudiosource.clip = machinegunnerDeath;
                    machineGunnerAudiosource.volume = machinegunnerDeathVolume - Random.Range(0f, 0.3f);
                    machineGunnerAudiosource.pitch = pitchVariation;
                    break;
                case "HitSandbag":
                    pitchVariation = Random.Range(0.6f, 1.1f);
                    machineGunnerAudiosource.clip = hitSandbag;
                    machineGunnerAudiosource.volume = Random.Range(0.2f, 0.4f); ;
                    machineGunnerAudiosource.pitch = pitchVariation;
                    Debug.Log("Sand");
                    break;
            }
            machineGunnerAudiosource.Play();
        }
    }

}
