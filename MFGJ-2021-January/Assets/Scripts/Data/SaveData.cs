using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    [System.Serializable]
    public struct EnemyData
    {
        public int m_mUuid;
        public int m_health;
    }

    public int m_Score;
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
