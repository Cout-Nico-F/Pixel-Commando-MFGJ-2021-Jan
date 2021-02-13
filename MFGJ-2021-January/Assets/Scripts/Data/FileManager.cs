using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{
    public static bool isLoadFromJSON = false;
    //Write
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        File.Open(a_FileName, FileMode.OpenOrCreate);


        #region Encryption
        StringBuilder outSb = new StringBuilder(a_FileContents.Length);
        int key = 2;
        for (int i = 0; i < a_FileContents.Length; i++)
        {
            char ch = (char)(a_FileContents[i] * key);
            outSb.Append(ch);
        }
        a_FileContents = outSb.ToString();
        #endregion

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {fullPath} with exception {e}");
        }
        return false;
    }

    //Read
    public static bool LoadFromFile(string a_FileName, out string json)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        json = File.ReadAllText(fullPath);

        #region DesEncryption
        StringBuilder outSb = new StringBuilder(json.Length);
        int key = 2;
        for (int i = 0; i < json.Length; i++)
        {
            char ch = (char)(json[i] / key);
            outSb.Append(ch);
        }
        json = outSb.ToString();
        File.WriteAllText(fullPath, json);
        JsonUtility.FromJsonOverwrite(json, a_FileName);
        #endregion

        try
        {
            json = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {fullPath} with exception {e}");
            json = "";
            return false;
        }
    }

}
