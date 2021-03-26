using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyData : MonoBehaviour
{
    DataManager dataManager;
    GameManager gameManager;
    DifficultySetter difficultySetter;

    int repeat = 0;

    public void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        gameManager = FindObjectOfType<GameManager>();
        difficultySetter = FindObjectOfType<DifficultySetter>();
    }

    public void Start()
    {
        if (gameManager.isNewGame) CreateFile();
        else LoadFile();
    }

    public void CreateFile()
    {
        DataManager.SaveJsonData(dataManager);
    }

    public void LoadFile()
    {
        
        DataManager.LoadJsonData(dataManager);
        try
        {
            switch (gameManager.Difficulty)
            {
                case 1:
                    difficultySetter.TweakPlayer(health: 200, lives: 5);
                    difficultySetter.TweakEnemies(health_multiplicator: 0.65);
                    difficultySetter.TweakConsumibles(rockets: 3, healing_multiplicator: 1.5);
                    break;
                case 2:
                    break;
                case 3:
                    difficultySetter.TweakPlayer(health: 80, lives: 2);
                    difficultySetter.TweakEnemies(health_multiplicator: 2.10);
                    difficultySetter.TweakConsumibles(rockets: 1, healing_multiplicator: 0.5);
                    break;
                default:
                    break;
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
