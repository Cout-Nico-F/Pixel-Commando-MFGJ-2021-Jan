using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, ISaveable
{
    GameManager gameManager;
    PlayerController player;
    Enemy enemy;
    Gunning gunning;
    Healing healing;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        gunning = FindObjectOfType<Gunning>();
        enemy = FindObjectOfType<Enemy>();
        healing = FindObjectOfType<Healing>();

        DontDestroyOnLoad(this.gameObject);
    }

    #region Save
    public static void SaveJsonData(DataManager a_DataManager)
    {
        SaveData sd = new SaveData();
        a_DataManager.PopulateSaveData(sd);

        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save Successful");
        }
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        //Score 
        a_SaveData.m_PlayerData.p_score = gameManager.score;

        //Player Data
        player.PopulateSaveData(a_SaveData);

        //Ammo Data
        gunning.PopulateSaveData(a_SaveData);

        //Items Data
        a_SaveData.m_grabbedItemsList = gameManager._grabbedItems;
        foreach (Healing items in gameManager._items)
        {
            items.PopulateSaveData(a_SaveData);
        }
        foreach (int itemsUuid in gameManager._grabbedItems)
        {
            SaveData.ItemsData itemData = new SaveData.ItemsData();
            itemData.e_id = FindObjectOfType<Healing>().itemsId;
            a_SaveData.m_ItemsData.Add(itemData);
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
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            a_DataManager.LoadFromSaveData(sd);
            Debug.Log("Load Complete");
        }
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        //Score
        gameManager.score = a_SaveData.m_PlayerData.p_score;

        //Player
        player.LoadFromSaveData(a_SaveData);

        //Ammo
        gunning.LoadFromSaveData(a_SaveData);

        //Items Data
        gameManager._grabbedItems = a_SaveData.m_grabbedItemsList;
        foreach (Healing item in gameManager._items)
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
