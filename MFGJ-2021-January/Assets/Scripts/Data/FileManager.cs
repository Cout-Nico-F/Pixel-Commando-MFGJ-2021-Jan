using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SFB;
using System.Runtime.InteropServices;

public static class FileManager
{
    public static string newPath;
    public static string[] loadPath;
    public static string loadPathPro;

    public static void CreateNewFile(string a_FileName)
    {
        newPath = StandaloneFileBrowser.SaveFilePanel("Save File", "", a_FileName, "dat");
        newPath.ToString();
    }
    public static void DownloadFile(string a_FileName)
    {
        loadPath = StandaloneFileBrowser.OpenFilePanel("Open File", "", "dat", false);
    }

    //Write
    public static bool WriteToFile(string a_FileContents)
    {
        //Local Storage
    #if UNITY_WEBGL
        PlayerPrefs.SetString("Data Saved", a_FileContents);
        Debug.Log("Playerprefs Data: " + PlayerPrefs.GetString("Data Saved"));
    #endif

    #if UNITY_STANDALONE
        File.WriteAllText(newPath, a_FileContents);
    #endif

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
            //Local Storage
    #if UNITY_STANDALONE
            File.WriteAllText(newPath, a_FileContents);
    #endif
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {newPath} with exception {e}");
        }
        return false;
    }

    public static void EncryptOnQuit(out string json)
    {
        json = File.ReadAllText(loadPathPro);
        StringBuilder outSb = new StringBuilder(json.Length);
        int key = 2;
        for (int i = 0; i < json.Length; i++)
        {
            char ch = (char)(json[i] * key);
            outSb.Append(ch);
        }
        json = outSb.ToString();
        File.WriteAllText(loadPathPro, json);
    }

    //Read
    public static bool LoadFromFile(out string json)
    {
    #if UNITY_WEBGL
        PlayerPrefs.GetString("Data Saved");
        Debug.Log("Playerprefs Data: " + PlayerPrefs.GetString("Data Saved"));

        json = PlayerPrefs.GetString("Data Saved");
    #endif
    #if UNITY_STANDALONE
        json = File.ReadAllText(loadPathPro);
    #endif

        #region DesEncryption
        StringBuilder outSb = new StringBuilder(json.Length);
        int key = 2;
        for (int i = 0; i < json.Length; i++)
        {
            char ch = (char)(json[i] / key);
            outSb.Append(ch);
        }
        json = outSb.ToString();
    #if UNITY_STANDALONE_WIN
        File.WriteAllText(loadPathPro, json);
    #endif
        #endregion

        try
        {
        #if UNITY_STANDALONE
            json = File.ReadAllText(loadPathPro);
        #endif
        #if UNITY_WEBGL
            json = PlayerPrefs.GetString("Data Saved");
        #endif
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {loadPathPro} with exception {e}");
            json = "";
            return false;
        }
    }

}
