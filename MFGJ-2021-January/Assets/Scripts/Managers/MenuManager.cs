using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Briefing");
        audioManager.PlayVoiceCommand("Brief");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartMission()
    {
        SceneManager.LoadScene("Level One");
        
        audioManager.MusicChangerLevels("Level One");
    }
}
