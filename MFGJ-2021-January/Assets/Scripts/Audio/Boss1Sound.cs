using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Sound : MonoBehaviour
{

    public GameObject player;
    Transform playerTransform;
    public AudioManager audioManager;

    public bool isClose;

    
    public float distanceToPlayer;
    public float minimumDistanceToTrigger;

    [SerializeField]
    float volume;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerTransform = player.transform;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        volume = 0.25f - (distanceToPlayer / 100);
        audioManager.helicopterAudioSource.volume = volume;

        if(distanceToPlayer <= minimumDistanceToTrigger)
        {
            if(isClose != true)
            {
                isClose = true;
                audioManager.PlayHelicopterSound();

            }else if(isClose)
            {
                return;
            }
        }else
        {
            isClose = false;
            audioManager.helicopterAudioSource.Stop();
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        audioManager.PlayHelicopterSound();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            audioManager.helicopterAudioSource.Stop();
    }*/
    private void OnDestroy()
    {
        audioManager.helicopterAudioSource.Stop();
    }
}
