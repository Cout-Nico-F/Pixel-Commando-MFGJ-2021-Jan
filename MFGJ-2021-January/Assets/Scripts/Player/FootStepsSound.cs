using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    private int audioClipIndex;
    public PlayerController playerController;

    [Header("FootStepsSounds")]
    public List<AudioClip> footstepsGrass;
    public List<AudioClip> footstepsSand;
    public List<AudioClip> footstepsConcrete;
    public List<AudioClip> footstepsWater;

    AudioSource aS;
    AudioClip currentFs;

    void Start()
    {
        aS = GetComponent<AudioSource>();
        audioClipIndex = Random.Range(0, footstepsGrass.Count);
        currentFs = footstepsGrass[audioClipIndex];
    }

    void PlayFootstepsSound()
    {
        aS.clip = currentFs;
        aS.pitch = 1 + Random.Range(-0.2f, 0.2f);
        aS.volume = 1 - Random.Range(0, 0.3f);
        aS.PlayOneShot(currentFs);
    }

    void PlaySecondarySteps()
    {
        //if (playerController.isRunning)  isrunning is deprecated on playerController
        //{
        //   // Debug.Log("secondary steps");
        //    PlayFootstepsSound();
        //}
        //else
        //{
        //    return;
        //}
    }

    public void SurfaceSelection(Collider2D collision)
    {
        audioClipIndex = Random.Range(0, footstepsGrass.Count);
        switch (collision.gameObject.tag)
        {
            //case "Grass":
            //    Debug.Log("Grass-Step");
            //   currentFs = footstepsGrass[audioClipIndex];
            //   break;
            case "Sand":
                //Debug.Log("Sand-Step");       
                currentFs = footstepsSand[audioClipIndex];
                break;
            case "Concrete":
                //Debug.Log("Concrete-Step");
                currentFs = footstepsConcrete[audioClipIndex];
                break;
            case "Water":
                //Debug.Log("Water-Step");;
                currentFs = footstepsWater[audioClipIndex];
                break;
            default:
                currentFs = footstepsGrass[audioClipIndex];
                //Debug.LogError("Error in footstep switch at FootStepsSound.cs line: 68");
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Concrete") || collision.gameObject.CompareTag("Sand"))
        {
            audioClipIndex = Random.Range(0, footstepsGrass.Count);
            currentFs = footstepsGrass[audioClipIndex];
        }
    }

}
