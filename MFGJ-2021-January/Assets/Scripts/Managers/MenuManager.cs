using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    ApplyData data;
    AudioManager audioManager;

    public bool isNewGame;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        DontDestroyOnLoad(this.gameObject);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Briefing");
        isNewGame = true;

        audioManager.PlayVoiceCommand("Brief");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level One");
        isNewGame = false;
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
