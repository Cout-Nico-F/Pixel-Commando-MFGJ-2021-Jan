using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyData : MonoBehaviour
{
    DataManager dataManager;
    GameManager gameManager;
    DifficultySetter difficultySetter;

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
        difficultySetter.SwitchDiff();
    }
}
