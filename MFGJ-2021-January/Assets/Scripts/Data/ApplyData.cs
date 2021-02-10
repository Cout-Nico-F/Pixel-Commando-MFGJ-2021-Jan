using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyData : MonoBehaviour
{
    GameManager gameManager;
    MenuManager menuManager;

    int repeat = 0;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        menuManager = FindObjectOfType<MenuManager>();  
    }

    public void Update()
    {
        if(repeat == 0)
        {
            if (menuManager.isNewGame) CreateFile();
            else LoadFile();

            repeat++;
        }

        if(Input.GetKey(KeyCode.G)) GameManager.SaveJsonData(gameManager);
        if (Input.GetKey(KeyCode.L)) GameManager.LoadJsonData(gameManager);
    }

    public void CreateFile()
    {
        GameManager.SaveJsonData(gameManager);
    }

    public void LoadFile()
    {
        GameManager.LoadJsonData(gameManager);
    }
}
