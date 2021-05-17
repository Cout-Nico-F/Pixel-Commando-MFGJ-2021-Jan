using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    
    public PlayerController playerController;
    #region
    [Header("FootStepsSounds")]
    [SerializeField] List<AudioClip> m_FootstepsGrass;
    [SerializeField] List<AudioClip> m_FootstepsSand;
    [SerializeField] List<AudioClip> m_FootstepsConcrete;
    [SerializeField] List<AudioClip> m_FootstepsWater;
    [SerializeField] List<AudioClip> m_FootstepsWood;
    
    [SerializeField] Surfaces m_CurrentSurface = new Surfaces();
    [SerializeField] AudioSource m_AudioSourceFS;
    private AudioClip m_CurrentFs;
    private int m_AudioClipIndex;
    #endregion
    void Start()
    {
        if (m_AudioSourceFS == null)
        {
            m_AudioSourceFS = GetComponent<AudioSource>();
        }

        m_CurrentFs = m_FootstepsGrass[m_AudioClipIndex];
    }

    void PlayFootstepsSound()
    {
        m_AudioSourceFS.clip = m_CurrentFs;
        m_AudioSourceFS.pitch = 1 + Random.Range(-0.2f, 0.2f);
        m_AudioSourceFS.volume = 1 - Random.Range(0, 0.3f);
        m_AudioSourceFS.PlayOneShot(m_CurrentFs);
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

    public void RandomizeFootsteps()
    {
        m_AudioClipIndex = Random.Range(0, m_FootstepsGrass.Count);
    }


    public void SurfaceSelection(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Sand":
                m_CurrentSurface = Surfaces.Sand;
                m_CurrentFs = m_FootstepsSand[m_AudioClipIndex];
                break;
            case "Concrete":
                m_CurrentSurface = Surfaces.Concrete;
                m_CurrentFs = m_FootstepsConcrete[m_AudioClipIndex];
                break;
            case "Water":
                m_CurrentSurface = Surfaces.Water;
                m_CurrentFs = m_FootstepsWater[m_AudioClipIndex];
                break;
            case "WaterOnConcrete":
                m_CurrentSurface = Surfaces.Water;
                m_CurrentFs = m_FootstepsWater[m_AudioClipIndex];
                break;
            case "Wood":
                m_CurrentSurface = Surfaces.Wood;
                m_CurrentFs = m_FootstepsWood[m_AudioClipIndex];
                break;
            case "WoodInsideRoom":
                m_CurrentSurface = Surfaces.Wood;
                m_CurrentFs = m_FootstepsWood[m_AudioClipIndex];
                break;
            case "Background":
                
                break;
            default:
                m_CurrentFs = m_FootstepsGrass[m_AudioClipIndex];
                m_CurrentSurface = Surfaces.Grass;
                Debug.Log("Grass Again, Why?");
                // Debug.LogError("Error in footstep switch at FootStepsSound.cs line: 68");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water") ||
            collision.gameObject.CompareTag("Concrete") ||
            collision.gameObject.CompareTag("Wood") ||
            collision.gameObject.CompareTag("Sand"))
        {
            m_AudioClipIndex = Random.Range(0, m_FootstepsGrass.Count);
            m_CurrentFs = m_FootstepsGrass[m_AudioClipIndex];
            m_CurrentSurface = Surfaces.Grass;
            Debug.Log("Grass Again");
        }
        else if (m_CurrentSurface == Surfaces.Concrete)
        {
            return;
        }
        if (collision.gameObject.CompareTag("WaterOnConcrete"))
        {
            m_AudioClipIndex = Random.Range(0, m_FootstepsGrass.Count);
            m_CurrentFs = m_FootstepsConcrete[m_AudioClipIndex];
            m_CurrentSurface = Surfaces.Concrete;
        }
    }
}
