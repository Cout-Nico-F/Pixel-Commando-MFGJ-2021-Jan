using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //the game manager class will manage the scene changes, pause, restart and etc, and will be the intermediate between UI and the player.

    private bool isGameOver;
    public bool IsGameOver { get => isGameOver; }

    [SerializeField]
    PlayerController player = null;

    public GameObject PlayerPrefab;
    public GameObject PauseCanvas;
    public GameObject ContinueCanvas;
    public GameObject MissionFailedCanvas;
    public Transform Checkpoint;

    public GameObject rocketsUI;
    public GameObject javelinUI;

    AudioManager audioManager;
    [HideInInspector]
    public int lastLives;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Time.timeScale = 1;
        lastLives = player.lives;
    }

    private void FixedUpdate()
    {
        if (player.healthPoints <= 0)
        {
            if (player.lives > 0)
            {
                ContinueCanvas.GetComponentInChildren<UnityEngine.UI.Text>().text = lastLives.ToString();
                ContinueCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                GameOver();
            }
        }
    }
    private void Update()
    {
        TogglePause();
    }
    private void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else Resume();
        }
    }
    public void Pause()
    {
        PauseCanvas.GetComponentInChildren<UnityEngine.UI.Text>().text = lastLives.ToString();
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        isGameOver = true;
        MissionFailedCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        isGameOver = false;
        Time.timeScale = 1;
        MissionFailedCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.MusicChangerLevels("Level One");
    }
    public void ToMainMenu()
    {
        GameOver();
        SceneManager.LoadScene("Main_Menu");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Continue()
    {
        ContinueCanvas.SetActive(false);
        var p = Instantiate(PlayerPrefab, Checkpoint.position, Checkpoint.rotation);
        player = p.GetComponent<PlayerController>();
        player.lives = lastLives;
        Time.timeScale = 1;
    }
    IEnumerator LoadAsyncScene(string scene_name)//from unity docs
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene_name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
