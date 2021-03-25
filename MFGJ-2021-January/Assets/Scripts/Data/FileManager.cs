using System;
using System.IO;
using System.Text;
using UnityEngine;

public static class FileManager
{
    //Write
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        Debug.Log(fullPath);

        //Local Storage
    #if UNITY_WEBGL
        PlayerPrefs.SetString("Data Saved", a_FileContents);
    #endif

    #if UNITY_STANDALONE
        File.WriteAllText(fullPath, a_FileContents);
        Debug.Log(a_FileContents);
    #endif

        #region Encryption
        /*StringBuilder outSb = new StringBuilder(a_FileContents.Length);
        int key = 2;
        for (int i = 0; i < a_FileContents.Length; i++)
        {
            char ch = (char)(a_FileContents[i] * key);
            outSb.Append(ch);
        }
        a_FileContents = outSb.ToString();*/
    #endregion

        try
        {
            //Local Storage
    #if UNITY_STANDALONE
            File.WriteAllText(fullPath, a_FileContents);
    #endif
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {fullPath} with exception {e}");
        }
        return false;
    }

    public static void EncryptOnQuit(string a_FileName, out string json)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        json = File.ReadAllText(fullPath);
        /*StringBuilder outSb = new StringBuilder(json.Length);
        int key = 2;
        for (int i = 0; i < json.Length; i++)
        {
            char ch = (char)(json[i] * key);
            outSb.Append(ch);
        }
        json = outSb.ToString();
        File.WriteAllText(fullPath, json);*/
    }

    //Read
    public static bool LoadFromFile(string a_FileName, out string json)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

    #if UNITY_WEBGL
        PlayerPrefs.GetString("Data Saved");
        json = PlayerPrefs.GetString("Data Saved");
    #endif
    #if UNITY_STANDALONE
        json = File.ReadAllText(fullPath);
    #endif

        #region DesEncryption
        /*StringBuilder outSb = new StringBuilder(json.Length);
        int key = 2;
        for (int i = 0; i < json.Length; i++)
        {
            char ch = (char)(json[i] / key);
            outSb.Append(ch);
        }
        json = outSb.ToString();*/
    #if UNITY_STANDALONE_WIN
        File.WriteAllText(fullPath, json);
    #endif
        #endregion

        try
        {
        #if UNITY_STANDALONE
            json = File.ReadAllText(fullPath);
        #endif
        #if UNITY_WEBGL
            json = PlayerPrefs.GetString("Data Saved");
        #endif
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
