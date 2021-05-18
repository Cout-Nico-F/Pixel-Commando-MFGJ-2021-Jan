using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region
    [Header("Music Tracks")]
    public AudioClip startScreenMx;
    public AudioClip lvl1Mx;
    public List<AudioClip> bossLvl1Mx;
    public AudioClip lvl2Mx;
    public List<AudioClip> deathMx;
    public List<AudioClip> winMx;
    #endregion


    [Header("Voice Commands")]
    public List<AudioClip> voiceCommands;

    public AudioClip typewriterSound;
    [Range(0, .1f)]
    public float typewriterVolume;

    [Header("- - - - - - - - - - Weapon Sounds - - - - - - - - - ")]
    public AudioClip mcBulletSound;
    public AudioClip enemiesBulletSound;
    public List<AudioClip> powerUpSound;
    public List<AudioClip> pickUpWeaponSound;
    [SerializeField] AudioClip pickUpWirecutterSound;
    [SerializeField] AudioClip cutFenceSound;

    [Header("Machine Gun Sounds")]
    public AudioClip rapidFireSoundBlast;
    public List<AudioClip> rapidFireSoundShell;
    public AudioClip rapidFireSoundZap;
    public AudioClip rapidFireSoundMech;
    [Range(0f, 0.4f)]
    public float RapidFireVolumeAdjustment;


    [Header("ScarSounds")]
    public AudioClip scarBlast;
    [Range(0f, 0.4f)]
    public float ScarFireBlastVolume;
    [Range(0f, 0.4f)]
    public float ScarFireZapVolume;
    [Range(0f, 0.4f)]
    public float ScarFireShellsVolume;
    [Range(0f, 0.2f)]
    public float ScarVolumeAdjustment;


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
    public float rocketExplossionVolume;
    [Range(0, 1f)]
    public float spearVolume;

    [Header("Bomb Sounds")]
    public AudioClip fallingBomb;
    public List<AudioClip> bossBombExplossion;
    [Range(0, .5f)]
    public float bombExplossionVolume = 0.3f;

    [Header("- - - - - - - - - - MC Sounds - - - - - - - - - -")]
    public List<AudioClip> mcGrunts;
    public List<AudioClip> playerDeath;

    [Header("Enemy Sounds")]
    public List<AudioClip> hitEnemy;
    public List<AudioClip> soldierDeath;
    public List<AudioClip> splat;
    public AudioClip machinegunnerDeath;
    public AudioClip hitSandbag;
    public AudioClip hutExplossion;
    public AudioClip helicopter;


    [Header("- - - - - - - - - - Audiosources - - - - - - - - - - ")]
    public AudioSource musicAudiosource;
    public AudioSource typewriterAudiosource;
    public AudioSource weaponsAs;
    public AudioSource enemySoundsAudiosource;
    public AudioSource machineGunnerAudiosource;
    public AudioSource mcAudioSource;
    public AudioSource voiceCommandsAudioSource;
    public AudioSource rocketTrustAudioSource;
    public AudioSource helicopterAudioSource;
    public AudioSource bombFallingAudioSource;

    public AudioMixerGroup masterOutput;

    #region
    [Header("- - - - - - - - - - Volume - - - - - - - - - - - ")]
    [Range(0f, 1f)]
    public float bulletvolume = 0.2f;
    [Range(0f, 4f)]
    public float musicVolume = 0.3f;
    [Range(0.3f, 15f)]
    public float bossMusicVolume = 15f;
    [Range(0.2f, 1f)]
    public float winMusicVolume = 15f;
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
    [Range(0f, 0.5f)]
    public float fallingBombVolume;
    [Range(0f, 0.5f)]
    public float wireCutterVolume;
    [Range(0f, 0.5f)]
    public float cutFenceVolume;
    #endregion

    [Header ("- - - - - Audio Mixer Outputs - - - - -")]
    public AudioMixerGroup voiceCommandsMixerGroup;
    public AudioMixerGroup weaponsMixerGroup;
    public AudioMixerGroup mcMixerGroup;

    int m_enemyDeathIndex;
    int m_enemyHitIndex;
    int m_winMxIndex;
    public float winMxLength; 

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

        voiceCommandsAudioSource.outputAudioMixerGroup = voiceCommandsMixerGroup;
        weaponsAs.outputAudioMixerGroup = weaponsMixerGroup;
        mcAudioSource.outputAudioMixerGroup = mcMixerGroup;
        machineGunnerAudiosource.outputAudioMixerGroup = weaponsMixerGroup;
        m_winMxIndex = Random.Range(0,winMx.Count);
        winMxLength = winMx[m_winMxIndex].length;
    }

    void Start()
    {
        MusicChangerLevels();
        //Debug.Log($"Win Mx Lenght is {winMxLength.ToString()}");
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
                musicAudiosource.loop = true;
                musicAudiosource.volume = musicVolume;
                break;
            case "Level Two":
                musicAudiosource.clip = lvl2Mx;
                musicAudiosource.loop = true;
                musicAudiosource.volume = musicVolume;
                break;
            case "Die":
                musicAudiosource.clip = deathMx[Random.Range(0, deathMx.Count)];
                musicAudiosource.loop = false;
                musicAudiosource.volume = musicVolume;
                break;
            case "BossFight":
                musicAudiosource.clip = bossLvl1Mx[Random.Range(0, bossLvl1Mx.Count)];
                musicAudiosource.loop = true;
                musicAudiosource.volume = bossMusicVolume;
                break;
            case "Win":
                if(musicAudiosource.clip == winMx[0] || musicAudiosource.clip == winMx[1])
                {
                    return;
                }else
                {
                    musicAudiosource.clip = winMx[m_winMxIndex];
                    musicAudiosource.loop = false;
                    musicAudiosource.volume = winMusicVolume;
                }
                break;

        }
        musicAudiosource.Play();
    }

    public void PlaySound(string audioClip)
    {

        switch (audioClip)
        {
            case "BulletSound":
                PlayShortSounds(mcBulletSound, bulletvolume, pitchVariation);
                break;
            case "McHit":
                if (mcAudioSource.isPlaying)
                {
                    return;
                }
                else
                {
                    mcAudioSource.volume = mcHitVolume - Random.Range(0.3f, 0.4f);
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
                mcAudioSource.volume = mcHitVolume + 0.05f;
                mcAudioSource.pitch = Random.Range(1.3f, 1.4f);
                mcAudioSource.Play();
                break;
            case "RocketFire":
                PlayShortSounds(rocketFire, fireVolume, 1f);
                break;
            case "RocketTrust":
              
                rocketTrustAudioSource.volume = trustVolume;
                rocketTrustAudioSource.outputAudioMixerGroup = masterOutput;
                rocketTrustAudioSource.loop = true;
                rocketTrustAudioSource.clip = rocketTrust;
                rocketTrustAudioSource.Play();
                break;
            case "RocketExplossion":
                rocketExplossionVolume = 0.3f;
                PlayShortSounds(rocketExplossion, rocketExplossionVolume, 0.5f);
                rocketTrustAudioSource.Stop();
                break;
            case "TrowSpear":
                PlayShortSounds(spearSound[Random.Range(0, spearSound.Count)], spearVolume, 1f);
                break;
            case "DestroyHut":
                PlayShortSounds(rocketExplossion, rocketExplossionVolume, 0.3f);
                //PlayShortSounds(hutExplossion, rocketExplossionVolume, Random.Range(0.8f,1.05f));
                break;
            case "PickUpWeapon":
                PlayShortSounds(pickUpWeaponSound[Random.Range(0, pickUpWeaponSound.Count)], pickUpWeaponVolume, Random.Range(0.9f,1.2f));
                break;
            case "RapidFire":
                PlayShortSounds(rapidFireSoundBlast, Random.Range(0.3f, 0.5f) - RapidFireVolumeAdjustment, Random.Range(0.9f, 1.1f));
                PlayShortSounds(rapidFireSoundZap , Random.Range(0.1f, 0.2f) - RapidFireVolumeAdjustment, Random.Range(0.9f, 1.1f));
                PlayShortSounds(rapidFireSoundShell[Random.Range(0, rapidFireSoundShell.Count)], Random.Range(0.01f, 0.05f) - RapidFireVolumeAdjustment, Random.Range(0.9f, 1.1f));
                PlayShortSounds(rapidFireSoundMech, Random.Range(0.1f, 0.2f), Random.Range(0.9f - RapidFireVolumeAdjustment, 1.1f));
                break;
            case "Splat":
                PlayShortSounds(splat[Random.Range(0, splat.Count)], Random.Range(0.2f, 0.5f), Random.Range(0.8f, 1.1f));
                break;
            case "FallingBomb":
                bombFallingAudioSource.volume = fallingBombVolume;
                bombFallingAudioSource.pitch = Random.Range(0.8f, 1.1f);
                bombFallingAudioSource.outputAudioMixerGroup = masterOutput;
                bombFallingAudioSource.loop = false;
                bombFallingAudioSource.clip = fallingBomb;
                bombFallingAudioSource.Play();
                break;
            case "BombExplossion":
                PlayShortSounds(bossBombExplossion[Random.Range(0, bossBombExplossion.Count)], bombExplossionVolume, Random.Range(0.8f, 1.2f));
                bombFallingAudioSource.Stop();
                break;
            case "BossExplode":
                PlayShortSounds(bossBombExplossion[Random.Range(0, bossBombExplossion.Count)], bombExplossionVolume + 0.3f, Random.Range(0.8f, 1.2f));
                break;
            case "Skar":
                try
                {
                    PlayShortSounds(scarBlast, ScarFireBlastVolume - ScarVolumeAdjustment, Random.Range(1f, 1.3f));
                    PlayShortSounds(rapidFireSoundZap, ScarFireZapVolume - ScarVolumeAdjustment, Random.Range(0.9f, 1.1f));
                    PlayShortSounds(rapidFireSoundShell[Random.Range(0, rapidFireSoundShell.Count)], ScarFireShellsVolume - ScarVolumeAdjustment, Random.Range(0.9f, 1.1f));
                    PlayShortSounds(rapidFireSoundMech, Random.Range(0.1f, 0.2f) - ScarVolumeAdjustment, Random.Range(0.9f, 1.1f));
                }
                catch
                {
                    Debug.Log("Hmmm... something's wrong");
                }
                break;
            case "TypeWriter":
                typewriterAudiosource.volume = typewriterVolume;
                typewriterAudiosource.PlayOneShot(typewriterSound);
                break;
            case "Wirecutter":
                PlayShortSounds(pickUpWirecutterSound, wireCutterVolume, Random.Range(0.95f, 1.05f));
                break;
            case "CutFence":
                PlayShortSounds(cutFenceSound, cutFenceVolume, Random.Range(0.95f, 1.05f));
                break;
            default:
                EnemySoundSelection(audioClip);
                break;
        }

    }


    public void PlayHelicopterSound()
    {
        if (!helicopterAudioSource.isPlaying)
        {
            helicopterAudioSource.loop = true;
            helicopterAudioSource.clip = helicopter;
            helicopterAudioSource.Play();
        }
        else
        {
            return;
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
            case "WireCutters":
                voiceCommandsAudioSource.clip = voiceCommands[4];
                break;
            case "MCdead":
                voiceCommandsAudioSource.Stop();
                voiceCommandsAudioSource.clip = null;
                break;
        }
        voiceCommandsAudioSource.Play();
    }

    public void PlayShortSounds(AudioClip audioClip, float volume, float pitch)
    {
        try
        {
            AudioSource aS = gameObject.AddComponent<AudioSource>() as AudioSource;
            aS.pitch = pitch;
            aS.outputAudioMixerGroup = weaponsMixerGroup;
            aS.PlayOneShot(audioClip, volume);
            Destroy(aS, audioClip.length);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }

    }
    void EnemySoundSelection(string audioClip)
    {
        if (audioClip == "HitSoldier" || audioClip == "EnemySoldierDeath")
        {
            switch (audioClip)
            {
                case "HitSoldier":
                    pitchVariation = Random.Range(1f, 1.8f);
                    m_enemyHitIndex = Random.Range(0, hitEnemy.Count);
                    enemySoundsAudiosource.clip = hitEnemy[m_enemyHitIndex];
                    enemySoundsAudiosource.volume = enemyHitVolume - Random.Range(0.2f, 0.4f);
                    enemySoundsAudiosource.pitch = pitchVariation;
                    break;

                case "EnemySoldierDeath":
                    pitchVariation = Random.Range(0.9f, 1.1f);
                    m_enemyDeathIndex = Random.Range(0, soldierDeath.Count);
                    enemySoundsAudiosource.clip = soldierDeath[m_enemyDeathIndex];
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
                    m_enemyHitIndex = Random.Range(0, hitEnemy.Count);
                    pitchVariation = Random.Range(0.85f, 1f);
                    machineGunnerAudiosource.clip = hitEnemy[m_enemyHitIndex];
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
                    //Debug.Log("Sand");
                    break;
            }
            machineGunnerAudiosource.outputAudioMixerGroup = mcMixerGroup;
            machineGunnerAudiosource.Play();
        }
    }

}
