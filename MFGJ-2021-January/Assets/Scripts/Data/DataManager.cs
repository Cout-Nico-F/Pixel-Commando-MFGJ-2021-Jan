﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataManager : MonoBehaviour, ISaveable
{
    GameManager gameManager;
    PlayerController player;
    Boss boss;
    Enemy enemy;
    Gunning gunning;
    Healing healing;

    public static int timesSaved = 0;

    public static DataManager instance;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        boss = FindObjectOfType<Boss>();
        gunning = FindObjectOfType<Gunning>();
        enemy = FindObjectOfType<Enemy>();
        healing = FindObjectOfType<Healing>();
        timesSaved = 0;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #region Save
    public static void SaveJsonData(DataManager a_DataManager)
    {
        
        SaveData sd = new SaveData();
        a_DataManager.PopulateSaveData(sd);

       
        if (FileManager.WriteToFile(sd.ToJson()))
        {
            Debug.Log("Save Successful");
            timesSaved++;
        }
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        //Score 
        a_SaveData.m_PlayerData.p_score = gameManager.score;

        //Player Data
        player.PopulateSaveData(a_SaveData);

        //Boss Data
        boss.PopulateSaveData(a_SaveData);

        //Ammo Data
        gunning.PopulateSaveData(a_SaveData);

        //Recollectables Data
        a_SaveData.m_grabbedRecolectablesList = gameManager._grabbedRecollectables;
        foreach (Healing items in gameManager._recollectable)
        {
            items.PopulateSaveData(a_SaveData);
        }
        foreach (int itemsUuid in gameManager._grabbedRecollectables)
        {
            SaveData.RecolectablesData itemData = new SaveData.RecolectablesData();
            itemData.r_id = FindObjectOfType<Healing>().itemsId;
            a_SaveData.m_RecolectablesData.Add(itemData);
        }

        //Enemies Data
        a_SaveData.m_deathEnemyList = gameManager._destroyedEnemies;
        foreach (Enemy enemy in gameManager._enemies)
        {
            enemy.PopulateSaveData(a_SaveData);
        }
        foreach (int enemyUuid in gameManager._destroyedEnemies)
        {
            SaveData.EnemyData enemyData = new SaveData.EnemyData();
            enemyData.e_health = 0;
            enemyData.e_id = FindObjectOfType<Enemy>().enemyId;
            a_SaveData.m_EnemyData.Add(enemyData);
        }
    }
    #endregion

    #region Load
    public static void LoadJsonData(DataManager a_DataManager)
    {
        if (FileManager.LoadFromFile(out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            a_DataManager.LoadFromSaveData(sd);
            Debug.Log("Load Complete");
        }
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        //Score Data
        gameManager.score = a_SaveData.m_PlayerData.p_score;

        //Player Data
        player.LoadFromSaveData(a_SaveData);

        //Boss Data
        boss.LoadFromSaveData(a_SaveData);

        //Ammo Data
        gunning.LoadFromSaveData(a_SaveData);

        //Recollectables Data
        gameManager._grabbedRecollectables = a_SaveData.m_grabbedRecolectablesList;
        foreach (Healing item in gameManager._recollectable)
        {
            item.LoadFromSaveData(a_SaveData);
        }

        //Enemies Data
        gameManager._destroyedEnemies = a_SaveData.m_deathEnemyList;
        foreach (Enemy enemy in gameManager._enemies)
        {
            enemy.LoadFromSaveData(a_SaveData);
        }

    }
    #endregion
}
