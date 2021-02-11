using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    ApplyData data;
    AudioManager audioManager;

    public bool isNewGame;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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
