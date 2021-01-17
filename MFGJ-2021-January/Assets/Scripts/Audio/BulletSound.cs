using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    AudioManager aM;

    private void Awake()
    {
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        aM.PlayBulletSound();
    }

}
