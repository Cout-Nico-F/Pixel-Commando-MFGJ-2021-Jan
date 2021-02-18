using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject boss;
    public bool bossIsActive = false;
    private void Awake()
    {
        bossIsActive = false;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>(); 
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.MusicChangerLevels("BossFight");
        }
    }*/

    private void Update()
    {
        if (bossIsActive == false)
        {
            if (boss.activeSelf)
            {
                audioManager.MusicChangerLevels("BossFight");
                bossIsActive = true;
            }
        }else
        {

        }
       
    }

}
