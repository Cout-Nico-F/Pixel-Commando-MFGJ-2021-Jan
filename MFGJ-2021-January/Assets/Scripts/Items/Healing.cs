using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISaveable
{
    public int amount;
    //[HideInInspector]
    public int healthPoints = 100; //This variable is only for hide grabbed items on Load (Load System)...and its temporal..I need to find other solution
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healthPoints = 0;
            Debug.Log("This Id: " + itemsId);
            gameManager._grabbedRecollectables.Add(this.itemsId); //Add "Destroyed" Item to Data.
        }
    }

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.RecolectablesData itemsData = new SaveData.RecolectablesData();
        itemsData.r_id = itemsId;
        itemsData.r_healthForHide = healthPoints;
        a_SaveData.m_RecolectablesData.Add(itemsData);
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (SaveData.RecolectablesData itemsData in a_SaveData.m_RecolectablesData)
        {
            if (itemsData.r_id == itemsId)
            {
                healthPoints = itemsData.r_healthForHide;
                break;
            }
        }
        if (healthPoints <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
