using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    [Header("FootStepsSounds")]
    public List<AudioClip> footstepsGrass;
    AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playFootstepsSound()
    {
        AudioClip currentFs = footstepsGrass[Random.Range(0,footstepsGrass.Count)];
        //aS.clip = currentFs;
        aS.pitch = 1 + Random.Range(-0.1f, 0.1f);
        aS.volume = 1 - Random.Range(0, 0.7f);
        aS.PlayOneShot(currentFs);


    }
}
