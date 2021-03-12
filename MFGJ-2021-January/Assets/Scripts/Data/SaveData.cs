using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    #region Structs Data
    [System.Serializable]
    public struct LevelData
    {
        public int l_level;
    }

    public struct EnemyData
    {
        public int e_id;
        public int e_health;
        public bool e_isDead;
        public Vector3 e_position;
    }

    [System.Serializable]
    public struct BossData
    {
        public int b_health;
        public float b_speed;
        public int b_zone;
    }

    [System.Serializable]
    public struct PlayerData
    {
        public Vector3 p_position;
        public int p_lives;
        public int p_health;
    }

    [System.Serializable]
    public struct ScoreData
    {
        public int s_score;
        public bool s_hgScore1;
        public bool s_hgScore2;
        public bool s_hgScore3;
    }

    [System.Serializable]
    public struct AmmoData
    {
        public int a_rocketAmmo;
        public int a_javelinAmmo;
        public int a_initialAmmo;
    }

    [System.Serializable]
    public struct RecolectablesData
    {
        public int r_id;
        public bool r_isGrabbed;
    }
    #endregion

    #region Struct Objects & Lists
    //Level
    public LevelData m_LevelData;

    //Player
    public PlayerData m_PlayerData;

    //Score
    public ScoreData m_ScoreData;

    //Bullets
    public AmmoData m_AmmoData;

    //Enemies
    public List<Enemy> m_enemyList = new List<Enemy>();
    public List<int> m_deathEnemyList = new List<int>();
    public List<EnemyData> m_EnemyData = new List<EnemyData>();

    //Boss 
    public BossData m_BossData;

    //Recollectables -> Potions, apples, cookies, etc...
    public List<Healing> m_recolectablesList = new List<Healing>();
    public List<int> m_grabbedRecolectablesList = new List<int>();
    public List<RecolectablesData> m_RecolectablesData = new List<RecolectablesData>();
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
