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

        if (this.gameObject.tag == "Heal")
        {
            gameManager.i_idSetter += 1;
            Debug.Log(gameManager.i_idSetter);
            itemsId = gameManager.i_idSetter;
            gameManager._items.Add(this);
        }
    }

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.ItemsData itemsData = new SaveData.ItemsData();
        itemsData.e_id = itemsId;
        a_SaveData.m_ItemsData.Add(itemsData);
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (SaveData.ItemsData itemsData in a_SaveData.m_ItemsData)
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
