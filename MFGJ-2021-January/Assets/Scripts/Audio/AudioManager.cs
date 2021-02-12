using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Music Tracks")]
    public AudioClip startScreenMx;
    public AudioClip lvl1Mx;
    public AudioClip lvl2Mx;
    public List<AudioClip> deathMx;

    [Header("Voice Commands")]
    public List<AudioClip> voiceCommands;

    [Header("Weapon Sounds")]
    public AudioClip mcBulletSound;
    public AudioClip enemiesBulletSound;
    public List<AudioClip> powerUpSound;
    public List<AudioClip> pickUpWeaponSound;
    public AudioClip rapidFireSoundBlast;
    public List<AudioClip> rapidFireSoundShell;
    public AudioClip rapidFireSoundZap;
    public AudioClip rapidFireSoundMech;

    [Header("Rocket & Spear Sounds")]
    public AudioClip rocketFire;
    public AudioClip rocketTrust;
    public AudioClip rocketExplossion;
    public List<AudioClip> spearSound;
    [Range(0, .5f)]
    public float fireVolume;
    [Range(0, .5f)]
    public float trustVolume;
    [Range(0, .5f)]
    public float explossionVolume;
    [Range(0, 1f)]
    public float spearVolume;


    [Header("MC Sounds")]
    public List<AudioClip> mcGrunts;
    public List<AudioClip> playerDeath;

    [Header("Enemy Sounds")]
    public List<AudioClip> hitEnemy;
    public List<AudioClip> soldierDeath;
    public AudioClip machinegunnerDeath;
    public AudioClip hitSandbag;
    public AudioClip hutExplossion;


    [Header("Audiosources")]
    public AudioSource musicAudiosource;
    public AudioSource weaponsAs;
    public AudioSource enemySoundsAudiosource;
    public AudioSource machineGunnerAudiosource;
    public AudioSource mcAudioSource;
    public AudioSource voiceCommandsAudioSource;

    public AudioMixerGroup masterOutput;


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
    [Range(0, 0.5f)]
    public float dialogueVolume;
    [Range(0f, 0.5f)]
    public float PickUpHealVolume;
    [Range(0f, 0.5f)]
    public float pickUpWeaponVolume;

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
        MusicChangerLevels();
    }

    public void MusicChangerLevels()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            musicAudiosource.playOnAwake = true;
            musicAudiosource.loop = true;
            musicAudiosource.volume = musicVolume;
            musicAudiosource.clip = startScreenMx;
            musicAudiosource.Play();
        }
    }

    public void MusicChangerLevels(string scene)
    {
        if (musicAudiosource.isPlaying)
        {
            musicAudiosource.Stop();
        }
        if (voiceCommandsAudioSource.isPlaying)
        {
            voiceCommandsAudioSource.Stop();
        }
        switch (scene)
        {
            case "Level One":
                musicAudiosource.clip = lvl1Mx;
                break;
            case "Die":

                musicAudiosource.clip = deathMx[Random.Range(0, deathMx.Count)];
                musicAudiosource.loop = false;
                break;
        }
        musicAudiosource.Play();
    }

    public void PlaySound(string audioClip)
    {

        switch (audioClip)
        {
            case "BulletSound":
                mcAudioSource.clip = mcBulletSound;
                mcAudioSource.volume = bulletvolume;
                mcAudioSource.pitch = pitchVariation;
                mcAudioSource.Play();
                break;
            case "McHit":
                if (mcAudioSource.isPlaying)
                {
                    return;
                }
                else
                {
                    mcAudioSource.volume = mcHitVolume - Random.Range(0.2f, 0.42f);
                    mcAudioSource.pitch = Random.Range(1.3f, 1.5f);
                    mcAudioSource.PlayOneShot(mcGrunts[Random.Range(0, mcGrunts.Count)]);
                }
                break;
            case "Damage":
                pitchVariation = Random.Range(0.95f, 1.15f);
                weaponsAs.clip = enemiesBulletSound;
                weaponsAs.volume = bulletvolume;
                weaponsAs.pitch = pitchVariation;
                weaponsAs.Play();
                break;
            case "PlayerDeath":
                mcAudioSource.clip = playerDeath[Random.Range(0, playerDeath.Count)];
                mcAudioSource.volume = mcHitVolume + 0.12f;
                mcAudioSource.pitch = Random.Range(1.3f, 1.9f);
                mcAudioSource.Play();
                break;
            case "RocketFire":
                PlayShortSounds(rocketFire, fireVolume, 1f);
                break;
            case "RocketTrust":
                AudioSource aSo = gameObject.AddComponent<AudioSource>() as AudioSource;
                aSo.volume = trustVolume;
                aSo.outputAudioMixerGroup = masterOutput;
                aSo.loop = true;
                aSo.clip = rocketTrust;
                aSo.Play();
                Destroy(aSo, rocketTrust.length);
                break;
            case "RocketExplossion":
                PlayShortSounds(rocketExplossion, explossionVolume, 0.5f);
                break;
            case "TrowSpear":
                PlayShortSounds(spearSound[Random.Range(0, spearSound.Count)], spearVolume, 1f);
                break;
            case "DestroyHut":
                PlayShortSounds(rocketExplossion, explossionVolume, 0.3f);
                PlayShortSounds(hutExplossion, explossionVolume, Random.Range(0.8f,1.05f));
                break;
            case "PickUpWeapon":
                PlayShortSounds(pickUpWeaponSound[Random.Range(0, pickUpWeaponSound.Count)], pickUpWeaponVolume, Random.Range(0.9f,1.2f));
                break;
            case "RapidFire":
                PlayShortSounds(rapidFireSoundBlast, Random.Range(0.5f, 0.7f), Random.Range(0.9f, 1.1f));
                PlayShortSounds(rapidFireSoundZap , Random.Range(0.1f, 0.2f), Random.Range(0.9f, 1.1f));
                PlayShortSounds(rapidFireSoundShell[Random.Range(0, rapidFireSoundShell.Count)], Random.Range(0.01f, 0.05f), Random.Range(0.9f, 1.1f));
                PlayShortSounds(rapidFireSoundMech, Random.Range(0.1f, 0.2f), Random.Range(0.9f, 1.1f));
                break;
            default:
                EnemySoundSelection(audioClip);
                break;
        }

    }

    public void PlayHealingSound(string audioClip)
    {
        if (audioClip == "Heal")
        {
            pitchVariation = Random.Range(0.9f, 1.12f);
            mcAudioSource.clip = powerUpSound[Random.Range(0, powerUpSound.Count)];
            mcAudioSource.volume = PickUpHealVolume;
            mcAudioSource.pitch = pitchVariation;
            mcAudioSource.Play();
        }
    }

    public void PlayVoiceCommand(string audioClip)
    {
        voiceCommandsAudioSource.volume = dialogueVolume;
        switch (audioClip)
        {
            case "Brief":
                voiceCommandsAudioSource.clip = voiceCommands[0];
                break;
            case "SurroundedByEnemies":
                if (voiceCommandsAudioSource.isPlaying)
                {
                    voiceCommandsAudioSource.Stop();
                }
                voiceCommandsAudioSource.clip = voiceCommands[1];
                break;
            case "DestroyHuts":
                voiceCommandsAudioSource.clip = voiceCommands[2];
                break;
            case "ShootFence":
                voiceCommandsAudioSource.clip = voiceCommands[3];
                break;
        }
        voiceCommandsAudioSource.Play();
    }

    public void PlayShortSounds(AudioClip audioClip, float volume, float pitch)
    {
        AudioSource aS = gameObject.AddComponent<AudioSource>() as AudioSource;
        aS.pitch = pitch;
        aS.outputAudioMixerGroup = masterOutput;
        aS.PlayOneShot(audioClip, volume);
        Destroy(aS, audioClip.length);
    }
    void EnemySoundSelection(string audioClip)
    {
        if (audioClip == "HitSoldier" || audioClip == "EnemySoldierDeath")
        {
            switch (audioClip)
            {
                case "HitSoldier":
                    pitchVariation = Random.Range(1f, 1.8f);
                    enemyHitIndex = Random.Range(0, hitEnemy.Count);
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
        else if (audioClip == "HitMachineGunner" || audioClip == "EnemyMachineGunnerDeath" || audioClip == "HitSandbag")
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
