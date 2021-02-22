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

    //Start Game State
    public GameStateEnum currentGameState = GameStateEnum.HOME;

    public string dataFileName = "PixelCommando.dat";
    public bool isNewGame;
    public int level = 1;
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

        //Init variables
        level = 1;
    }
    #endregion

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

    //RESTART
    public void GameOver()
    {
        sharedInstance.currentGameState = GameStateEnum.GAME_OVER;
        SetGameState(currentGameState);
    }
    #endregion

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
            SceneManager.LoadScene("Briefing");
            audioManager.PlayVoiceCommand("Brief");
        }
        else if (newGameState == GameStateEnum.LOAD_GAME) //Load Game -> Load File
        {
            //Create Data
            isNewGame = false;
            StartGame();
        }
        else if (newGameState == GameStateEnum.IN_GAME) //Gameplay
        {
            //Set Level
            switch (level)
            {
                case 1:
                    SceneManager.LoadScene("Level One");
                    audioManager = FindObjectOfType<AudioManager>();
                    audioManager.MusicChangerLevels("Level One");
                    break;
                case 2:
                    //Load Second Level
                    Debug.Log("LEVEL TWO");
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


}
