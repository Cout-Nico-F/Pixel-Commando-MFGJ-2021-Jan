using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    ApplyData data;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Briefing");
        audioManager.PlayVoiceCommand("Brief");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level One");
        data.LoadFile();

        audioManager.PlayVoiceCommand("Brief");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartMission()
    {
        SceneManager.LoadScene("Level One");
        data.CreateFile();

        audioManager.MusicChangerLevels("Level One");
    }
}
