using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISaveable
{
    public int amount;
    //[HideInInspector]
    public bool isGrabbed = false; 
    public GameObject prefab;

    LevelManager levelManager;
    public int itemsId;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        if (this.gameObject.tag == "Heal" || this.gameObject.tag == "Gun" || 
            this.gameObject.tag == "RocketAmmo" || this.gameObject.tag == "JavelinAmmo")
        {
            levelManager.r_idSetter += 1;
            itemsId = levelManager.r_idSetter;
            levelManager._recollectable.Add(this);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.isGrabbed = true;
            //Debug.Log("This Id: " + itemsId);
            levelManager._grabbedRecollectables.Add(this.itemsId); //Add "Destroyed" Item to Data.
        }
    }

    #region Saving and Loading Data
    //Save
    public void PopulateSaveData(SaveData a_SaveData)
    {
        SaveData.RecolectablesData itemsData = new SaveData.RecolectablesData();
        itemsData.r_id = itemsId;
        itemsData.r_isGrabbed = isGrabbed;
        a_SaveData.m_RecolectablesData.Add(itemsData);
    }

    //Load
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        foreach (SaveData.RecolectablesData itemsData in a_SaveData.m_RecolectablesData)
        {
            if (itemsData.r_id == itemsId)
            {
                isGrabbed = itemsData.r_isGrabbed;
                break;
            }
        }
        if (isGrabbed == true)
        {
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
