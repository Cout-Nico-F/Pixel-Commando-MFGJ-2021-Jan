using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

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

    //Player
    public PlayerData m_PlayerData;
    //Bullets
    public AmmoData m_AmmoData;

    //Enemies
    public List<Enemy> m_enemyList = new List<Enemy>();
    public List<EnemyData> m_EnemyData = new List<EnemyData>();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}
