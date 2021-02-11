using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    #region Struts Data
    [System.Serializable]
    public struct EnemyData
    {
        public int e_id;
        public int e_health;
    }

    [System.Serializable]
    public struct PlayerData
    {
        public Vector3 p_position;
        public int p_lives;
        public int p_health;
        public int p_score;
    }

    [System.Serializable]
    public struct AmmoData
    {
        public int a_rocketAmmo;
        public int a_javelinAmmo;
        public int a_initialAmmo;
    }

    [System.Serializable]
    public struct WeaponsData
    {
        public int e_id;
    }

    [System.Serializable]
    public struct ItemsData
    {
        public int e_id;
    }
    #endregion

    #region Struct Objects & Lists
    //Player
    public PlayerData m_PlayerData;

    //Bullets
    public AmmoData m_AmmoData;

    //Enemies
    public List<Enemy> m_enemyList = new List<Enemy>();
    public List<int> m_deathEnemyList = new List<int>();
    public List<EnemyData> m_EnemyData = new List<EnemyData>();

    //Weapons
    //public List<> m_weaponsList = new List<>();
    public List<int> m_grabbedWeaponsList = new List<int>();
    public List<WeaponsData> m_WeaponsData = new List<WeaponsData>();

    //Items -> Potions, apples, cookies, etc...
    public List<Healing> m_itemsList = new List<Healing>();
    public List<int> m_grabbedItemsList = new List<int>();
    public List<ItemsData> m_ItemsData = new List<ItemsData>();
    #endregion

    #region JSON
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
    #endregion
}

#region Interface
public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}
#endregion
