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
    public GameObject scoreUI;

    public int score;
    private bool hScore1 = false;
    private bool hScore2 = false;
    private bool hScore3 = false;

    HintsManager hintsManager;
    AudioManager audioManager;
    [HideInInspector]
    public int lastLives;
    [HideInInspector]
    public int lastJavelinAmmo;
    [HideInInspector]
    public int lastRocketsAmmo;
    [HideInInspector]
    public string lastSelectedSpecial;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        hintsManager = FindObjectOfType<HintsManager>();
        score = 0;
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
        scoreUI.GetComponentInChildren<UnityEngine.UI.Text>().text = score.ToString();
    }
    private void Update()
    {
        TogglePause();
        CheckScore();
    }
    private void CheckScore()
    {
        if (score >= 4000 && hScore1 == false) //placeholder ammount to gain 1up
        {
            hintsManager.ShowHintPanel("score", 3);
             
            player.lives++;
            hScore1 = true;
            //Play 1up SFX
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
        }
        else if (score >= 12500 && hScore2 == false)
        {
            hintsManager.ShowHintPanel("score", 3);

            player.lives++;
            hScore2 = true;
            //Play 1up SFX
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
        }
        else if (score >= 22000 && hScore3 == false)
        {
            hintsManager.ShowHintPanel("score", 3);

            player.lives++;
            hScore3 = true;
            //Play 1up SFX
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder.
            audioManager.PlaySound("PickUpWeapon");//placeholder. More sounds at the same time makes it hear stronger. its only a placeholder.
        }
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
        player.gunning.rocketsAmmo = lastRocketsAmmo;
        player.gunning.javelinAmmo = lastJavelinAmmo;
        player.gunning.selectedSpecial = lastSelectedSpecial;

        Time.timeScale = 1;
        audioManager.MusicChangerLevels("Level One");
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
