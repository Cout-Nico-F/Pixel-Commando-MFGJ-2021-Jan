using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

//Game States Enum
public enum GameStateEnum
{
    HOME,
    NEW_GAME,
    LOAD_GAME,
    IN_GAME,
    GAME_OVER
}

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager sharedInstance;
    [SerializeField]
    AudioManager audioManager;

    //Start Game States
    public GameStateEnum currentGameState = GameStateEnum.HOME;

    public string dataFileName = "PixelCommando.dat";
    public bool isNewGame;
    private int level;
    //private int repeat = 0;
    private int difficulty;
    public int Difficulty { get => difficulty; set => difficulty = value; }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (sharedInstance == null) sharedInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //Level
        PlayerPrefs.GetInt("Level");
    }
    #endregion

    public void CurrentLevel(int lv)
    {
        PlayerPrefs.SetInt("Level", lv);
    }

    #region Game States
    //HOME
    public void Home()
    {
        sharedInstance.currentGameState = GameStateEnum.HOME;
        SetGameState(currentGameState);
    }
    //NEW GAME
    public void NewGame()
    {
        sharedInstance.currentGameState = GameStateEnum.NEW_GAME;
        SetGameState(currentGameState); 
    }
    //LOAD GAME
    public void LoadGame()
    {
        sharedInstance.currentGameState = GameStateEnum.LOAD_GAME;
        SetGameState(currentGameState);
    }
    //START GAME
    public void StartGame()
    {
        sharedInstance.currentGameState = GameStateEnum.IN_GAME;
        SetGameState(currentGameState);
    }
    //NEXT LEVEL
    public void NextLevel(int lv)
    {
        PlayerPrefs.SetInt("Level", lv);
        StartGame();
    }
    public void GameOver()
    {
        sharedInstance.currentGameState = GameStateEnum.GAME_OVER;
        SetGameState(currentGameState);
    }
    //QUIT
    public void QuitGame()
    {
        Application.Quit();
    }

    //Set the Game State
    private void SetGameState(GameStateEnum newGameState)
    {
        if (newGameState == GameStateEnum.HOME) //Home
        {
            SceneManager.LoadScene("Main_Menu");
        }
        else if (newGameState == GameStateEnum.NEW_GAME) //New Game -> New File
        {
            isNewGame = true;
            CurrentLevel(1);
            SceneManager.LoadScene("Briefing");
            audioManager.PlayVoiceCommand("Brief");
        }
        else if (newGameState == GameStateEnum.LOAD_GAME) //Load Game -> Load File
        {
            //Create Data
            isNewGame = false;
            PlayerPrefs.GetInt("Level");
            StartGame();
        }
        else if (newGameState == GameStateEnum.IN_GAME) //Gameplay
        {
            //Set Level
            switch (PlayerPrefs.GetInt("Level"))
            {
                case 1:
                    SceneManager.LoadScene("Level One");
                    Debug.Log(isNewGame);
                    audioManager = FindObjectOfType<AudioManager>();
                    audioManager.MusicChangerLevels("Level One");
                    break;
                case 2:
                    SceneManager.LoadScene("Level Two");

                    //Create new archive with Level 2 data
                    DataManager.SaveJsonData(FindObjectOfType<DataManager>());
                    break;
            }
        }
        else if (newGameState == GameStateEnum.GAME_OVER) //Game Over
        {
           //ANYTHING
        }

        //Update Game State
        newGameState = currentGameState;
        currentGameState = newGameState;
    }
    #endregion

}
