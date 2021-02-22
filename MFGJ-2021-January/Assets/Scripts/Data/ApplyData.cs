using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyData : MonoBehaviour
{
    DataManager dataManager;
    GameManager gameManager;

    int repeat = 0;

    public void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        gameManager = FindObjectOfType<GameManager>();  
    }

    public void Start()
    {
        if(repeat == 0 && gameManager !=null)//To avoid editor errors in the console
        {
            //Create or Load Files
            if (gameManager.isNewGame) CreateFile();
            else LoadFile(); 

            repeat++;
        }
    }

    public void CreateFile()
    {
        DataManager.SaveJsonData(dataManager);
    }

    public void LoadFile()
    {
        DataManager.LoadJsonData(dataManager);
    }
}
