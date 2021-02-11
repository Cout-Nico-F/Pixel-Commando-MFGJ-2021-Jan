using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISaveable
{
    public int amount;
    public GameObject prefab;

    GameManager gameManager;
    public int itemsId;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (this.gameObject.tag == "Heal" || this.gameObject.tag == "Gun" || 
            this.gameObject.tag == "RocketAmmo" || this.gameObject.tag == "JavelinAmmo")
        {
            gameManager.r_idSetter += 1;
            itemsId = gameManager.r_idSetter;
            gameManager._recollectable.Add(this);
        }
    }

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.RecolectablesData itemsData = new SaveData.RecolectablesData();
        itemsData.e_id = itemsId;
        a_SaveData.m_RecolectablesData.Add(itemsData);
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (SaveData.RecolectablesData itemsData in a_SaveData.m_RecolectablesData)
        {
            if (itemsData.e_id == itemsId)
            {
                this.gameObject.SetActive(false);
                break;
            }
        }
    }
    #endregion
}
