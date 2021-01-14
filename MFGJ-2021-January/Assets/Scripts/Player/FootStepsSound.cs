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

    public void SurfaceSelection(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            //case "Grass":
            //    Debug.Log("Grass-Step");
            //    audioClipIndex = Random.Range(0, footstepsGrass.Count);
            //    currentFs = footstepsGrass[audioClipIndex];
            //    break;
            case "Sand":
                Debug.Log("Sand-Step");
                audioClipIndex = Random.Range(0, footstepsSand.Count);
                currentFs = footstepsSand[audioClipIndex];
                break;
            case "Concrete":
                Debug.Log("Concrete-Step");
                audioClipIndex = Random.Range(0, footstepsConcrete.Count);
                currentFs = footstepsConcrete[audioClipIndex];
                break;
            case "Water":
                Debug.Log("Water-Step");
                audioClipIndex = Random.Range(0, footstepsWater.Count);
                currentFs = footstepsWater[audioClipIndex];
                break;
            default:
                Debug.LogError("Error in footstep switch at FootStepsSound.cs line: 68");
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
