using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    private int audioClipIndex;

    [Header("FootStepsSounds")]
    public List<AudioClip> footstepsGrass;
    public List<AudioClip> footstepsSand;
    public List<AudioClip> footstepsConcrete;
    public List<AudioClip> footstepsWater;


    AudioSource aS;
    AudioClip currentFs;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Grass"))
        {
            audioClipIndex = Random.Range(0, footstepsGrass.Count);
            currentFs = footstepsGrass[audioClipIndex];
        }
        else if (collision.gameObject.CompareTag("Sand"))
        {
            audioClipIndex = Random.Range(0, footstepsSand.Count);
            currentFs = footstepsSand[audioClipIndex];
        }
        else if (collision.gameObject.CompareTag("Concrete"))
        {
            audioClipIndex = Random.Range(0, footstepsConcrete.Count);
            currentFs = footstepsConcrete[audioClipIndex];
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            audioClipIndex = Random.Range(0, footstepsWater.Count);
            currentFs = footstepsWater[audioClipIndex];
        }

    }
    void playFootstepsSound()
    {
        aS.clip = currentFs;
        aS.pitch = 1 + Random.Range(-0.2f, 0.2f);
        aS.volume = 1 - Random.Range(0, 0.7f);
        aS.PlayOneShot(currentFs);
    }

}
