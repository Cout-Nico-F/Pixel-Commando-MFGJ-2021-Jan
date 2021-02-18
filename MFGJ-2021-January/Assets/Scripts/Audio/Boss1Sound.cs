using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Sound : MonoBehaviour
{

    public GameObject player;
    public GameObject boss;
    public AudioSource audioSource;
    [SerializeField]
    float playerHealth;

    Transform playerTransform;
    public AudioManager audioManager;

    public bool isClose;
    public bool mcIsAlive = true;

    
    public float distanceToPlayer;
    public float minimumDistanceToTrigger;

    MusicTrigger musicTrigger;

    [SerializeField]
    float volume;

    private void Awake()
    {
        musicTrigger = FindObjectOfType<MusicTrigger>();
        player = FindObjectOfType<PlayerController>().gameObject;
        playerTransform = player.transform;
        audioManager = FindObjectOfType<AudioManager>();
    }



    private void OnEnable()
    {
        
       audioManager.PlayHelicopterSound();
    }


    private void OnDisable()
    {
        audioManager.helicopterAudioSource.Stop();
    }


    private void Update()
    {
        

        if(player.activeSelf)
        {
            CalculateChopperVol();
            if (playerHealth > 0)
            {
                if (audioManager.helicopterAudioSource.isPlaying)
                {
                    return;
                }
                else
                {
                    audioManager.PlayHelicopterSound();
                }
            }
            else if (playerHealth <= 0)
            {
                mcIsAlive = false;
                audioManager.helicopterAudioSource.volume = 0.1f;
               

            }
        }else
        {
            audioManager.helicopterAudioSource.Stop();
            Debug.Log("PlayerIsDead");
        }
       
    }

    private void CalculateChopperVol()
    {
        playerHealth = player.gameObject.GetComponent<PlayerController>().healthPoints;
        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        volume = Math.Abs(0.25f + Mathf.Log10(distanceToPlayer / 100));
        audioManager.helicopterAudioSource.volume = volume;
    }


}
/*
    if (distanceToPlayer <= minimumDistanceToTrigger)
    {
        if (isClose != true)
        {
            isClose = true;
            audioManager.PlayHelicopterSound();

        }
        else if (isClose)
        {
            return;
        }
    }

else
{
    isClose = false;
    audioManager.helicopterAudioSource.Stop();
}*/
/*
        if (musicTrigger.bossIsActive)
        {
            distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
            volume = 0.25f - (distanceToPlayer / 100);
            audioManager.helicopterAudioSource.volume = volume;

            if (audioManager.helicopterAudioSource.isPlaying)
            {
                return;
            }
            else
            {
                audioManager.PlayHelicopterSound();
            }               
        }
        else if (!musicTrigger.bossIsActive)
        {
            audioManager.helicopterAudioSource.Stop();
        }*/