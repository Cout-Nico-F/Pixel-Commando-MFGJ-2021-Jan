using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyData : MonoBehaviour
{
    GameManager gameManager;

    public void CreateFile()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadFile()
    {
        GameManager.SaveJsonData(gameManager);
    }
}
