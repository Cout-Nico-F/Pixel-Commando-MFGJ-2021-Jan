using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    ApplyData data;
    [SerializeField]
    AudioManager audioManager;

    public GameObject comingSoonCanvas;
    public GameObject loadCanvas;
    public bool isNewGame;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

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
        //Create Data
        FileManager.CreateNewFile("SaveData");

        audioManager.PlayVoiceCommand("Brief");

    }
    
    public void LoadGame()
    {
        //Create Data
        FileManager.DownloadFile("PixelCommando_Data01");
        /*if (FileManager.loadPath.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(FileManager.loadPath[0]).AbsolutePath));
        }*/
        isNewGame = false;
        StartMission();
    }

    private IEnumerator OutputRoutine(string url)
    {
        UnityWebRequest loader = new UnityWebRequest(url);
        FileManager.loadPathPro = url;
        FileManager.newPath = FileManager.loadPathPro;
        yield return loader;
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void StartMission()
    {
        SceneManager.LoadScene("Level One");

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.MusicChangerLevels("Level One");
    }
    public void RestartMissiom()
    {
        isNewGame = true;
        SceneManager.LoadScene("Level One");

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.MusicChangerLevels("Level One");
    }

    public void ComingSoon()
    {
        loadCanvas.SetActive(false);
        comingSoonCanvas.SetActive(true);
    }
}
