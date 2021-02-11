using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    #region Variables
    //the game manager class will manage the scene changes, pause, restart and etc, and will be the intermediate between UI and the player.
    public static GameManager gameManager;

    private bool isGameOver;
    public bool IsGameOver { get => isGameOver; }

    [SerializeField]
    PlayerController player = null;
    Gunning gunning;

    public GameObject PlayerPrefab;
    public GameObject PauseCanvas;
    public GameObject ContinueCanvas;
    public GameObject MissionFailedCanvas;
    [SerializeField] GameObject MapCanvas;
    public Transform Checkpoint;

    public GameObject rocketsUI;
    public GameObject javelinUI;
    public GameObject scoreUI;
    public GameObject livesUI;
    public GameObject ammoUI;

    public int score = 0;
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

    [Header("Enemies")]
    Enemy enemy;
    public int idSetter;
    public List<Enemy> _enemies = new List<Enemy>();
    public List<int> _destroyedEnemies = new List<int>();
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gunning = FindObjectOfType<Gunning>();
        enemy = FindObjectOfType<Enemy>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        hintsManager = FindObjectOfType<HintsManager>();

        score = 0;

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Time.timeScale = 1;
        lastLives = player.lives;

        Debug.Log(Application.persistentDataPath);
    }

    private void FixedUpdate()
    {
        if (player.healthPoints <= 0)
        {
            MapCanvas.SetActive(false);
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
        livesUI.GetComponentInChildren<UnityEngine.UI.Text>().text = player.lives.ToString();
        ammoUI.GetComponentInChildren<UnityEngine.UI.Text>().text = player.gunning.initial_Ammo.ToString();
        if (ammoUI.GetComponentInChildren<UnityEngine.UI.Text>().text == "0")
        {
            ammoUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "- - -";
        }
    }

    private void Update()
    {
        TogglePause();
        CheckScore();
        ToggleMap();
    }
    #endregion

    #region Other Methods
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
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !MapCanvas.activeSelf)
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else Resume();
        }
    }
    private void EndOfProtectedTime()
    {
        PlayerPrefab.tag = "Player";
    }
    private void ToggleMap()
    {
        if (Input.GetKeyDown(KeyCode.M) && !PauseCanvas.activeSelf)
        {
            MapCanvas.SetActive(!MapCanvas.activeSelf);
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
            else Time.timeScale = 1;
        }
    }
    #endregion

    #region Game States
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

        //Update Transform
        PlayerPrefab.transform.position = Checkpoint.position;
        PlayerPrefab.transform.rotation = Checkpoint.rotation;
        PlayerPrefab.tag = "Untagged";
        PlayerPrefab.SetActive(true);

        //Update Values
        var p = PlayerPrefab.GetComponent<PlayerController>();
        p.lives = lastLives; 
        p.healthPoints = p.maxHealthPoints; //Reset health points 

        p.gunning.rocketsAmmo = lastRocketsAmmo;
        p.gunning.javelinAmmo = lastJavelinAmmo;
        p.gunning.selectedSpecial = lastSelectedSpecial;

        Time.timeScale = 1;
        audioManager.MusicChangerLevels("Level One");

        //To keep enemies from damaging us when we spawn again.
        Invoke(nameof(EndOfProtectedTime), 2.0f);
    }
    #endregion

    #region Saving and Loading Data
    //Save
    public static void SaveJsonData(GameManager a_GameManager)
    {
        SaveData sd = new SaveData();
        a_GameManager.PopulateSaveData(sd);

        if(FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save Successful");
        }
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        //Score 
        a_SaveData.m_PlayerData.p_score = score;

        //Player Data
        player.PopulateSaveData(a_SaveData);

        //Ammo Data
        gunning.PopulateSaveData(a_SaveData);

        //Enemies Data
        a_SaveData.m_deathEnemyList = _destroyedEnemies;
        foreach (Enemy enemy in _enemies)
        {
            enemy.PopulateSaveData(a_SaveData);
        }
        foreach(int enemyUuid in _destroyedEnemies)
        {
            SaveData.EnemyData enemyData = new SaveData.EnemyData();
            enemyData.e_health = 0;
            enemyData.e_id = FindObjectOfType<Enemy>().enemyId;
            a_SaveData.m_EnemyData.Add(enemyData);
            Debug.Log(enemyUuid);
        }
    }

    //Load
    public  static void LoadJsonData(GameManager a_GameManager)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            a_GameManager.LoadFromSaveData(sd);
            Debug.Log("Load Complete");
        }
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        //Score
        score = a_SaveData.m_PlayerData.p_score;
        player.healthPoints = a_SaveData.m_PlayerData.p_health;

        //Player
        player.LoadFromSaveData(a_SaveData);

        //Ammo
        gunning.LoadFromSaveData(a_SaveData);

        //Enemies
        _destroyedEnemies = a_SaveData.m_deathEnemyList;
        foreach (Enemy enemy in _enemies)
        {
            enemy.LoadFromSaveData(a_SaveData);
        }
    }
    #endregion
}
