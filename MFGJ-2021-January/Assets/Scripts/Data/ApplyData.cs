using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyData : MonoBehaviour
{
    DataManager dataManager;
    MenuManager menuManager;

    int repeat = 0;

    public void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        menuManager = FindObjectOfType<MenuManager>();  
    }

    public void Update()
    {
        if(repeat == 0 && menuManager !=null)//To avoid editor errors in the console
        {
            //Create or Load Files
            if (menuManager.isNewGame) CreateFile();
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
